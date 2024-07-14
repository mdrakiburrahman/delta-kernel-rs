using System.Runtime.InteropServices;

namespace DeltaLake.Kernel.Rust.Ffi;

public unsafe partial struct ExternResultArrowFFIData
{
    public ExternResultArrowFFIData_Tag tag;

    [NativeTypeName("__AnonymousRecord_delta_kernel_ffi_L701_C3")]
    public _Anonymous_e__Union Anonymous;

    public ref ArrowFFIData* ok
    {
        get
        {
            return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1)).Anonymous.Anonymous1.ok;
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
        [NativeTypeName("__AnonymousRecord_delta_kernel_ffi_L702_C5")]
        public _Anonymous1_e__Struct Anonymous1;

        [FieldOffset(0)]
        [NativeTypeName("__AnonymousRecord_delta_kernel_ffi_L705_C5")]
        public _Anonymous2_e__Struct Anonymous2;

        public unsafe partial struct _Anonymous1_e__Struct
        {
            [NativeTypeName("struct ArrowFFIData *")]
            public ArrowFFIData* ok;
        }

        public unsafe partial struct _Anonymous2_e__Struct
        {
            [NativeTypeName("struct EngineError *")]
            public EngineError* err;
        }
    }
}
