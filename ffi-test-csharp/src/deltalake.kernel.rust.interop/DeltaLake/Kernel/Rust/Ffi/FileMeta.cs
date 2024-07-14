namespace DeltaLake.Kernel.Rust.Ffi;

public partial struct FileMeta
{
    [NativeTypeName("struct KernelStringSlice")]
    public KernelStringSlice path;

    [NativeTypeName("int64_t")]
    public long last_modified;

    [NativeTypeName("uintptr_t")]
    public nuint size;
}
