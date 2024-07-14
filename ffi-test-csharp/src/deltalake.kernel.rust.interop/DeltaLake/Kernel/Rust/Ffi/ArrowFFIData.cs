namespace DeltaLake.Kernel.Rust.Ffi;

public partial struct ArrowFFIData
{
    [NativeTypeName("struct FFI_ArrowArray")]
    public FFI_ArrowArray array;

    [NativeTypeName("struct FFI_ArrowSchema")]
    public FFI_ArrowSchema schema;
}
