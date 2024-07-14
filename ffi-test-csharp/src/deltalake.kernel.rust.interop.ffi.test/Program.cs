using Deltalake.Kernel.Rust.Interop.Ffi.Test.Callbacks.Errors;
using DeltaLake.Kernel.Rust.Ffi;
using DeltaLake.Kernel.Rust.Interop.Ffi.Test.Extensions;
using System.Runtime.InteropServices;

namespace DeltaLake.Kernel.Rust.Interop.Ffi.Test
{
    public unsafe class FfiTestConsoleApp
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentException("Usage: table/path");
            }
            var tablePath = args[0];

            Console.WriteLine($"Reading table at {tablePath}");

            fixed (sbyte* tablePathPtr = tablePath.ToSByte())
            {
                KernelStringSlice tablePathSlice = new KernelStringSlice
                {
                    ptr = tablePathPtr,
                    len = (nuint)tablePath.Length
                };

                AllocateErrorFn callbackDelegate = AllocateErrorCallbacks.WarnAndThrowAllocateError;
                IntPtr callbackPointer = Marshal.GetFunctionPointerForDelegate(callbackDelegate);

                ExternResultHandleSharedExternEngine defaultEngineRes =
                    FFI_NativeMethodsHandler.get_default_engine(tablePathSlice, callbackPointer);
                ExternResultHandleSharedExternEngine syncEngineRes =
                    FFI_NativeMethodsHandler.get_sync_engine(callbackPointer);
            }
        }
    }
}
