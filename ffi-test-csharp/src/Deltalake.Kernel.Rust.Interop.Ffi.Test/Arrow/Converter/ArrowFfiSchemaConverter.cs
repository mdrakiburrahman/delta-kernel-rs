using Apache.Arrow.C;
using DeltaLake.Kernel.Rust.Ffi;
using System.Runtime.InteropServices;

namespace Deltalake.Kernel.Rust.Interop.Ffi.Test.Arrow.Converter
{
    public static class ArrowFfiSchemaConverter
    {
        public unsafe static CArrowSchema* ConvertFFISchema(FFI_ArrowSchema* ffiSchema)
        {
            CArrowSchema* cSchema = CArrowSchema.Create();
            *cSchema = default(CArrowSchema);

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

        public unsafe static CArrowArray* ConvertFFIArray(FFI_ArrowArray* ffiArray)
        {
            CArrowArray* cArray = (CArrowArray*)Marshal.AllocHGlobal(sizeof(CArrowArray));
            *cArray = default(CArrowArray);

            cArray->length = ffiArray->length;
            cArray->null_count = ffiArray->null_count;
            cArray->offset = ffiArray->offset;
            cArray->n_buffers = ffiArray->n_buffers;
            cArray->n_children = ffiArray->n_children;
            cArray->buffers = (byte**)ffiArray->buffers;
            cArray->children = (CArrowArray**)ffiArray->children;
            cArray->dictionary = (CArrowArray*)ffiArray->dictionary;
            cArray->private_data = ffiArray->private_data;

            return cArray;
        }
    }
}
