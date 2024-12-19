using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenIris;

namespace VarjoOpenIrisTestAppFramework
{
    public partial class Form1 : Form
    {
        private EyeTracker eyeTracker;

        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var eyeTracker = EyeTracker.Create();
            eyeTracker.Settings.EyeTrackerSystem = "Varjo";

            eyeTracker.NewDataAndImagesAvailable += EyeTracker_NewDataAndImagesAvailable;

            eyeTracker.StartTracking();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            eyeTracker.StopTracking();
        }

        private void EyeTracker_NewDataAndImagesAvailable(object sender, EyeTrackerImagesAndData e)
        {
            throw new NotImplementedException();
        }
    }
}
