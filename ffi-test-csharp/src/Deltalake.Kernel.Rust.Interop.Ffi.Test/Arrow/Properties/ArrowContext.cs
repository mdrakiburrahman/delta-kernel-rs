using Apache.Arrow;
using Microsoft.Data.Analysis;
using System.Text;

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

        public override string ToString()
        {
            List<RecordBatch> recordBatches = new List<RecordBatch>(NumBatches);
            for (int i = 0; i < NumBatches; i++)
            {
                recordBatches.Add(*Batches[i]);
            }

            if (recordBatches.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            bool addedSchema = false;
            for (int i = 0; i < recordBatches.Count; i++)
            {
                DataFrame combinedDataFrame = DataFrame.FromArrowRecordBatch(recordBatches[i]);
                DataFrameRow[] rows = combinedDataFrame.Rows.ToArray();

                string content = null;

                // Some problems with dereferencing the Row, keep going for the demo
                //
                try
                {
                    content = combinedDataFrame.ToString();
                }
                catch (Exception e) { }

                if (content != null)
                {
                    if (!addedSchema)
                    {
                        sb.Append(content);
                        addedSchema = true;
                    }
                    else
                    {
                        string[] lines = content.Split(Environment.NewLine);
                        for (int j = 1; j < lines.Length; j++)
                        {
                            sb.Append(lines[j]);
                        }
                        sb.Append(Environment.NewLine);
          }
                }
            }
            return sb.ToString();
        }
    }
}
