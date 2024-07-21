using Deltalake.Kernel.Rust.Interop.Ffi.Test.Callbacks.Schema;
using Deltalake.Kernel.Rust.Interop.Ffi.Test.Delegates.Schema;
using Deltalake.Kernel.Rust.Interop.Ffi.Test.Schema.Properties;
using DeltaLake.Kernel.Rust.Ffi;
using System.Runtime.InteropServices;

namespace Deltalake.Kernel.Rust.Interop.Ffi.Test.Schema.Handlers
{
    public class SchemaHandlerDemo : ISchemaHandler
    {
        public unsafe void PrintSchema(SharedSnapshot* snapshot)
        {
            Console.WriteLine("Building schema\n");
            SchemaBuilder builder = new SchemaBuilder() { list_count = 0, };

            EngineSchemaVisitor visitor = new EngineSchemaVisitor()
            {
                data = &builder,
                make_field_list = Marshal.GetFunctionPointerForDelegate((EngineSchemaVisitorDelegates.MakeFieldListDelegate)EngineSchemaVisitorCallbacks.MakeFieldListDemo),
                visit_struct = Marshal.GetFunctionPointerForDelegate((EngineSchemaVisitorDelegates.VisitStructDelegate)EngineSchemaVisitorCallbacks.VisitStructDemo),
                visit_array = Marshal.GetFunctionPointerForDelegate((EngineSchemaVisitorDelegates.VisitArrayDelegate)EngineSchemaVisitorCallbacks.VisitArrayDemo),
                visit_map = Marshal.GetFunctionPointerForDelegate((EngineSchemaVisitorDelegates.VisitMapDelegate)EngineSchemaVisitorCallbacks.VisitMapDemo),
                visit_decimal = Marshal.GetFunctionPointerForDelegate((EngineSchemaVisitorDelegates.VisitDecimalDelegate)EngineSchemaVisitorCallbacks.VisitDecimalDemo),
                visit_string = Marshal.GetFunctionPointerForDelegate((EngineSchemaVisitorDelegates.VisitStringDelegate)EngineSchemaVisitorCallbacks.VisitStringDemo),
                visit_long = Marshal.GetFunctionPointerForDelegate((EngineSchemaVisitorDelegates.VisitLongDelegate)EngineSchemaVisitorCallbacks.VisitLongDemo),
                visit_integer = Marshal.GetFunctionPointerForDelegate((EngineSchemaVisitorDelegates.VisitIntegerDelegate)EngineSchemaVisitorCallbacks.VisitIntegerDemo),
                visit_short = Marshal.GetFunctionPointerForDelegate((EngineSchemaVisitorDelegates.VisitShortDelegate)EngineSchemaVisitorCallbacks.VisitShortDemo),
                visit_byte = Marshal.GetFunctionPointerForDelegate((EngineSchemaVisitorDelegates.VisitByteDelegate)EngineSchemaVisitorCallbacks.VisitByteDemo),
                visit_float = Marshal.GetFunctionPointerForDelegate((EngineSchemaVisitorDelegates.VisitFloatDelegate)EngineSchemaVisitorCallbacks.VisitFloatDemo),
                visit_double = Marshal.GetFunctionPointerForDelegate((EngineSchemaVisitorDelegates.VisitDoubleDelegate)EngineSchemaVisitorCallbacks.VisitDoubleDemo),
                visit_boolean = Marshal.GetFunctionPointerForDelegate((EngineSchemaVisitorDelegates.VisitBooleanDelegate)EngineSchemaVisitorCallbacks.VisitBooleanDemo),
                visit_binary = Marshal.GetFunctionPointerForDelegate((EngineSchemaVisitorDelegates.VisitBinaryDelegate)EngineSchemaVisitorCallbacks.VisitBinaryDemo),
                visit_date = Marshal.GetFunctionPointerForDelegate((EngineSchemaVisitorDelegates.VisitDateDelegate)EngineSchemaVisitorCallbacks.VisitDateDemo),
                visit_timestamp = Marshal.GetFunctionPointerForDelegate((EngineSchemaVisitorDelegates.VisitTimestampDelegate)EngineSchemaVisitorCallbacks.VisitTimestampDemo),
                visit_timestamp_ntz = Marshal.GetFunctionPointerForDelegate((EngineSchemaVisitorDelegates.VisitTimestampNtzDelegate)EngineSchemaVisitorCallbacks.VisitTimestampNtzDemo),
            };

            ulong schemaListId = FFI_NativeMethodsHandler.visit_schema(snapshot, &visitor);

            Console.WriteLine("Done building schema\n");

            Console.WriteLine("Schema:\n");
            PrintList(builder, (UIntPtr)schemaListId, 0, 0);
            Console.WriteLine("\n");

            // There's a high chance there's a memory leak because this isn't
            // rigorous as I'm convert C++ to C#.
            //
            // TODO: Stress test this entire handler and spot for memory leaks.
            //
            FreeBuilder(builder);
        }

        public unsafe void PrintList(
            SchemaBuilder builder,
            UIntPtr listId,
            int indent,
            int parentsOnLast
        )
        {
            SchemaItemList list = builder.lists[(int)listId];
            for (uint i = 0; i < list.len; i++)
            {
                bool isLast = i == list.len - 1;
                for (int j = 0; j < indent; j++)
                {
                    if ((indent - parentsOnLast) <= j)
                    {
                        // don't print a dangling | on any parents that are on their last item
                        Console.Write("   ");
                    }
                    else
                    {
                        Console.Write("│  ");
                    }
                }
                SchemaItem item = list.list[i];
                string prefix = isLast ? "└" : "├";
                string name = Marshal.PtrToStringAnsi((IntPtr)item.name);
                string type = Marshal.PtrToStringAnsi((IntPtr)item.type);

                Console.WriteLine($"{prefix}─ {name}: {type}");
                if (item.children != UIntPtr.MaxValue)
                {
                    PrintList(builder, item.children, indent + 1, parentsOnLast + (isLast ? 1 : 0));
                }
            }
        }

        private static unsafe void FreeBuilder(SchemaBuilder builder)
        {
          for (int i = 0; i < builder.list_count; i++)
          {
            SchemaItemList list = builder.lists[i];
            for (int j = 0; j < list.len; j++)
            {
              SchemaItem item = list.list[j];
              Marshal.FreeHGlobal((IntPtr)item.name);
              // Don't free item.Type, those are static strings
              //
              if (Marshal.PtrToStringAnsi((IntPtr)item.type).StartsWith("decimal"))
              {
                // Except decimal types, we allocated those
                //
                Marshal.FreeHGlobal((IntPtr)item.type);
              }
            }
            // Free all the items in this list (we allocated them together)
            //
            Marshal.FreeHGlobal((IntPtr)list.list);
          }
          Marshal.FreeHGlobal((IntPtr)builder.lists);
        }
  }
}
