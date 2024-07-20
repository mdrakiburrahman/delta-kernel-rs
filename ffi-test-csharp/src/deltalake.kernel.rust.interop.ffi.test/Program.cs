using Deltalake.Kernel.Rust.Interop.Ffi.Test.Callbacks.Errors;
using Deltalake.Kernel.Rust.Interop.Ffi.Test.Engines;
using DeltaLake.Kernel.Rust.Ffi;
using DeltaLake.Kernel.Rust.Interop.Ffi.Test.Extensions;
using System.Runtime.InteropServices;

namespace DeltaLake.Kernel.Rust.Interop.Ffi.Test
{
    public unsafe static class FfiTestConsoleApp
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentException("Usage: 'localtable/path'");
            }
            var localTablePath = args[0];
            bool isLocalTestPass = RunLocalTest(localTablePath);

            if (isLocalTestPass)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nTests succeeded :)\n");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nTest failed :(\n");
                Console.ResetColor();
                throw new InvalidOperationException("Test failed");
            }
        }

        private static bool RunLocalTest(string localTablePath)
        {
            Console.WriteLine($"Reading table at {localTablePath}");

            fixed (sbyte* tablePathPtr = localTablePath.ToSByte())
            {
                KernelStringSlice tablePathSlice = new KernelStringSlice
                {
                    ptr = tablePathPtr,
                    len = (nuint)localTablePath.Length
                };

                AllocateErrorFn callbackDelegate = AllocateErrorCallbacks.WarnAndThrowAllocateError;
                IntPtr callbackPointer = Marshal.GetFunctionPointerForDelegate(callbackDelegate);

                ExternResultHandleSharedExternEngine defaultEngineRes =
                    FFI_NativeMethodsHandler.get_default_engine(tablePathSlice, callbackPointer);
                ExternResultHandleSharedExternEngine syncEngineRes =
                    FFI_NativeMethodsHandler.get_sync_engine(callbackPointer);

                Console.WriteLine($"Executing with default engine");
                int defaultTestResult = TestEngines.LocalTestEngine(
                    tablePathSlice,
                    defaultEngineRes
                );

                Console.WriteLine($"Executing with sync engine");
                int syncTestResult = TestEngines.LocalTestEngine(tablePathSlice, syncEngineRes);

                return defaultTestResult == 0 && syncTestResult == 0;
            }
        }
    }
}
