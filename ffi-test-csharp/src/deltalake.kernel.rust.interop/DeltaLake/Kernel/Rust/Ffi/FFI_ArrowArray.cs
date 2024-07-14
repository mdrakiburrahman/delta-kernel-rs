// -----------------------------------------------------------------------------
// <copyright company="The Delta Lake Project Authors">
// Copyright (2024) The Delta Lake Project Authors.  All rights reserved.
// Licensed under the Apache license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------------

using System;

namespace DeltaLake.Kernel.Rust.Ffi;

public unsafe partial struct FFI_ArrowArray
{
    [NativeTypeName("int64_t")]
    public long length;

    [NativeTypeName("int64_t")]
    public long null_count;

    [NativeTypeName("int64_t")]
    public long offset;

    [NativeTypeName("int64_t")]
    public long n_buffers;

    [NativeTypeName("int64_t")]
    public long n_children;

    [NativeTypeName("const void **")]
    public void** buffers;

    [NativeTypeName("struct FFI_ArrowArray **")]
    public FFI_ArrowArray** children;

    [NativeTypeName("struct FFI_ArrowArray *")]
    public FFI_ArrowArray* dictionary;

    [NativeTypeName("void (*)(struct FFI_ArrowArray *)")]
    public IntPtr release;

    public void* private_data;
}
