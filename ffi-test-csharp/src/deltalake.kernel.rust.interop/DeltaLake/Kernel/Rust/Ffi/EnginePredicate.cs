namespace DeltaLake.Kernel.Rust.Ffi;

public unsafe partial struct EnginePredicate
{
    public void* predicate;

    [NativeTypeName("uintptr_t (*)(void *, struct KernelExpressionVisitorState *)")]
    public delegate* unmanaged[Cdecl]<void*, KernelExpressionVisitorState*, nuint> visitor;
}
