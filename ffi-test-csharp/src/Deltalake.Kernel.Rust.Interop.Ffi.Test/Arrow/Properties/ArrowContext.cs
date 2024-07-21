using Apache.Arrow;

namespace Deltalake.Kernel.Rust.Interop.Ffi.Test.Arrow.Properties
{
    public unsafe class ArrowContext
    {
        public ulong NumBatches;
        public RecordBatch** Batches;
        public BooleanArray* CurFilter;

        public ArrowContext()
        {
            NumBatches = 0;
            Batches = null;
            CurFilter = null;
        }
    }
}
