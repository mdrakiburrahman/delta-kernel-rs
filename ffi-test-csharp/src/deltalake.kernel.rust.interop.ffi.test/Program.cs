using Azure.Core;
using Azure.Identity;
using Deltalake.Kernel.Rust.Interop.Ffi.Test.Callbacks.Errors;
using Deltalake.Kernel.Rust.Interop.Ffi.Test.Engines.Test;
using Deltalake.Kernel.Rust.Interop.Ffi.Test.Extensions;
using DeltaLake.Kernel.Rust.Ffi;
using DeltaLake.Kernel.Rust.Interop.Ffi.Test.Extensions;
using System.Runtime.InteropServices;

namespace DeltaLake.Kernel.Rust.Interop.Ffi.Test
{
  public unsafe static class FfiTestConsoleApp
    {
        public static void Main(string[] args)
        {
            if (args.Length < 3)
                throw new ArgumentException(
                    "Usage: 'Drive:\\folder\\table' 'abfss://container@storage.dfs.core.windows.net/table/' '2'"
                );

            var localTablePath = args[0];
            var remoteTablePath = args[1];
            int numLoops = int.Parse(args[2]);

            var adlsOauthToken = new VisualStudioCredential().GetToken(new TokenRequestContext(new[] { "https://storage.azure.com/.default" }), default).Token;

            for (int i = 0; i < numLoops; i++)
            {
                Console.WriteLine("\n=================\n");
                Console.WriteLine($"Loop {i + 1} of {numLoops}");
                Console.WriteLine("\n=================\n");

                bool isLocalTestPass = RunLocalTest(localTablePath);
                bool isAdlsTestPass = RunAdlsTest(remoteTablePath, adlsOauthToken);

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
            }

        private static bool RunAdlsTest(string adlsTablePath, string adlsOauthToken)
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
                  Console.WriteLine("Could not get engine builder");
                  return false;
                }
                EngineBuilder* engineBuilder = engineBuilderRes.Anonymous.Anonymous1.ok;
                EngineBuilderPointerMethods.WithBuilderOption(engineBuilder, "bearer_token", adlsOauthToken);

                ExternResultHandleSharedExternEngine engineRes = FFI_NativeMethodsHandler.builder_build(engineBuilder);
                int adlsTestResult = TestEngines.TestWithEngine(engineRes, tablePathSlice);

                return adlsTestResult == 0;
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
                int defaultTestResult = TestEngines.TestWithEngine(defaultEngineRes, tablePathSlice);

                Console.WriteLine($"Executing with sync engine");
                int syncTestResult = TestEngines.TestWithEngine(syncEngineRes, tablePathSlice);

                return defaultTestResult == 0 && syncTestResult == 0;
            }
        }
    }
}
