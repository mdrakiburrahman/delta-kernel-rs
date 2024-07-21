using Deltalake.Kernel.Rust.Interop.Ffi.Test.Schema.Context;
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
            CScanCallback callbackDelegate = VisitCallbackDemo;
            IntPtr callbackPtr = Marshal.GetFunctionPointerForDelegate(callbackDelegate);
            FFI_NativeMethodsHandler.visit_scan_data(
                engineData,
                selectionVec,
                engineContext,
                callbackPtr
            );
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
