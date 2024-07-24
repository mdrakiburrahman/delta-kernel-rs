using Apache.Arrow;
using Deltalake.Kernel.Rust.Interop.Ffi.Test.Arrow.Properties;

namespace Deltalake.Kernel.Rust.Interop.Ffi.Test.Arrow
{
    public static class ArrowContextExtensions
    {
        public unsafe static Table ConvertToTable(this ArrowContext context)
        {
            if (
                context == null
                || context.NumBatches == 0
                || context.Batches == null
                || context.Schema == null
            )
            {
                throw new ArgumentException("Invalid ArrowContext provided.");
            }

            List<RecordBatch> recordBatches = new List<RecordBatch>(context.NumBatches);
            for (int i = 0; i < context.NumBatches; i++)
            {
                recordBatches.Add(*context.Batches[i]);
            }
            return Table.TableFromRecordBatches(context.Schema, recordBatches);
        }
    }
}
