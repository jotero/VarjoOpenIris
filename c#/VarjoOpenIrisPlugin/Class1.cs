using System.Runtime.InteropServices;

namespace VarjoOpenIrisPlugin
{
    public class Class1
    {
        [DllImport("VarjoOpenIrisLib.dll", CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi, EntryPoint = "MyFunction2")]
        public static extern int MyFunction2();


    }
}