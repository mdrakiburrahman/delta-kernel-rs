namespace DeltaLake.Kernel.Rust.Ffi;

public unsafe partial struct FFI_ArrowSchema
{
    [NativeTypeName("const char *")]
    public sbyte* format;

    [NativeTypeName("const char *")]
    public sbyte* name;

    [NativeTypeName("const char *")]
    public sbyte* metadata;

    [NativeTypeName("int64_t")]
    public long flags;

    [NativeTypeName("int64_t")]
    public long n_children;

    [NativeTypeName("struct FFI_ArrowSchema **")]
    public FFI_ArrowSchema** children;

    [NativeTypeName("struct FFI_ArrowSchema *")]
    public FFI_ArrowSchema* dictionary;

    [NativeTypeName("void (*)(struct FFI_ArrowSchema *)")]
    public delegate* unmanaged[Cdecl]<FFI_ArrowSchema*, void> release;

    public void* private_data;
}
