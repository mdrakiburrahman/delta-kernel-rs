namespace DeltaLake.Kernel.Rust.Ffi;

public unsafe partial struct EngineIterator
{
    public void* data;

    [NativeTypeName("const void *(*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, void*> get_next;
}
