using Deltalake.Kernel.Rust.Interop.Ffi.Test.Schema.Context;
using Deltalake.Kernel.Rust.Interop.Ffi.Test.Schema.Diagnostics;
using DeltaLake.Kernel.Rust.Ffi;
using System.Runtime.InteropServices;

namespace Deltalake.Kernel.Rust.Interop.Ffi.Test.Callbacks.Visit
{
    public static class VisitCallbacks
    {
        public static unsafe void VisitCallbackDemo(
            void* engine_context,
            KernelStringSlice path,
            long size,
            Stats* stats,
            DvInfo* dv_info,
            CStringMap* partition_map
        )
        {
            string pathStr = new string(path.ptr, 0, (int)path.len, System.Text.Encoding.UTF8);
            string message = $"file: {pathStr} (size: {size}, num_records:";
            if (stats != null)
            {
                message += $"{stats->num_records})";
            }
            else
            {
                message += " [no stats])";
            }
            Console.WriteLine(message);
        }

        public static unsafe void VisitDataDemo(
            void* engineContext,
            ExclusiveEngineData* engineData,
            KernelBoolSlice selectionVec
        )
        {
            Console.WriteLine("\nScan iterator found some data to read.\nOf this data, here is a selection vector:\n");
            VisitPrinter.PrintSelectionVector("\t", selectionVec);

            Console.WriteLine("\nAsking kernel to call us back for each scan row (file to read)\n");
            FFI_NativeMethodsHandler.visit_scan_data(
                engineData,
                selectionVec,
                engineContext,
                Marshal.GetFunctionPointerForDelegate(VisitCallbackDemo)
            );
            FFI_NativeMethodsHandler.free_bool_slice(selectionVec);
    }

        public static unsafe void VisitPartition(void* context, KernelStringSlice partition)
        {
            PartitionList* list = (PartitionList*)context;
            char* col = (char*)StringAllocator.AllocateString(partition);
            list->Cols[list->Len] = col;
            list->Len++;
        }
    }
}
