// -----------------------------------------------------------------------------
// <copyright company="The Delta Lake Project Authors">
// Copyright (2024) The Delta Lake Project Authors.  All rights reserved.
// Licensed under the Apache license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;

namespace DeltaLake.Kernel.Rust.Ffi;

public static unsafe partial class FFI_NativeMethodsHandler
{
    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?free_bool_slice@@YAXUKernelBoolSlice@@@Z", ExactSpelling = true)]
    public static extern void free_bool_slice([NativeTypeName("struct KernelBoolSlice")] KernelBoolSlice slice);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?free_engine_data@@YAXPEAUExclusiveEngineData@@@Z", ExactSpelling = true)]
    public static extern void free_engine_data([NativeTypeName("HandleExclusiveEngineData")] ExclusiveEngineData* engine_data);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?get_engine_builder@@YA?AUExternResultEngineBuilder@@UKernelStringSlice@@P6APEAUEngineError@@W4KernelError@@0@Z@Z", ExactSpelling = true)]
    [return: NativeTypeName("struct ExternResultEngineBuilder")]
    public static extern ExternResultEngineBuilder get_engine_builder([NativeTypeName("struct KernelStringSlice")] KernelStringSlice path, [NativeTypeName("AllocateErrorFn")] IntPtr allocate_error);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?set_builder_option@@YAXPEAUEngineBuilder@@UKernelStringSlice@@1@Z", ExactSpelling = true)]
    public static extern void set_builder_option([NativeTypeName("struct EngineBuilder *")] EngineBuilder* builder, [NativeTypeName("struct KernelStringSlice")] KernelStringSlice key, [NativeTypeName("struct KernelStringSlice")] KernelStringSlice value);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?builder_build@@YA?AUExternResultHandleSharedExternEngine@@PEAUEngineBuilder@@@Z", ExactSpelling = true)]
    [return: NativeTypeName("struct ExternResultHandleSharedExternEngine")]
    public static extern ExternResultHandleSharedExternEngine builder_build([NativeTypeName("struct EngineBuilder *")] EngineBuilder* builder);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?get_default_engine@@YA?AUExternResultHandleSharedExternEngine@@UKernelStringSlice@@P6APEAUEngineError@@W4KernelError@@0@Z@Z", ExactSpelling = true)]
    [return: NativeTypeName("struct ExternResultHandleSharedExternEngine")]
    public static extern ExternResultHandleSharedExternEngine get_default_engine([NativeTypeName("struct KernelStringSlice")] KernelStringSlice path, [NativeTypeName("AllocateErrorFn")] IntPtr allocate_error);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?get_sync_engine@@YA?AUExternResultHandleSharedExternEngine@@P6APEAUEngineError@@W4KernelError@@UKernelStringSlice@@@Z@Z", ExactSpelling = true)]
    [return: NativeTypeName("struct ExternResultHandleSharedExternEngine")]
    public static extern ExternResultHandleSharedExternEngine get_sync_engine([NativeTypeName("AllocateErrorFn")] IntPtr allocate_error);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?free_engine@@YAXPEAUSharedExternEngine@@@Z", ExactSpelling = true)]
    public static extern void free_engine([NativeTypeName("HandleSharedExternEngine")] SharedExternEngine* engine);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?snapshot@@YA?AUExternResultHandleSharedSnapshot@@UKernelStringSlice@@PEAUSharedExternEngine@@@Z", ExactSpelling = true)]
    [return: NativeTypeName("struct ExternResultHandleSharedSnapshot")]
    public static extern ExternResultHandleSharedSnapshot snapshot([NativeTypeName("struct KernelStringSlice")] KernelStringSlice path, [NativeTypeName("HandleSharedExternEngine")] SharedExternEngine* engine);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?free_snapshot@@YAXPEAUSharedSnapshot@@@Z", ExactSpelling = true)]
    public static extern void free_snapshot([NativeTypeName("HandleSharedSnapshot")] SharedSnapshot* snapshot);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?version@@YA_KPEAUSharedSnapshot@@@Z", ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong version([NativeTypeName("HandleSharedSnapshot")] SharedSnapshot* snapshot);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?snapshot_table_root@@YAPEAXPEAUSharedSnapshot@@P6APEAXUKernelStringSlice@@@Z@Z", ExactSpelling = true)]
    [return: NativeTypeName("NullableCvoid")]
    public static extern void* snapshot_table_root([NativeTypeName("HandleSharedSnapshot")] SharedSnapshot* snapshot, [NativeTypeName("AllocateStringFn")] IntPtr allocate_fn);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?string_slice_next@@YA_NPEAUStringSliceIterator@@PEAXP6AX1UKernelStringSlice@@@Z@Z", ExactSpelling = true)]
    public static extern bool string_slice_next([NativeTypeName("HandleStringSliceIterator")] StringSliceIterator* data, [NativeTypeName("NullableCvoid")] void* engine_context, [NativeTypeName("void (*)(NullableCvoid, struct KernelStringSlice)")] IntPtr engine_visitor);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?free_string_slice_data@@YAXPEAUStringSliceIterator@@@Z", ExactSpelling = true)]
    public static extern void free_string_slice_data([NativeTypeName("HandleStringSliceIterator")] StringSliceIterator* data);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?visit_schema@@YA_KPEAUSharedSnapshot@@PEAUEngineSchemaVisitor@@@Z", ExactSpelling = true)]
    [return: NativeTypeName("uintptr_t")]
    public static extern ulong visit_schema([NativeTypeName("HandleSharedSnapshot")] SharedSnapshot* snapshot, [NativeTypeName("struct EngineSchemaVisitor *")] EngineSchemaVisitor* visitor);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?visit_expression_and@@YA_KPEAUKernelExpressionVisitorState@@PEAUEngineIterator@@@Z", ExactSpelling = true)]
    [return: NativeTypeName("uintptr_t")]
    public static extern ulong visit_expression_and([NativeTypeName("struct KernelExpressionVisitorState *")] KernelExpressionVisitorState* state, [NativeTypeName("struct EngineIterator *")] EngineIterator* children);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?visit_expression_lt@@YA_KPEAUKernelExpressionVisitorState@@_K1@Z", ExactSpelling = true)]
    [return: NativeTypeName("uintptr_t")]
    public static extern ulong visit_expression_lt([NativeTypeName("struct KernelExpressionVisitorState *")] KernelExpressionVisitorState* state, [NativeTypeName("uintptr_t")] ulong a, [NativeTypeName("uintptr_t")] ulong b);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?visit_expression_le@@YA_KPEAUKernelExpressionVisitorState@@_K1@Z", ExactSpelling = true)]
    [return: NativeTypeName("uintptr_t")]
    public static extern ulong visit_expression_le([NativeTypeName("struct KernelExpressionVisitorState *")] KernelExpressionVisitorState* state, [NativeTypeName("uintptr_t")] ulong a, [NativeTypeName("uintptr_t")] ulong b);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?visit_expression_gt@@YA_KPEAUKernelExpressionVisitorState@@_K1@Z", ExactSpelling = true)]
    [return: NativeTypeName("uintptr_t")]
    public static extern ulong visit_expression_gt([NativeTypeName("struct KernelExpressionVisitorState *")] KernelExpressionVisitorState* state, [NativeTypeName("uintptr_t")] ulong a, [NativeTypeName("uintptr_t")] ulong b);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?visit_expression_ge@@YA_KPEAUKernelExpressionVisitorState@@_K1@Z", ExactSpelling = true)]
    [return: NativeTypeName("uintptr_t")]
    public static extern ulong visit_expression_ge([NativeTypeName("struct KernelExpressionVisitorState *")] KernelExpressionVisitorState* state, [NativeTypeName("uintptr_t")] ulong a, [NativeTypeName("uintptr_t")] ulong b);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?visit_expression_eq@@YA_KPEAUKernelExpressionVisitorState@@_K1@Z", ExactSpelling = true)]
    [return: NativeTypeName("uintptr_t")]
    public static extern ulong visit_expression_eq([NativeTypeName("struct KernelExpressionVisitorState *")] KernelExpressionVisitorState* state, [NativeTypeName("uintptr_t")] ulong a, [NativeTypeName("uintptr_t")] ulong b);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?visit_expression_column@@YA?AUExternResultusize@@PEAUKernelExpressionVisitorState@@UKernelStringSlice@@P6APEAUEngineError@@W4KernelError@@1@Z@Z", ExactSpelling = true)]
    [return: NativeTypeName("struct ExternResultusize")]
    public static extern ExternResultusize visit_expression_column([NativeTypeName("struct KernelExpressionVisitorState *")] KernelExpressionVisitorState* state, [NativeTypeName("struct KernelStringSlice")] KernelStringSlice name, [NativeTypeName("AllocateErrorFn")] IntPtr allocate_error);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?visit_expression_not@@YA_KPEAUKernelExpressionVisitorState@@_K@Z", ExactSpelling = true)]
    [return: NativeTypeName("uintptr_t")]
    public static extern ulong visit_expression_not([NativeTypeName("struct KernelExpressionVisitorState *")] KernelExpressionVisitorState* state, [NativeTypeName("uintptr_t")] ulong inner_expr);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?visit_expression_is_null@@YA_KPEAUKernelExpressionVisitorState@@_K@Z", ExactSpelling = true)]
    [return: NativeTypeName("uintptr_t")]
    public static extern ulong visit_expression_is_null([NativeTypeName("struct KernelExpressionVisitorState *")] KernelExpressionVisitorState* state, [NativeTypeName("uintptr_t")] ulong inner_expr);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?visit_expression_literal_string@@YA?AUExternResultusize@@PEAUKernelExpressionVisitorState@@UKernelStringSlice@@P6APEAUEngineError@@W4KernelError@@1@Z@Z", ExactSpelling = true)]
    [return: NativeTypeName("struct ExternResultusize")]
    public static extern ExternResultusize visit_expression_literal_string([NativeTypeName("struct KernelExpressionVisitorState *")] KernelExpressionVisitorState* state, [NativeTypeName("struct KernelStringSlice")] KernelStringSlice value, [NativeTypeName("AllocateErrorFn")] IntPtr allocate_error);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?visit_expression_literal_int@@YA_KPEAUKernelExpressionVisitorState@@H@Z", ExactSpelling = true)]
    [return: NativeTypeName("uintptr_t")]
    public static extern ulong visit_expression_literal_int([NativeTypeName("struct KernelExpressionVisitorState *")] KernelExpressionVisitorState* state, [NativeTypeName("int32_t")] int value);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?visit_expression_literal_long@@YA_KPEAUKernelExpressionVisitorState@@_J@Z", ExactSpelling = true)]
    [return: NativeTypeName("uintptr_t")]
    public static extern ulong visit_expression_literal_long([NativeTypeName("struct KernelExpressionVisitorState *")] KernelExpressionVisitorState* state, [NativeTypeName("int64_t")] long value);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?visit_expression_literal_short@@YA_KPEAUKernelExpressionVisitorState@@F@Z", ExactSpelling = true)]
    [return: NativeTypeName("uintptr_t")]
    public static extern ulong visit_expression_literal_short([NativeTypeName("struct KernelExpressionVisitorState *")] KernelExpressionVisitorState* state, [NativeTypeName("int16_t")] short value);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?visit_expression_literal_byte@@YA_KPEAUKernelExpressionVisitorState@@C@Z", ExactSpelling = true)]
    [return: NativeTypeName("uintptr_t")]
    public static extern ulong visit_expression_literal_byte([NativeTypeName("struct KernelExpressionVisitorState *")] KernelExpressionVisitorState* state, [NativeTypeName("int8_t")] sbyte value);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?visit_expression_literal_float@@YA_KPEAUKernelExpressionVisitorState@@M@Z", ExactSpelling = true)]
    [return: NativeTypeName("uintptr_t")]
    public static extern ulong visit_expression_literal_float([NativeTypeName("struct KernelExpressionVisitorState *")] KernelExpressionVisitorState* state, float value);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?visit_expression_literal_double@@YA_KPEAUKernelExpressionVisitorState@@N@Z", ExactSpelling = true)]
    [return: NativeTypeName("uintptr_t")]
    public static extern ulong visit_expression_literal_double([NativeTypeName("struct KernelExpressionVisitorState *")] KernelExpressionVisitorState* state, double value);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?visit_expression_literal_bool@@YA_KPEAUKernelExpressionVisitorState@@_N@Z", ExactSpelling = true)]
    [return: NativeTypeName("uintptr_t")]
    public static extern ulong visit_expression_literal_bool([NativeTypeName("struct KernelExpressionVisitorState *")] KernelExpressionVisitorState* state, bool value);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?read_result_next@@YA?AUExternResultbool@@PEAUExclusiveFileReadResultIterator@@PEAXP6AX1PEAUExclusiveEngineData@@@Z@Z", ExactSpelling = true)]
    [return: NativeTypeName("struct ExternResultbool")]
    public static extern ExternResultbool read_result_next([NativeTypeName("HandleExclusiveFileReadResultIterator")] ExclusiveFileReadResultIterator* data, [NativeTypeName("NullableCvoid")] void* engine_context, [NativeTypeName("void (*)(NullableCvoid, HandleExclusiveEngineData)")] IntPtr engine_visitor);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?free_read_result_iter@@YAXPEAUExclusiveFileReadResultIterator@@@Z", ExactSpelling = true)]
    public static extern void free_read_result_iter([NativeTypeName("HandleExclusiveFileReadResultIterator")] ExclusiveFileReadResultIterator* data);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?read_parquet_file@@YA?AUExternResultHandleExclusiveFileReadResultIterator@@PEAUSharedExternEngine@@PEBUFileMeta@@PEAUSharedSchema@@@Z", ExactSpelling = true)]
    [return: NativeTypeName("struct ExternResultHandleExclusiveFileReadResultIterator")]
    public static extern ExternResultHandleExclusiveFileReadResultIterator read_parquet_file([NativeTypeName("HandleSharedExternEngine")] SharedExternEngine* engine, [NativeTypeName("const struct FileMeta *")] FileMeta* file, [NativeTypeName("HandleSharedSchema")] SharedSchema* physical_schema);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?engine_data_length@@YA_KPEAPEAUExclusiveEngineData@@@Z", ExactSpelling = true)]
    [return: NativeTypeName("uintptr_t")]
    public static extern ulong engine_data_length([NativeTypeName("HandleExclusiveEngineData *")] ExclusiveEngineData** data);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?get_raw_engine_data@@YAPEAXPEAUExclusiveEngineData@@@Z", ExactSpelling = true)]
    public static extern void* get_raw_engine_data([NativeTypeName("HandleExclusiveEngineData")] ExclusiveEngineData* data);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?get_raw_arrow_data@@YA?AUExternResultArrowFFIData@@PEAUExclusiveEngineData@@PEAUSharedExternEngine@@@Z", ExactSpelling = true)]
    [return: NativeTypeName("struct ExternResultArrowFFIData")]
    public static extern ExternResultArrowFFIData get_raw_arrow_data([NativeTypeName("HandleExclusiveEngineData")] ExclusiveEngineData* data, [NativeTypeName("HandleSharedExternEngine")] SharedExternEngine* engine);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?free_scan@@YAXPEAUSharedScan@@@Z", ExactSpelling = true)]
    public static extern void free_scan([NativeTypeName("HandleSharedScan")] SharedScan* scan);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?scan@@YA?AUExternResultHandleSharedScan@@PEAUSharedSnapshot@@PEAUSharedExternEngine@@PEAUEnginePredicate@@@Z", ExactSpelling = true)]
    [return: NativeTypeName("struct ExternResultHandleSharedScan")]
    public static extern ExternResultHandleSharedScan scan([NativeTypeName("HandleSharedSnapshot")] SharedSnapshot* snapshot, [NativeTypeName("HandleSharedExternEngine")] SharedExternEngine* engine, [NativeTypeName("struct EnginePredicate *")] EnginePredicate* predicate);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?get_global_scan_state@@YAPEAUSharedGlobalScanState@@PEAUSharedScan@@@Z", ExactSpelling = true)]
    [return: NativeTypeName("HandleSharedGlobalScanState")]
    public static extern SharedGlobalScanState* get_global_scan_state([NativeTypeName("HandleSharedScan")] SharedScan* scan);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?get_global_read_schema@@YAPEAUSharedSchema@@PEAUSharedGlobalScanState@@@Z", ExactSpelling = true)]
    [return: NativeTypeName("HandleSharedSchema")]
    public static extern SharedSchema* get_global_read_schema([NativeTypeName("HandleSharedGlobalScanState")] SharedGlobalScanState* state);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?free_global_read_schema@@YAXPEAUSharedSchema@@@Z", ExactSpelling = true)]
    public static extern void free_global_read_schema([NativeTypeName("HandleSharedSchema")] SharedSchema* schema);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?get_partition_column_count@@YA_KPEAUSharedGlobalScanState@@@Z", ExactSpelling = true)]
    [return: NativeTypeName("uintptr_t")]
    public static extern ulong get_partition_column_count([NativeTypeName("HandleSharedGlobalScanState")] SharedGlobalScanState* state);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?get_partition_columns@@YAPEAUStringSliceIterator@@PEAUSharedGlobalScanState@@@Z", ExactSpelling = true)]
    [return: NativeTypeName("HandleStringSliceIterator")]
    public static extern StringSliceIterator* get_partition_columns([NativeTypeName("HandleSharedGlobalScanState")] SharedGlobalScanState* state);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?free_global_scan_state@@YAXPEAUSharedGlobalScanState@@@Z", ExactSpelling = true)]
    public static extern void free_global_scan_state([NativeTypeName("HandleSharedGlobalScanState")] SharedGlobalScanState* state);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?kernel_scan_data_init@@YA?AUExternResultHandleSharedScanDataIterator@@PEAUSharedExternEngine@@PEAUSharedScan@@@Z", ExactSpelling = true)]
    [return: NativeTypeName("struct ExternResultHandleSharedScanDataIterator")]
    public static extern ExternResultHandleSharedScanDataIterator kernel_scan_data_init([NativeTypeName("HandleSharedExternEngine")] SharedExternEngine* engine, [NativeTypeName("HandleSharedScan")] SharedScan* scan);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?kernel_scan_data_next@@YA?AUExternResultbool@@PEAUSharedScanDataIterator@@PEAXP6AX1PEAUExclusiveEngineData@@UKernelBoolSlice@@@Z@Z", ExactSpelling = true)]
    [return: NativeTypeName("struct ExternResultbool")]
    public static extern ExternResultbool kernel_scan_data_next([NativeTypeName("HandleSharedScanDataIterator")] SharedScanDataIterator* data, [NativeTypeName("NullableCvoid")] void* engine_context, [NativeTypeName("void (*)(NullableCvoid, HandleExclusiveEngineData, struct KernelBoolSlice)")] IntPtr engine_visitor);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?free_kernel_scan_data@@YAXPEAUSharedScanDataIterator@@@Z", ExactSpelling = true)]
    public static extern void free_kernel_scan_data([NativeTypeName("HandleSharedScanDataIterator")] SharedScanDataIterator* data);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?get_from_map@@YAPEAXPEBUCStringMap@@UKernelStringSlice@@P6APEAX1@Z@Z", ExactSpelling = true)]
    [return: NativeTypeName("NullableCvoid")]
    public static extern void* get_from_map([NativeTypeName("const struct CStringMap *")] CStringMap* map, [NativeTypeName("struct KernelStringSlice")] KernelStringSlice key, [NativeTypeName("AllocateStringFn")] IntPtr allocate_fn);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?selection_vector_from_dv@@YA?AUExternResultKernelBoolSlice@@PEBUDvInfo@@PEAUSharedExternEngine@@PEAUSharedGlobalScanState@@@Z", ExactSpelling = true)]
    [return: NativeTypeName("struct ExternResultKernelBoolSlice")]
    public static extern ExternResultKernelBoolSlice selection_vector_from_dv([NativeTypeName("const struct DvInfo *")] DvInfo* dv_info, [NativeTypeName("HandleSharedExternEngine")] SharedExternEngine* engine, [NativeTypeName("HandleSharedGlobalScanState")] SharedGlobalScanState* state);

    [DllImport("delta_kernel_ffi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?visit_scan_data@@YAXPEAUExclusiveEngineData@@UKernelBoolSlice@@PEAXP6AX2UKernelStringSlice@@_JPEBUStats@@PEBUDvInfo@@PEBUCStringMap@@@Z@Z", ExactSpelling = true)]
    public static extern void visit_scan_data([NativeTypeName("HandleExclusiveEngineData")] ExclusiveEngineData* data, [NativeTypeName("struct KernelBoolSlice")] KernelBoolSlice selection_vec, [NativeTypeName("NullableCvoid")] void* engine_context, [NativeTypeName("CScanCallback")] IntPtr callback);
}
