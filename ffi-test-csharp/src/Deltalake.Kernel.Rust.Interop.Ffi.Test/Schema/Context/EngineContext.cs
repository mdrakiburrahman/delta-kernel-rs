using Deltalake.Kernel.Rust.Interop.Ffi.Test.Arrow.Properties;
using DeltaLake.Kernel.Rust.Ffi;

namespace Deltalake.Kernel.Rust.Interop.Ffi.Test.Schema.Context
{
  public unsafe struct EngineContext
  {
    public SharedGlobalScanState* GlobalState;
    public SharedSchema* ReadSchema;
    public char* TableRoot;
    public SharedExternEngine* Engine;
    public PartitionList* PartitionCols;
    public CStringMap* PartitionValues;
    public ArrowContext* ArrowContext;
  }
}
