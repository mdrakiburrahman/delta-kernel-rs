using Deltalake.Kernel.Rust.Interop.Ffi.Test.Callbacks.Schema;
using Deltalake.Kernel.Rust.Interop.Ffi.Test.Callbacks.Visit;
using Deltalake.Kernel.Rust.Interop.Ffi.Test.Schema.Context;
using Deltalake.Kernel.Rust.Interop.Ffi.Test.Schema.Properties;
using DeltaLake.Kernel.Rust.Ffi;
using System.Runtime.InteropServices;

namespace Deltalake.Kernel.Rust.Interop.Ffi.Test.Schema.Handlers
{
    public class SchemaHandlerDemo : ISchemaHandler
    {
        public unsafe void PrintSchema(SharedSnapshot* snapshot)
        {
            Console.WriteLine("Building schema\n");
            SchemaBuilder builder = new SchemaBuilder() { list_count = 0, };

            EngineSchemaVisitor visitor = new EngineSchemaVisitor()
            {
                data = &builder,
                make_field_list = Marshal.GetFunctionPointerForDelegate(EngineSchemaVisitorCallbacks.MakeFieldListDemo),
                visit_struct = Marshal.GetFunctionPointerForDelegate(EngineSchemaVisitorCallbacks.VisitStructDemo),
                visit_array = Marshal.GetFunctionPointerForDelegate(EngineSchemaVisitorCallbacks.VisitArrayDemo),
                visit_map = Marshal.GetFunctionPointerForDelegate(EngineSchemaVisitorCallbacks.VisitMapDemo),
                visit_decimal = Marshal.GetFunctionPointerForDelegate(EngineSchemaVisitorCallbacks.VisitDecimalDemo),
                visit_string = Marshal.GetFunctionPointerForDelegate( EngineSchemaVisitorCallbacks.VisitStringDemo),
                visit_long = Marshal.GetFunctionPointerForDelegate(EngineSchemaVisitorCallbacks.VisitLongDemo),
                visit_integer = Marshal.GetFunctionPointerForDelegate(EngineSchemaVisitorCallbacks.VisitIntegerDemo),
                visit_short = Marshal.GetFunctionPointerForDelegate(EngineSchemaVisitorCallbacks.VisitShortDemo),
                visit_byte = Marshal.GetFunctionPointerForDelegate(EngineSchemaVisitorCallbacks.VisitByteDemo),
                visit_float = Marshal.GetFunctionPointerForDelegate(EngineSchemaVisitorCallbacks.VisitFloatDemo),
                visit_double = Marshal.GetFunctionPointerForDelegate(EngineSchemaVisitorCallbacks.VisitDoubleDemo),
                visit_boolean = Marshal.GetFunctionPointerForDelegate(EngineSchemaVisitorCallbacks.VisitBooleanDemo),
                visit_binary = Marshal.GetFunctionPointerForDelegate(EngineSchemaVisitorCallbacks.VisitBinaryDemo),
                visit_date = Marshal.GetFunctionPointerForDelegate(EngineSchemaVisitorCallbacks.VisitDateDemo),
                visit_timestamp = Marshal.GetFunctionPointerForDelegate(EngineSchemaVisitorCallbacks.VisitTimestampDemo),
                visit_timestamp_ntz = Marshal.GetFunctionPointerForDelegate(EngineSchemaVisitorCallbacks.VisitTimestampNtzDemo),
            };

            ulong schemaListId = FFI_NativeMethodsHandler.visit_schema(snapshot, &visitor);

            Console.WriteLine("Done building schema\n");

            Console.WriteLine("Schema:\n");
            PrintList(builder, (UIntPtr)schemaListId, 0, 0);
            Console.WriteLine("\n");

            // There's a high chance there's a memory leak because this isn't
            // rigorous as I'm convert C++ to C#.
            //
            // TODO: Stress test this entire handler and spot for memory leaks.
            //
            FreeBuilder(builder);
        }

        public unsafe void PrintList(
            SchemaBuilder builder,
            UIntPtr listId,
            int indent,
            int parentsOnLast
        )
        {
            SchemaItemList list = builder.lists[(int)listId];
            for (uint i = 0; i < list.len; i++)
            {
                bool isLast = i == list.len - 1;
                for (int j = 0; j < indent; j++)
                {
                    if ((indent - parentsOnLast) <= j)
                    {
                        // don't print a dangling | on any parents that are on their last item
                        Console.Write("   ");
                    }
                    else
                    {
                        Console.Write("│  ");
                    }
                }
                SchemaItem item = list.list[i];
                string prefix = isLast ? "└" : "├";
                string name = Marshal.PtrToStringAnsi((IntPtr)item.name);
                string type = Marshal.PtrToStringAnsi((IntPtr)item.type);

                Console.WriteLine($"{prefix}─ {name}: {type}");
                if (item.children != UIntPtr.MaxValue)
                {
                    PrintList(builder, item.children, indent + 1, parentsOnLast + (isLast ? 1 : 0));
                }
            }
        }

        private static unsafe void FreeBuilder(SchemaBuilder builder)
        {
          for (int i = 0; i < builder.list_count; i++)
          {
            SchemaItemList list = builder.lists[i];
            for (int j = 0; j < list.len; j++)
            {
              SchemaItem item = list.list[j];
              Marshal.FreeHGlobal((IntPtr)item.name);
              // Don't free item.Type, those are static strings
              //
              if (Marshal.PtrToStringAnsi((IntPtr)item.type).StartsWith("decimal"))
              {
                // Except decimal types, we allocated those
                //
                Marshal.FreeHGlobal((IntPtr)item.type);
              }
            }
            // Free all the items in this list (we allocated them together)
            //
            Marshal.FreeHGlobal((IntPtr)list.list);
          }
          Marshal.FreeHGlobal((IntPtr)builder.lists);
        }

        public unsafe PartitionList* GetPartitionList(SharedGlobalScanState* state)
        {
            Console.WriteLine("\nBuilding list of partition columns");
            int count = (int)FFI_NativeMethodsHandler.get_partition_column_count(state);

            PartitionList* list = (PartitionList*)Marshal.AllocHGlobal(sizeof(PartitionList));
            list->Len = 0; // We set the `len` to 0 here and use it to track how many items we've added to the list
            list->Cols = (char**)Marshal.AllocHGlobal(sizeof(char*) * count);

            StringSliceIterator* partIter = FFI_NativeMethodsHandler.get_partition_columns(state);

            for (;;)
            {
              bool hasNext = FFI_NativeMethodsHandler.string_slice_next(partIter, (void*)list, Marshal.GetFunctionPointerForDelegate(VisitCallbacks.VisitPartition));
              if (!hasNext)
              {
                Console.WriteLine("Done iterating partition columns");
                break;
              }
            }

            if (list->Len != count)
            {
              throw new InvalidOperationException("Error, partition iterator did not return get_partition_column_count columns");
            }

            if (list->Len > 0)
            {
              Console.WriteLine("\nPartition columns are:\n");
              for (int i = 0; i < list->Len; i++)
              {
                string col = Marshal.PtrToStringAnsi((IntPtr)list->Cols[i]);
                Console.WriteLine($"  - {col}");
              }
            }
            else
            {
              Console.WriteLine("Table has no partition columns");
            }
            Console.WriteLine();

            FFI_NativeMethodsHandler.free_string_slice_data(partIter);
            return list;
          }
  }
}
