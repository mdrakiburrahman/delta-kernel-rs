using Deltalake.Kernel.Rust.Interop.Ffi.Test.Arrow.Properties;
using Deltalake.Kernel.Rust.Interop.Ffi.Test.Schema.Context;
using DeltaLake.Kernel.Rust.Ffi;

namespace Deltalake.Kernel.Rust.Interop.Ffi.Test.Arrow.Handlers
{
  public interface IArrowInteropHandler
    {
        public unsafe void AddBatchToContext(
            ArrowContext context,
            ArrowFFIData arrowData,
            PartitionList partitionCols,
            CStringMap partitionValues
        );
    }
}
