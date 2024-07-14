// -----------------------------------------------------------------------------
// <copyright company="The Delta Lake Project Authors">
// Copyright (2024) The Delta Lake Project Authors.  All rights reserved.
// Licensed under the Apache license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------------

using System;

namespace DeltaLake.Kernel.Rust.Ffi;

public unsafe partial struct EngineSchemaVisitor
{
    public void* data;

    [NativeTypeName("uintptr_t (*)(void *, uintptr_t)")]
    public IntPtr make_field_list;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice, uintptr_t)")]
    public IntPtr visit_struct;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice, bool, uintptr_t)")]
    public IntPtr visit_array;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice, bool, uintptr_t)")]
    public IntPtr visit_map;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice, uint8_t, uint8_t)")]
    public IntPtr visit_decimal;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice)")]
    public IntPtr visit_string;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice)")]
    public IntPtr visit_long;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice)")]
    public IntPtr visit_integer;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice)")]
    public IntPtr visit_short;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice)")]
    public IntPtr visit_byte;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice)")]
    public IntPtr visit_float;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice)")]
    public IntPtr visit_double;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice)")]
    public IntPtr visit_boolean;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice)")]
    public IntPtr visit_binary;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice)")]
    public IntPtr visit_date;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice)")]
    public IntPtr visit_timestamp;

    [NativeTypeName("void (*)(void *, uintptr_t, struct KernelStringSlice)")]
    public IntPtr visit_timestamp_ntz;
}
