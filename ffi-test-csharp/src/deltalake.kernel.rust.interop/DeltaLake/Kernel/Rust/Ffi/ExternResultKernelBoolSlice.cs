using System.Runtime.InteropServices;

namespace DeltaLake.Kernel.Rust.Ffi;

public unsafe partial struct ExternResultKernelBoolSlice
{
    public ExternResultKernelBoolSlice_Tag tag;

    [NativeTypeName("__AnonymousRecord_delta_kernel_ffi_L879_C3")]
    public _Anonymous_e__Union Anonymous;

    public ref KernelBoolSlice ok
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
        [NativeTypeName("__AnonymousRecord_delta_kernel_ffi_L880_C5")]
        public _Anonymous1_e__Struct Anonymous1;

        [FieldOffset(0)]
        [NativeTypeName("__AnonymousRecord_delta_kernel_ffi_L883_C5")]
        public _Anonymous2_e__Struct Anonymous2;

        public partial struct _Anonymous1_e__Struct
        {
            [NativeTypeName("struct KernelBoolSlice")]
            public KernelBoolSlice ok;
        }

        public unsafe partial struct _Anonymous2_e__Struct
        {
            [NativeTypeName("struct EngineError *")]
            public EngineError* err;
        }
    }
}
