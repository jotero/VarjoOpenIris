using System.Runtime.InteropServices;
using VarjoOpenIrisPlugin;
using static VarjoOpenIrisPlugin.Class1;

namespace VarjoOpenIrisDotNetApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int res = Class1.MyFunction2();

            MessageBox.Show("success res = " + res);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] myArgs = { };
           int result = Class1.MyFunction(myArgs.Length, myArgs);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Create a delegate instance
            CallbackDelegate callback = new CallbackDelegate(MyCallbackFunction);

            // Register the callback with the C++ DLL
            RegisterCallback(callback);

            Console.WriteLine("Callback registered.");
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }
    }
}