using DeltaLake.Kernel.Rust.Ffi;
using System.Runtime.InteropServices;

namespace Deltalake.Kernel.Rust.Interop.Ffi.Test.Callbacks.Errors
{
  public struct ReadError
    {
        public EngineError etype;
        public IntPtr msg;

        public string Message
        {
            get => Marshal.PtrToStringAnsi(msg);
            set => msg = Marshal.StringToHGlobalAnsi(value);
        }
    }
}
