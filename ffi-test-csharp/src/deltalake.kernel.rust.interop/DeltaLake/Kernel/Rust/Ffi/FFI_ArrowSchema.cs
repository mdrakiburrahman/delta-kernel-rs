// -----------------------------------------------------------------------------
// <copyright company="The Delta Lake Project Authors">
// Copyright (2024) The Delta Lake Project Authors.  All rights reserved.
// Licensed under the Apache license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------------

using System;

namespace DeltaLake.Kernel.Rust.Ffi;

public unsafe partial struct FFI_ArrowSchema
{
    [NativeTypeName("const char *")]
    public sbyte* format;

    [NativeTypeName("const char *")]
    public sbyte* name;

    [NativeTypeName("const char *")]
    public sbyte* metadata;

    [NativeTypeName("int64_t")]
    public long flags;

    [NativeTypeName("int64_t")]
    public long n_children;

    [NativeTypeName("struct FFI_ArrowSchema **")]
    public FFI_ArrowSchema** children;

    [NativeTypeName("struct FFI_ArrowSchema *")]
    public FFI_ArrowSchema* dictionary;

    [NativeTypeName("void (*)(struct FFI_ArrowSchema *)")]
    public IntPtr release;

    public void* private_data;
}
