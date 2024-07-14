namespace DeltaLake.Kernel.Rust.Ffi;

public unsafe partial struct KernelStringSlice
{
    [NativeTypeName("const char *")]
    public sbyte* ptr;

    [NativeTypeName("uintptr_t")]
    public nuint len;
}
