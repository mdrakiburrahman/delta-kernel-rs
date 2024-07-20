using Deltalake.Kernel.Rust.Interop.Ffi.Test.Callbacks.Delegates;
using DeltaLake.Kernel.Rust.Ffi;
using System.Runtime.InteropServices;
using static Deltalake.Kernel.Rust.Interop.Ffi.Test.Delegates.VisitDelegates;

namespace Deltalake.Kernel.Rust.Interop.Ffi.Test.Engines
{
    public unsafe static class TestEngines
    {
        public static int LocalTestEngine(
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
