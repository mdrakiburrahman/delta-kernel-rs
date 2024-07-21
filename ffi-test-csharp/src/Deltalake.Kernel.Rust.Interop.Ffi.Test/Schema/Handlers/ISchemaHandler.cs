using Deltalake.Kernel.Rust.Interop.Ffi.Test.Schema.Properties;
using DeltaLake.Kernel.Rust.Ffi;

namespace Deltalake.Kernel.Rust.Interop.Ffi.Test.Schema.Handlers
{
    public interface ISchemaHandler
    {
        public unsafe void PrintSchema(SharedSnapshot* snapshot);

        public void PrintList(
            SchemaBuilder builder,
            UIntPtr listId,
            int indent,
            int parentsOnLast
        );
    }
}
