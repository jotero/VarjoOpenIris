using System.Runtime.InteropServices;
using VarjoOpenIrisPlugin;
using static VarjoOpenIrisPlugin.Class1;

namespace VarjoOpenIrisDotNetApp
{
    public partial class Form1 : Form
    {
        Class1 Class1 = new Class1();

        public Form1()
        {
            InitializeComponent();

            Class1.NewFrame += Class1_NewFrame;
        }

        private void Class1_NewFrame(object sender, NewFrameEventArgs e)
        {
            this.label1.Text = e.frameNumber.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // int res = Class1.MyFunction2();

           // MessageBox.Show("success res = " + res);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Create a delegate instance
            CallbackDelegate callback = new CallbackDelegate(Class1.MyCallbackFunction);

            // Register the callback with the C++ DLL
            Console.WriteLine("Callback registered.");

            int result = Class1.VarjoStartCameras(callback);

            int a = 1;
        }

    }
}