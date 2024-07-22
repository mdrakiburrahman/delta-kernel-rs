using Apache.Arrow.C;
using DeltaLake.Kernel.Rust.Ffi;

namespace Deltalake.Kernel.Rust.Interop.Ffi.Test.Arrow.Converter
{
  public static class ArrowFfiSchemaConverter
    {
        public unsafe static CArrowSchema* ConvertFFIArrowSchemaToCArrowSchema(
            FFI_ArrowSchema* ffiSchema
        )
        {
            CArrowSchema* cSchema = CArrowSchema.Create();

            cSchema->format = (byte*)ffiSchema->format;
            cSchema->name = (byte*)ffiSchema->name;
            cSchema->metadata = (byte*)ffiSchema->metadata;
            cSchema->flags = ffiSchema->flags;
            cSchema->n_children = ffiSchema->n_children;
            cSchema->children = (CArrowSchema**)ffiSchema->children;
            cSchema->dictionary = (CArrowSchema*)ffiSchema->dictionary;
            cSchema->private_data = ffiSchema->private_data;

            return cSchema;
        }
    }
}
