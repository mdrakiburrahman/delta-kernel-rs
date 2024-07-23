using Apache.Arrow;
using Apache.Arrow.C;
using Apache.Arrow.Types;
using Deltalake.Kernel.Rust.Interop.Ffi.Test.Arrow.Converter;
using Deltalake.Kernel.Rust.Interop.Ffi.Test.Arrow.Properties;
using Deltalake.Kernel.Rust.Interop.Ffi.Test.Callbacks.Errors;
using Deltalake.Kernel.Rust.Interop.Ffi.Test.Callbacks.Visit;
using Deltalake.Kernel.Rust.Interop.Ffi.Test.Schema.Context;
using DeltaLake.Kernel.Rust.Ffi;
using DeltaLake.Kernel.Rust.Interop.Ffi.Test.Extensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Deltalake.Kernel.Rust.Interop.Ffi.Test.Arrow.Handlers
{
    public class ArrowFFIInteropHandler : IArrowInteropHandler
    {
        public ArrowFFIInteropHandler() { }

        public unsafe void AddBatchToContext(
            ArrowContext* context,
            ArrowFFIData* arrowData,
            PartitionList* partitionCols,
            CStringMap* partitionValues
        )
        {
            var schema = GetSchema(&arrowData->schema);
            var recordBatch = GetRecordBatch(&arrowData->array, schema);

            if (context->CurFilter != null)
            {
                Console.WriteLine(
                    $"WARNING: Not applying any filter since Apache.Arrow does not support garrow_record_batch_filter yet"
                );
            }

            recordBatch = AddPartitionColumns(recordBatch, partitionCols, partitionValues);
            if (recordBatch == null)
            {
                Console.WriteLine("Failed to add partition columns, not adding batch");
                return;
            }

            // Add the record batch to the context
            //
            var newBatches = new RecordBatch*[context->NumBatches + 1];
            if (context->NumBatches > 0)
            {
                for (ulong i = 0; i < context->NumBatches; i++)
                {
                    newBatches[i] = context->Batches[i];
                }
            }
            fixed (RecordBatch** batchesPtr = newBatches)
            {
                context->Batches = batchesPtr;
            }
            newBatches[context->NumBatches] = (RecordBatch*)Unsafe.AsPointer(ref recordBatch);
            context->NumBatches++;

            Console.WriteLine(
                $"Added batch to arrow context, have {context->NumBatches} batches in context now"
            );
        }

        public unsafe void CReadParquetFile(
            EngineContext* context,
            KernelStringSlice path,
            KernelBoolSlice selectionVector
        )
        {
            string tableRoot = Marshal.PtrToStringAnsi((IntPtr)context->TableRoot);
            int fullLen = tableRoot.Length + (int)path.len + 1;
            char* fullPath = (char*)Marshal.AllocHGlobal(sizeof(char) * fullLen);
            string fullPathStr =
                $"{tableRoot}{Marshal.PtrToStringAnsi((IntPtr)path.ptr, (int)path.len)}";

            fixed (sbyte* keyPtr = fullPathStr.ToSByte())
            {
                KernelStringSlice tablePathSlice = new KernelStringSlice
                {
                    ptr = keyPtr,
                    len = (nuint)fullPathStr.Length
                };
                FileMeta meta = new FileMeta { path = tablePathSlice, };

                Console.WriteLine($"\tReading parquet file at {fullPathStr}");
                ExternResultHandleExclusiveFileReadResultIterator readRes =
                    FFI_NativeMethodsHandler.read_parquet_file(
                        context->Engine,
                        &meta,
                        context->ReadSchema
                    );
                if (
                    readRes.tag
                    != ExternResultHandleExclusiveFileReadResultIterator_Tag.OkHandleExclusiveFileReadResultIterator
                )
                {
                    Console.WriteLine("Failed to read parquet file");
                    return;
                }
                ExclusiveFileReadResultIterator* readIterator = readRes.Anonymous.Anonymous1.ok;

                if (selectionVector.len > 0)
                {
                    Console.WriteLine(
                        $"WARNING: Not applying selection vector since Apache.Arrow does not support garrow_record_batch_filter yet"
                    );
                }

                for (; ; )
                {
                    ExternResultbool okRes = FFI_NativeMethodsHandler.read_result_next(
                        readIterator,
                        context,
                        Marshal.GetFunctionPointerForDelegate(VisitCallbacks.VisitReadDataDemo)
                    );
                    if (okRes.tag != ExternResultbool_Tag.Okbool)
                    {
                        ReadError* castedError = (ReadError*)okRes.Anonymous.Anonymous2.err;
                        throw new InvalidOperationException(
                            $"Failed to iterate read data: {castedError->Message}"
                        );
                    }
                    else if (!okRes.Anonymous.Anonymous1.ok)
                    {
                        Console.WriteLine("\tDone reading parquet file\n");
                        break;
                    }
                }

                FFI_NativeMethodsHandler.free_read_result_iter(readIterator);
            }
        }

        private unsafe Apache.Arrow.Schema GetSchema(FFI_ArrowSchema* schema)
        {
            return CArrowSchemaImporter.ImportSchema(
                ArrowFfiSchemaConverter.ConvertFFISchema(schema)
            );
        }

        private unsafe static RecordBatch GetRecordBatch(
            FFI_ArrowArray* array,
            Apache.Arrow.Schema schema
        )
        {
            return CArrowArrayImporter.ImportRecordBatch(
                ArrowFfiSchemaConverter.ConvertFFIArray(array),
                schema
            );
        }

        private unsafe static RecordBatch AddPartitionColumns(
            RecordBatch recordBatch,
            PartitionList* partitionCols,
            CStringMap* partitionValues
        )
        {
            var schemaBuilder = new Apache.Arrow.Schema.Builder();
            foreach (var field in recordBatch.Schema.FieldsList)
            {
                schemaBuilder.Field(field);
            }

            var fields = new List<Field>(recordBatch.Schema.FieldsList);
            var columns = new List<IArrowArray>();
            for (int i = 0; i < recordBatch.Schema.FieldsList.Count; i++)
            {
                columns.Add(recordBatch.Column(i));
            }

            for (int i = 0; i < partitionCols->Len; i++)
            {
                var colName = Marshal.PtrToStringAnsi((IntPtr)partitionCols->Cols[i]);
                var field = new Field(colName, StringType.Default, nullable: true);
                schemaBuilder.Field(field);
                fields.Add(field);

                var columnBuilder = new StringArray.Builder();
                var partitionValPtr = FFI_NativeMethodsHandler.get_from_map(
                    partitionValues,
                    new KernelStringSlice
                    {
                        ptr = (sbyte*)partitionCols->Cols[i],
                        len = (ulong)colName.Length
                    },
                    Marshal.GetFunctionPointerForDelegate(StringAllocator.AllocateString)
                );
                var partitionVal =
                    partitionValPtr != null
                        ? Marshal.PtrToStringAnsi((IntPtr)partitionValPtr)
                        : null;

                for (int j = 0; j < recordBatch.Length; j++)
                {
                    columnBuilder.Append(partitionVal ?? "");
                }

                columns.Add(columnBuilder.Build());
            }

            var newSchema = schemaBuilder.Build();
            return new RecordBatch(newSchema, columns, recordBatch.Length);
        }
    }
}
