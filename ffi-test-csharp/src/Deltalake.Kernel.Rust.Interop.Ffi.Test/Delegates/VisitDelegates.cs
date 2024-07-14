using DeltaLake.Kernel.Rust.Ffi;
using System.Runtime.InteropServices;

namespace Deltalake.Kernel.Rust.Interop.Ffi.Test.Delegates
{
    public static class VisitDelegates
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void VisitDataDelegate(
            void* engineContext,
            ExclusiveEngineData* engineData,
            KernelBoolSlice selectionVec
        );
    }
}
