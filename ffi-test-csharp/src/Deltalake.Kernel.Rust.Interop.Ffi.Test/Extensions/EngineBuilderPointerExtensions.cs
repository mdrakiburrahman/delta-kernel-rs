using DeltaLake.Kernel.Rust.Ffi;
using DeltaLake.Kernel.Rust.Interop.Ffi.Test.Extensions;

namespace Deltalake.Kernel.Rust.Interop.Ffi.Test.Extensions
{
    public static class EngineBuilderPointerMethods
    {
        public unsafe static void WithBuilderOption(
            EngineBuilder* engineBuilder,
            string key,
            string value
        )
        {
            fixed (sbyte* keyPtr = key.ToSByte(), valuePtr = value.ToSByte())
            {
                KernelStringSlice keySlice = new KernelStringSlice { ptr = keyPtr, len = (nuint)key.Length};
                KernelStringSlice valueSlice = new KernelStringSlice { ptr = valuePtr, len = (nuint)value.Length };
                FFI_NativeMethodsHandler.set_builder_option(engineBuilder, keySlice, valueSlice);
            }
        }
    }
}
