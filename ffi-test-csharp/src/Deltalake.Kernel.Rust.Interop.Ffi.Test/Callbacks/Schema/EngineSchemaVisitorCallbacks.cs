using Deltalake.Kernel.Rust.Interop.Ffi.Test.Schema.Diagnostics;
using Deltalake.Kernel.Rust.Interop.Ffi.Test.Schema.Properties;
using DeltaLake.Kernel.Rust.Ffi;
using System.Runtime.InteropServices;

namespace Deltalake.Kernel.Rust.Interop.Ffi.Test.Callbacks.Schema
{
  public static class EngineSchemaVisitorCallbacks
    {
        public static unsafe UIntPtr MakeFieldListDemo(void* data, UIntPtr reserve)
        {
            SchemaBuilder* builder = (SchemaBuilder*)data;
            int id = builder->list_count;
            Console.WriteLine($"Making a list of length {reserve} with id {id}");
            builder->list_count++;
            builder->lists = (SchemaItemList*)Marshal.ReAllocHGlobal((IntPtr)builder->lists, (IntPtr)(sizeof(SchemaItemList) * builder->list_count));
            SchemaItem* list = (SchemaItem*)Marshal.AllocHGlobal((int)reserve * sizeof(SchemaItem));
            for (UIntPtr i = 0; i < reserve; i++) list[(int)i].children = UIntPtr.MaxValue;
            builder->lists[id].len = 0;
            builder->lists[id].list = list;
            return (UIntPtr)id;
        }

        public static unsafe void VisitStructDemo(
            void* data,
            UIntPtr siblingListId,
            KernelStringSlice name,
            UIntPtr childListId
        )
        {
            SchemaBuilder* builder = (SchemaBuilder*)data;
            sbyte* namePtr = StringAllocator.AllocateString(name);
            VisitPrinter.PrintVisit("struct", new string(namePtr), (long)siblingListId, "Children", (long)childListId);
            SchemaItem* structItem = AddToList(&builder->lists[(int)siblingListId], namePtr, (sbyte*)Marshal.StringToHGlobalAnsi("struct"));
            structItem->children = childListId;
        }

        public static unsafe void VisitArrayDemo(
            void* data,
            UIntPtr siblingListId,
            KernelStringSlice name,
            bool containsNull,
            UIntPtr childListId
        )
        {
            SchemaBuilder* builder = (SchemaBuilder*)data;
            int sliceLength = (int)name.len;
            int nameLength = sliceLength + 24;
            sbyte* namePtr = (sbyte*)Marshal.AllocHGlobal(nameLength);
            for (int i = 0; i < sliceLength; i++) namePtr[i] = name.ptr[i];
            namePtr[sliceLength] = (sbyte)'\0';
            string containsNullText = $" (contains null: {(containsNull ? "true" : "false")})";
            for (int i = 0; i < containsNullText.Length; i++) namePtr[sliceLength + i] = (sbyte)containsNullText[i];
            namePtr[sliceLength + containsNullText.Length] = (sbyte)'\0';
            VisitPrinter.PrintVisit("array", new string(namePtr), (long)siblingListId, "Types", (long)childListId);
            SchemaItem* arrayItem = AddToList(&builder->lists[(int)siblingListId], namePtr, (sbyte*)Marshal.StringToHGlobalAnsi("array"));
            arrayItem->children = childListId;
        }

        public static unsafe void VisitMapDemo(
            void* data,
            UIntPtr siblingListId,
            KernelStringSlice name,
            bool valueContainsNull,
            UIntPtr childListId
        )
        {
            SchemaBuilder* builder = (SchemaBuilder*)data;
            int sliceLength = (int)name.len;
            int nameLength = sliceLength + 24;
            sbyte* namePtr = (sbyte*)Marshal.AllocHGlobal(nameLength);
            for (int i = 0; i < sliceLength; i++) namePtr[i] = name.ptr[i];
            namePtr[sliceLength] = (sbyte)'\0';
            string valueContainsNullText = $" (contains null: {(valueContainsNull ? "true" : "false")})";
            for (int i = 0; i < valueContainsNullText.Length; i++) namePtr[sliceLength + i] = (sbyte)valueContainsNullText[i];
            namePtr[sliceLength + valueContainsNullText.Length] = (sbyte)'\0';
            VisitPrinter.PrintVisit("map", new string(namePtr), (long)siblingListId, "Types", (long)childListId);
            SchemaItem* mapItem = AddToList(&builder->lists[(int)siblingListId], namePtr, (sbyte*)Marshal.StringToHGlobalAnsi("map"));
            mapItem->children = childListId;
        }

        public static unsafe void VisitDecimalDemo(
            void* data,
            UIntPtr siblingListId,
            KernelStringSlice name,
            byte precision,
            byte scale
        )
        {
            SchemaBuilder* builder = (SchemaBuilder*)data;
            string nameStr = new string(name.ptr, 0, (int)name.len, System.Text.Encoding.UTF8);
            string typeStr = $"decimal({precision},{scale})";
            VisitPrinter.PrintVisit(typeStr, nameStr, (long)siblingListId);
            AddToList(&builder->lists[(int)siblingListId], (sbyte*)Marshal.StringToHGlobalAnsi(nameStr), (sbyte*)Marshal.StringToHGlobalAnsi(typeStr));
        }

        public static unsafe void VisitSimpleTypeDemo(
            void* data,
            UIntPtr siblingListId,
            KernelStringSlice name,
            string type
        )
        {
            SchemaBuilder* builder = (SchemaBuilder*)data;
            string nameStr = new string(name.ptr, 0, (int)name.len, System.Text.Encoding.UTF8);
            VisitPrinter.PrintVisit(type, nameStr, (long)siblingListId);
            AddToList(&builder->lists[(int)siblingListId], (sbyte*)Marshal.StringToHGlobalAnsi(nameStr), (sbyte*)Marshal.StringToHGlobalAnsi(type));
        }

        public static unsafe void VisitStringDemo(void* data, UIntPtr siblingListId, KernelStringSlice name) => VisitSimpleTypeDemo(data, siblingListId, name, "string");
        public static unsafe void VisitLongDemo(void* data, UIntPtr siblingListId, KernelStringSlice name) => VisitSimpleTypeDemo(data, siblingListId, name, "long");
        public static unsafe void VisitIntegerDemo(void* data, UIntPtr siblingListId, KernelStringSlice name) => VisitSimpleTypeDemo(data, siblingListId, name, "integer");
        public static unsafe void VisitShortDemo(void* data, UIntPtr siblingListId, KernelStringSlice name) => VisitSimpleTypeDemo(data, siblingListId, name, "short");
        public static unsafe void VisitByteDemo(void* data, UIntPtr siblingListId, KernelStringSlice name) => VisitSimpleTypeDemo(data, siblingListId, name, "byte");
        public static unsafe void VisitFloatDemo(void* data, UIntPtr siblingListId, KernelStringSlice name) => VisitSimpleTypeDemo(data, siblingListId, name, "float");
        public static unsafe void VisitDoubleDemo(void* data, UIntPtr siblingListId, KernelStringSlice name) => VisitSimpleTypeDemo(data, siblingListId, name, "double");
        public static unsafe void VisitBooleanDemo(void* data, UIntPtr siblingListId, KernelStringSlice name) => VisitSimpleTypeDemo(data, siblingListId, name, "boolean");
        public static unsafe void VisitBinaryDemo(void* data, UIntPtr siblingListId, KernelStringSlice name) => VisitSimpleTypeDemo(data, siblingListId, name, "binary");
        public static unsafe void VisitDateDemo(void* data, UIntPtr siblingListId, KernelStringSlice name) => VisitSimpleTypeDemo(data, siblingListId, name, "date");
        public static unsafe void VisitTimestampDemo(void* data, UIntPtr siblingListId, KernelStringSlice name) => VisitSimpleTypeDemo(data, siblingListId, name, "timestamp");
        public static unsafe void VisitTimestampNtzDemo(void* data, UIntPtr siblingListId, KernelStringSlice name) => VisitSimpleTypeDemo(data, siblingListId, name, "timestamp_ntz");

        private static unsafe SchemaItem* AddToList(SchemaItemList* list, sbyte* name, sbyte* type)
        {
            int idx = (int)list->len;
            list->list[idx].name = name;
            list->list[idx].type = type;
            list->len++;
            return &list->list[idx];
        }
    }
}
