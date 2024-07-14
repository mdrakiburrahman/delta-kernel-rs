using DeltaLake.Kernel.Rust.Ffi;
using DeltaLake.Kernel.Rust.Interop.Ffi.Test.Extensions;

namespace DeltaLake.Kernel.Rust.Interop.Ffi.Test
{
    public unsafe class FfiTestConsoleApp
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentException("Usage: table/path");
            }
            var tablePath = args[0];

            Console.WriteLine($"Reading table at {tablePath}");

            fixed (sbyte* tablePathPtr = tablePath.ToSByte())
            {
                KernelStringSlice table_path_slice = new KernelStringSlice
                {
                    ptr = tablePathPtr,
                    len = (nuint)tablePath.Length
                };
            }
        }
    }
}
