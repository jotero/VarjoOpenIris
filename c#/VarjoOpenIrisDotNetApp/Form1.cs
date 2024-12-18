using VarjoOpenIrisPlugin;

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
    }
}