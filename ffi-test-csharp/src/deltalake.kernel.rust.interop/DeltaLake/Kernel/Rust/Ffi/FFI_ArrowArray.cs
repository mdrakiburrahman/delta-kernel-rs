namespace DeltaLake.Kernel.Rust.Ffi;

public unsafe partial struct FFI_ArrowArray
{
    [NativeTypeName("int64_t")]
    public long length;

    [NativeTypeName("int64_t")]
    public long null_count;

    [NativeTypeName("int64_t")]
    public long offset;

    [NativeTypeName("int64_t")]
    public long n_buffers;

    [NativeTypeName("int64_t")]
    public long n_children;

    [NativeTypeName("const void **")]
    public void** buffers;

    [NativeTypeName("struct FFI_ArrowArray **")]
    public FFI_ArrowArray** children;

    [NativeTypeName("struct FFI_ArrowArray *")]
    public FFI_ArrowArray* dictionary;

    [NativeTypeName("void (*)(struct FFI_ArrowArray *)")]
    public delegate* unmanaged[Cdecl]<FFI_ArrowArray*, void> release;

    public void* private_data;
}
