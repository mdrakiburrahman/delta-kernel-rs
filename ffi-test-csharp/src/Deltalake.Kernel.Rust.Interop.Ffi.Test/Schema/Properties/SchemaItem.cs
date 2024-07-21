namespace Deltalake.Kernel.Rust.Interop.Ffi.Test.Schema.Properties;

public unsafe partial struct SchemaItem
{
    public sbyte* name;
    public sbyte* type;
    public UIntPtr children;
}
