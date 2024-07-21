using DeltaLake.Kernel.Rust.Ffi;

namespace Deltalake.Kernel.Rust.Interop.Ffi.Test.Schema.Context
{
  public unsafe struct Error
  {
    public EngineError Etype;
    public char* Msg;
  }
}
