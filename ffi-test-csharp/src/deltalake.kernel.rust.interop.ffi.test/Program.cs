using Deltalake.Kernel.Rust.Interop.Ffi.Test.Callbacks.Delegates;
using Deltalake.Kernel.Rust.Interop.Ffi.Test.Callbacks.Errors;
using DeltaLake.Kernel.Rust.Ffi;
using DeltaLake.Kernel.Rust.Interop.Ffi.Test.Extensions;
using System.Runtime.InteropServices;
using static Deltalake.Kernel.Rust.Interop.Ffi.Test.Delegates.VisitDelegates;

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

                Console.WriteLine($"Executing with default engine");
                int defaultTestResult = TestEngine(tablePathSlice, defaultEngineRes);

                Console.WriteLine($"Executing with sync engine");
                int syncTestResult = TestEngine(tablePathSlice, syncEngineRes);

                if (defaultTestResult != 0 || syncTestResult != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nTest failed :(\n");
                    Console.ResetColor();
                    throw new InvalidOperationException("Test failed");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nTests succeeded :)\n");
                    Console.ResetColor();
                }
            }
        }

        public static int TestEngine(
            KernelStringSlice tablePathSlice,
            ExternResultHandleSharedExternEngine engineRes
        )
        {
            if (
                engineRes.tag != ExternResultHandleSharedExternEngine_Tag.OkHandleSharedExternEngine
            )
            {
                Console.WriteLine("Failed to get engine");
                return -1;
            }

            SharedExternEngine* engine = engineRes.Anonymous.Anonymous1.ok;

            ExternResultHandleSharedSnapshot snapshotRes = FFI_NativeMethodsHandler.snapshot(
                tablePathSlice,
                engine
            );
            if (snapshotRes.tag != ExternResultHandleSharedSnapshot_Tag.OkHandleSharedSnapshot)
            {
                Console.WriteLine("Failed to create snapshot");
                return -1;
            }

            SharedSnapshot* snapshot = snapshotRes.Anonymous.Anonymous1.ok;

            ulong v = FFI_NativeMethodsHandler.version(snapshot);
            Console.WriteLine($"version: {v}");
            ExternResultHandleSharedScan scanRes = FFI_NativeMethodsHandler.scan(
                snapshot,
                engine,
                null
            );
            if (scanRes.tag != ExternResultHandleSharedScan_Tag.OkHandleSharedScan)
            {
                Console.WriteLine("Failed to create scan");
                return -1;
            }

            SharedScan* scan = scanRes.Anonymous.Anonymous1.ok;

            ExternResultHandleSharedScanDataIterator dataIterRes =
                FFI_NativeMethodsHandler.kernel_scan_data_init(engine, scan);
            if (
                dataIterRes.tag
                != ExternResultHandleSharedScanDataIterator_Tag.OkHandleSharedScanDataIterator
            )
            {
                Console.WriteLine("Failed to construct scan data iterator");
                return -1;
            }

            SharedScanDataIterator* dataIter = dataIterRes.Anonymous.Anonymous1.ok;

            VisitDataDelegate callbackDelegate = VisitCallbacks.VisitData;
            IntPtr callbackPointer = Marshal.GetFunctionPointerForDelegate(callbackDelegate);

            // Iterate scanned files
            //
            for (; ; )
            {
                ExternResultbool okRes = FFI_NativeMethodsHandler.kernel_scan_data_next(
                    dataIter,
                    null,
                    callbackPointer
                );
                if (okRes.tag != ExternResultbool_Tag.Okbool)
                {
                    Console.WriteLine("Failed to iterate scan data");
                    return -1;
                }
                else if (!okRes.Anonymous.Anonymous1.ok)
                {
                    break;
                }
            }

            FFI_NativeMethodsHandler.free_scan(scan);
            FFI_NativeMethodsHandler.free_snapshot(snapshot);
            FFI_NativeMethodsHandler.free_engine(engine);
            return 0;
        }
    }
}
