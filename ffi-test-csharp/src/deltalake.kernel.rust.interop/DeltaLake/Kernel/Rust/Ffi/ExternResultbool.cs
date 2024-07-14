using System.Runtime.InteropServices;

namespace DeltaLake.Kernel.Rust.Ffi;

public unsafe partial struct ExternResultbool
{
    public ExternResultbool_Tag tag;

    [NativeTypeName("__AnonymousRecord_delta_kernel_ffi_L521_C3")]
    public _Anonymous_e__Union Anonymous;

    public ref bool ok
    {
        get
        {
            return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref Anonymous.Anonymous1.ok, 1));
        }
    }

    public ref EngineError* err
    {
        get
        {
            return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous2.err;
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public unsafe partial struct _Anonymous_e__Union
    {
        [FieldOffset(0)]
        [NativeTypeName("__AnonymousRecord_delta_kernel_ffi_L522_C5")]
        public _Anonymous1_e__Struct Anonymous1;

        [FieldOffset(0)]
        [NativeTypeName("__AnonymousRecord_delta_kernel_ffi_L525_C5")]
        public _Anonymous2_e__Struct Anonymous2;

        public partial struct _Anonymous1_e__Struct
        {
            public bool ok;
        }

        public unsafe partial struct _Anonymous2_e__Struct
        {
            [NativeTypeName("struct EngineError *")]
            public EngineError* err;
        }
    }
}
