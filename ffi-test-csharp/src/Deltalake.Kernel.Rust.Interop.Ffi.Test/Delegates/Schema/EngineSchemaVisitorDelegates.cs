using DeltaLake.Kernel.Rust.Ffi;
using System.Runtime.InteropServices;

namespace Deltalake.Kernel.Rust.Interop.Ffi.Test.Delegates.Schema
{
    public static class EngineSchemaVisitorDelegates
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate UIntPtr MakeFieldListDelegate(void* data, UIntPtr reserve);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void VisitStructDelegate(
            void* data,
            UIntPtr siblingListId,
            KernelStringSlice name,
            UIntPtr childListId
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void VisitArrayDelegate(
            void* data,
            UIntPtr siblingListId,
            KernelStringSlice name,
            bool containsNull,
            UIntPtr childListId
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void VisitMapDelegate(
            void* data,
            UIntPtr siblingListId,
            KernelStringSlice name,
            bool valueContainsNull,
            UIntPtr childListId
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void VisitDecimalDelegate(
            void* data,
            UIntPtr siblingListId,
            KernelStringSlice name,
            byte precision,
            byte scale
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void VisitStringDelegate(
            void* data,
            UIntPtr siblingListId,
            KernelStringSlice name
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void VisitLongDelegate(
            void* data,
            UIntPtr siblingListId,
            KernelStringSlice name
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void VisitIntegerDelegate(
            void* data,
            UIntPtr siblingListId,
            KernelStringSlice name
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void VisitShortDelegate(
            void* data,
            UIntPtr siblingListId,
            KernelStringSlice name
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void VisitByteDelegate(
            void* data,
            UIntPtr siblingListId,
            KernelStringSlice name
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void VisitFloatDelegate(
            void* data,
            UIntPtr siblingListId,
            KernelStringSlice name
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void VisitDoubleDelegate(
            void* data,
            UIntPtr siblingListId,
            KernelStringSlice name
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void VisitBooleanDelegate(
            void* data,
            UIntPtr siblingListId,
            KernelStringSlice name
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void VisitDateDelegate(
            void* data,
            UIntPtr siblingListId,
            KernelStringSlice name
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void VisitTimestampDelegate(
            void* data,
            UIntPtr siblingListId,
            KernelStringSlice name
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void VisitTimestampNtzDelegate(
            void* data,
            UIntPtr siblingListId,
            KernelStringSlice name
        );
    }
}
