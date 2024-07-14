using DeltaLake.Kernel.Rust.Ffi;
using System.Runtime.InteropServices;

namespace Deltalake.Kernel.Rust.Interop.Ffi.Test.Callbacks.Errors
{
  public class AllocateErrorCallbacks
    {
        public static unsafe EngineError* WarnAndThrowAllocateError(
            KernelError etype,
            KernelStringSlice msg
        )
        {
            string message = Marshal.PtrToStringAnsi((IntPtr)msg.ptr);
            Console.WriteLine($"Error occurred: {message}");
            throw new InvalidOperationException($"Engine error of type {etype}: {message}");
        }
    }
}
