// -----------------------------------------------------------------------------
// <copyright company="The Delta Lake Project Authors">
// Copyright (2024) The Delta Lake Project Authors.  All rights reserved.
// Licensed under the Apache license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------------

namespace DeltaLake.Kernel.Rust.Ffi;

public enum KernelError
{
    UnknownError,
    FFIError,
    ArrowError,
    EngineDataTypeError,
    ExtractError,
    GenericError,
    IOErrorError,
    ParquetError,
    ObjectStoreError,
    ObjectStorePathError,
    ReqwestError,
    FileNotFoundError,
    MissingColumnError,
    UnexpectedColumnTypeError,
    MissingDataError,
    MissingVersionError,
    DeletionVectorError,
    InvalidUrlError,
    MalformedJsonError,
    MissingMetadataError,
    MissingProtocolError,
    MissingMetadataAndProtocolError,
    ParseError,
    JoinFailureError,
    Utf8Error,
    ParseIntError,
    InvalidColumnMappingModeError,
    InvalidTableLocationError,
    InvalidDecimalError,
    InvalidStructDataError,
}
