// -----------------------------------------------------------------------------
// <copyright company="The Delta Lake Project Authors">
// Copyright (2024) The Delta Lake Project Authors.  All rights reserved.
// Licensed under the Apache license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------------

namespace DeltaLake.Kernel.Rust.Ffi;

public partial struct ArrowFFIData
{
    [NativeTypeName("struct FFI_ArrowArray")]
    public FFI_ArrowArray array;

    [NativeTypeName("struct FFI_ArrowSchema")]
    public FFI_ArrowSchema schema;
}
