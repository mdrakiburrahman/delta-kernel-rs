using Deltalake.Kernel.Rust.Interop.Ffi.Test.Schema.Context;
using DeltaLake.Kernel.Rust.Ffi;
using System.Runtime.InteropServices;

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

        public static unsafe void PrintPartitionInfo(
            EngineContext* context,
            CStringMap* partitionValues
        )
        {
            for (int i = 0; i < context->PartitionCols->Len; i++)
            {
                char* col = context->PartitionCols->Cols[i];
                KernelStringSlice key = new KernelStringSlice
                {
                    ptr = (sbyte*)col,
                    len = StrLen(col)
        };

                char* partitionVal = (char*)FFI_NativeMethodsHandler.get_from_map(partitionValues, key, Marshal.GetFunctionPointerForDelegate(StringAllocator.AllocateString));
                if (partitionVal != null)
                {
                    Console.WriteLine($"\tPartition '{Marshal.PtrToStringAnsi((IntPtr)col)}' here: {Marshal.PtrToStringAnsi((IntPtr)partitionVal)}");
                }
                else
                {
                    Console.WriteLine("\tNo partition here");
                }
            }
        }

        private static unsafe ulong StrLen(char* str)
        {
          char* s;
          for (s = str; *s != '\0'; ++s) { }
          return (ulong)(s - str);
        }
  }
}
