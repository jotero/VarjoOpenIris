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
        }
    }
}