using System.Runtime.InteropServices;

class CffiTest
{
  [DllImport("delta_kernel_ffi", EntryPoint = "addition")]
  public static extern uint Addition(uint a, uint b);

  static public void Main()
  {
    var sum = CffiTest.Addition(1, 2);
    Console.WriteLine(sum);
  }
}
