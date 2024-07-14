namespace DeltaLake.Kernel.Rust.Ffi;

public unsafe partial struct EngineSchemaVisitor
{
    public void* data;

    [NativeTypeName("uintptr_t (*)(void *, uintptr_t)")]
    public delegate* unmanaged[Cdecl]<void*, nuint, nuint> make_field_list;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice, uintptr_t)")]
    public delegate* unmanaged[Cdecl]<void*, nuint, KernelStringSlice, nuint, void> visit_struct;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice, bool, uintptr_t)")]
    public delegate* unmanaged[Cdecl]<void*, nuint, KernelStringSlice, bool, nuint, void> visit_array;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice, bool, uintptr_t)")]
    public delegate* unmanaged[Cdecl]<void*, nuint, KernelStringSlice, bool, nuint, void> visit_map;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice, uint8_t, uint8_t)")]
    public delegate* unmanaged[Cdecl]<void*, nuint, KernelStringSlice, byte, byte, void> visit_decimal;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice)")]
    public delegate* unmanaged[Cdecl]<void*, nuint, KernelStringSlice, void> visit_string;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice)")]
    public delegate* unmanaged[Cdecl]<void*, nuint, KernelStringSlice, void> visit_long;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice)")]
    public delegate* unmanaged[Cdecl]<void*, nuint, KernelStringSlice, void> visit_integer;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice)")]
    public delegate* unmanaged[Cdecl]<void*, nuint, KernelStringSlice, void> visit_short;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice)")]
    public delegate* unmanaged[Cdecl]<void*, nuint, KernelStringSlice, void> visit_byte;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice)")]
    public delegate* unmanaged[Cdecl]<void*, nuint, KernelStringSlice, void> visit_float;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice)")]
    public delegate* unmanaged[Cdecl]<void*, nuint, KernelStringSlice, void> visit_double;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice)")]
    public delegate* unmanaged[Cdecl]<void*, nuint, KernelStringSlice, void> visit_boolean;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice)")]
    public delegate* unmanaged[Cdecl]<void*, nuint, KernelStringSlice, void> visit_binary;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice)")]
    public delegate* unmanaged[Cdecl]<void*, nuint, KernelStringSlice, void> visit_date;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice)")]
    public delegate* unmanaged[Cdecl]<void*, nuint, KernelStringSlice, void> visit_timestamp;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice)")]
    public delegate* unmanaged[Cdecl]<void*, nuint, KernelStringSlice, void> visit_timestamp_ntz;
}
