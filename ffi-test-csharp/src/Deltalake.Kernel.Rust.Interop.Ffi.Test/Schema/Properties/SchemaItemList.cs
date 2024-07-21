namespace Deltalake.Kernel.Rust.Interop.Ffi.Test.Schema.Properties;

public unsafe partial struct SchemaItemList
{
    public uint len;
    public SchemaItem* list;
}
