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
            if (args.Length < 2)
                throw new ArgumentException(
                    "Usage: 'Drive:\\folder\\table' 'abfss://container@storage.dfs.core.windows.net/table'"
                );

            var localTablePath = args[0];
            var remoteTablePath = args[1];

            bool isLocalTestPass = RunLocalTest(localTablePath);
            bool isAdlsTestPass = RunAdlsTest(remoteTablePath);

            if (isLocalTestPass && isAdlsTestPass)
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

        private static bool RunAdlsTest(string adlsTablePath)
        {
            Console.WriteLine($"Reading Azure Data Lake Storage table at {adlsTablePath}");

            fixed (sbyte* tablePathPtr = adlsTablePath.ToSByte())
            {
                KernelStringSlice tablePathSlice = new KernelStringSlice
                {
                    ptr = tablePathPtr,
                    len = (nuint)adlsTablePath.Length
                };

                AllocateErrorFn callbackDelegate = AllocateErrorCallbacks.WarnAndThrowAllocateError;
                IntPtr callbackPointer = Marshal.GetFunctionPointerForDelegate(callbackDelegate);

                ExternResultEngineBuilder engineBuilderRes = FFI_NativeMethodsHandler.get_engine_builder(tablePathSlice, callbackPointer);
                if (engineBuilderRes.tag != ExternResultEngineBuilder_Tag.OkEngineBuilder)
                {
                  Console.WriteLine("Failed to get engine builder");
                  return false;
                }
                EngineBuilder* engineBuilder = engineBuilderRes.Anonymous.Anonymous1.ok;

                return true;
            }
        }

        private static bool RunLocalTest(string localTablePath)
        {
            Console.WriteLine($"Reading local table at {localTablePath}");

            fixed (sbyte* tablePathPtr = localTablePath.ToSByte())
            {
                KernelStringSlice tablePathSlice = new KernelStringSlice
                {
                    ptr = tablePathPtr,
                    len = (nuint)localTablePath.Length
                };

                AllocateErrorFn callbackDelegate = AllocateErrorCallbacks.WarnAndThrowAllocateError;
                IntPtr callbackPointer = Marshal.GetFunctionPointerForDelegate(callbackDelegate);

                ExternResultHandleSharedExternEngine defaultEngineRes = FFI_NativeMethodsHandler.get_default_engine(tablePathSlice, callbackPointer);
                ExternResultHandleSharedExternEngine syncEngineRes = FFI_NativeMethodsHandler.get_sync_engine(callbackPointer);

                if (defaultEngineRes.tag != ExternResultHandleSharedExternEngine_Tag.OkHandleSharedExternEngine
                    || syncEngineRes.tag != ExternResultHandleSharedExternEngine_Tag.OkHandleSharedExternEngine
                )
                {
                  Console.WriteLine("Failed to get one or more engines");
                  return false;
                }

                Console.WriteLine($"Executing with default engine");
                int defaultTestResult = TestEngines.TestWithEngineLocally(defaultEngineRes, tablePathSlice);

                Console.WriteLine($"Executing with sync engine");
                int syncTestResult = TestEngines.TestWithEngineLocally(syncEngineRes, tablePathSlice);

                return defaultTestResult == 0 && syncTestResult == 0;
            }
        }
    }
}
