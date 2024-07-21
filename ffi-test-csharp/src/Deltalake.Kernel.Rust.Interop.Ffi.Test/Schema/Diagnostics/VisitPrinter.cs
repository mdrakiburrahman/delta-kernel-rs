using DeltaLake.Kernel.Rust.Ffi;

namespace Deltalake.Kernel.Rust.Interop.Ffi.Test.Schema.Diagnostics
{
    public static class VisitPrinter
    {
        private const string ChildFmt =
            "Asked to visit {0} named {1} belonging to list {2}. {3} are in {4}.";
        private const string NoChildFmt = "Asked to visit {0} named {1} belonging to list {2}.";

        public static void PrintVisit(
            string type,
            string name,
            long listId,
            string childrenDescription,
            long childrenListId
        )
        {
            Console.WriteLine(ChildFmt, type, name, listId, childrenDescription, childrenListId);
        }

        public static void PrintVisit(string type, string name, long listId)
        {
            Console.WriteLine(NoChildFmt, type, name, listId);
        }

        public static unsafe void PrintSelectionVector(string indent, KernelBoolSlice selectionVec)
        {
            for (ulong i = 0; i < selectionVec.len; i++)
            {
                Console.WriteLine($"{indent}sel[{i}] = {(selectionVec.ptr[i] ? "true" : "false")}");
            }
        }
    }
}
