using Deltalake.Kernel.Rust.Interop.Ffi.Test.Callbacks.Visit;
using Deltalake.Kernel.Rust.Interop.Ffi.Test.Schema.Context;
using Deltalake.Kernel.Rust.Interop.Ffi.Test.Schema.Handlers;
using DeltaLake.Kernel.Rust.Ffi;
using System.Runtime.InteropServices;
using static Deltalake.Kernel.Rust.Interop.Ffi.Test.Delegates.Visit.VisitDelegates;

namespace Deltalake.Kernel.Rust.Interop.Ffi.Test.Engines.Test
{
  public unsafe static class TestEngines
  {
    public static int TestWithEngine(
        ExternResultHandleSharedExternEngine engineRes,
        KernelStringSlice tablePathSlice
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

      ExternResultHandleSharedSnapshot snapshotRes = FFI_NativeMethodsHandler.snapshot(tablePathSlice, engine);
      if (snapshotRes.tag != ExternResultHandleSharedSnapshot_Tag.OkHandleSharedSnapshot)
      {
        Console.WriteLine("Failed to create snapshot");
        return -1;
      }

      SharedSnapshot* snapshot = snapshotRes.Anonymous.Anonymous1.ok;
      ulong v = FFI_NativeMethodsHandler.version(snapshot);

      Console.WriteLine($"version: {v}");

      ISchemaHandler schemaHandler = new SchemaHandlerDemo();
      schemaHandler.PrintSchema(snapshot);

      string tableRoot = Marshal.PtrToStringAnsi((nint)FFI_NativeMethodsHandler.snapshot_table_root(snapshot, Marshal.GetFunctionPointerForDelegate(StringAllocator.AllocateString)));
      Console.WriteLine($"Table root: {tableRoot}");

      Console.WriteLine("Starting table scan");

      ExternResultHandleSharedScan scanRes = FFI_NativeMethodsHandler.scan(snapshot, engine, null);
      if (scanRes.tag != ExternResultHandleSharedScan_Tag.OkHandleSharedScan)
      {
        Console.WriteLine("Failed to create scan");
        return -1;
      }

      SharedScan* scan = scanRes.Anonymous.Anonymous1.ok;
      SharedGlobalScanState* globalState = FFI_NativeMethodsHandler.get_global_scan_state(scan);
      SharedSchema* readSchema = FFI_NativeMethodsHandler.get_global_read_schema(globalState);
      PartitionList* partitionCols = schemaHandler.GetPartitionList(globalState);

      EngineContext context = new EngineContext
      {
        GlobalState = globalState,
        ReadSchema = readSchema,
        TableRoot = (char*)Marshal.StringToHGlobalAnsi(tableRoot),
        Engine = engine,
        PartitionCols = partitionCols,
        PartitionValues = null
      };

      ExternResultHandleSharedScanDataIterator dataIterRes = FFI_NativeMethodsHandler.kernel_scan_data_init(engine, scan);
      if (
          dataIterRes.tag
          != ExternResultHandleSharedScanDataIterator_Tag.OkHandleSharedScanDataIterator
      )
      {
        Console.WriteLine("Failed to construct scan data iterator");
        return -1;
      }

      SharedScanDataIterator* dataIter = dataIterRes.Anonymous.Anonymous1.ok;

      VisitDataDelegate callbackDelegate = VisitCallbacks.VisitDataDemo;
      nint callbackPointer = Marshal.GetFunctionPointerForDelegate(callbackDelegate);

      Console.WriteLine("\nIterating scan data\n");
      for (; ; )
      {
        ExternResultbool okRes = FFI_NativeMethodsHandler.kernel_scan_data_next(
            dataIter,
            &context,
            callbackPointer
        );
        if (okRes.tag != ExternResultbool_Tag.Okbool)
        {
          Console.WriteLine("Failed to iterate scan data");
          return -1;
        }
        else if (!okRes.Anonymous.Anonymous1.ok)
        {
          Console.WriteLine("Scan data iterator done\n");
          break;
        }
      }

      Console.WriteLine("All done reading table data\n");

      FFI_NativeMethodsHandler.free_scan(scan);
      FFI_NativeMethodsHandler.free_snapshot(snapshot);
      FFI_NativeMethodsHandler.free_engine(engine);
      return 0;
    }
  }
}
