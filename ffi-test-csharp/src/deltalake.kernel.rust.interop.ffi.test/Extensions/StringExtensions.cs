using System.Text;

namespace DeltaLake.Kernel.Rust.Interop.Ffi.Test.Extensions
{
  public unsafe static class StringExtensions
  {
    public static sbyte[] ToSByte(this string str)
    {
      int byteCount = Encoding.UTF8.GetByteCount(str);
      sbyte[] bytes = new sbyte[byteCount];
      fixed (char* strPtr = str)
      fixed (sbyte* bytesPtr = bytes)
      {
        Encoding.UTF8.GetBytes(strPtr, str.Length, (byte*)bytesPtr, byteCount);
      }
      return bytes;
    }
  }
}
