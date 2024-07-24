using Apache.Arrow;

namespace Deltalake.Kernel.Rust.Interop.Ffi.Test.Arrow.Properties
{
    public unsafe class ArrowContext
    {
        public int NumBatches;
        public Apache.Arrow.Schema Schema;
        public RecordBatch** Batches;
        public BooleanArray* CurFilter;

        public ArrowContext()
        {
            NumBatches = 0;
            Batches = null;
            CurFilter = null;
            Schema = null;
        }
    }
}
