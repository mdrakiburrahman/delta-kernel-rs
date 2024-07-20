// -----------------------------------------------------------------------------
// <copyright company="The Delta Lake Project Authors">
// Copyright (2024) The Delta Lake Project Authors.  All rights reserved.
// Licensed under the Apache license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------------

using System.Runtime.InteropServices;

namespace DeltaLake.Kernel.Rust.Ffi;

public unsafe partial struct ExternResultHandleExclusiveFileReadResultIterator
{
    public ExternResultHandleExclusiveFileReadResultIterator_Tag tag;

    [NativeTypeName("__AnonymousRecord_delta_kernel_ffi_L581_C3")]
    public _Anonymous_e__Union Anonymous;

    [StructLayout(LayoutKind.Explicit)]
    public unsafe partial struct _Anonymous_e__Union
    {
        [FieldOffset(0)]
        [NativeTypeName("__AnonymousRecord_delta_kernel_ffi_L582_C5")]
        public _Anonymous1_e__Struct Anonymous1;

        [FieldOffset(0)]
        [NativeTypeName("__AnonymousRecord_delta_kernel_ffi_L585_C5")]
        public _Anonymous2_e__Struct Anonymous2;

        public unsafe partial struct _Anonymous1_e__Struct
        {
            [NativeTypeName("HandleExclusiveFileReadResultIterator")]
            public ExclusiveFileReadResultIterator* ok;
        }

        public unsafe partial struct _Anonymous2_e__Struct
        {
            [NativeTypeName("struct EngineError *")]
            public EngineError* err;
        }
    }
}
