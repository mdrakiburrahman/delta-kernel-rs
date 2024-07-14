namespace DeltaLake.Kernel.Rust.Ffi;

public unsafe partial struct KernelBoolSlice
{
    public bool* ptr;

    [NativeTypeName("uintptr_t")]
    public nuint len;
}
