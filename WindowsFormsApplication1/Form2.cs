using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
             
            progressBar1.Visible=true;
                    progressBar1.Value = progressBar1.Value + 2;
                    if (progressBar1.Value == 10)
                        label1.Text = "Reading modules..";
                    else if (progressBar1.Value == 20)
                        label1.Text = "Turning on modules.";
                    else if (progressBar1.Value == 40)
                        label1.Text = "Starting modules..";
                    else if (progressBar1.Value == 60)
                        label1.Text = "Loading modules..";
                    else if (progressBar1.Value == 80)
                        label1.Text = "Done Loading modules..";
                    else if (progressBar1.Value == 100)
                    {
                        Form1 f1 = new Form1();
                        f1.Show();
                        timer1.Enabled = false;
                        this.Hide();
                    }
                        
                    }
    }
}
