using DeltaLake.Kernel.Rust.Ffi;
using System.Runtime.InteropServices;

public static unsafe class StringAllocator
{
    public static sbyte* AllocateString(KernelStringSlice slice)
    {
        if (slice.ptr == null || slice.len == 0)
            return null;

        // Allocate unmanaged memory for the string, adding 1 for the null
        // terminator.
        //
        int len = (int)slice.len;
        IntPtr unmanagedMemory = Marshal.AllocHGlobal(len + 1);

        // Copy the string into the allocated unmanaged memory.
        //
        for (int i = 0; i < len; i++)
        {
            Marshal.WriteByte(unmanagedMemory, i, Marshal.ReadByte((IntPtr)slice.ptr, i));
        }

        // Set the null terminator.
        //
        Marshal.WriteByte(unmanagedMemory, len, 0);

        return (sbyte*)unmanagedMemory.ToPointer();
    }
}
