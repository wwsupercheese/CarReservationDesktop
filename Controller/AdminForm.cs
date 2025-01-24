using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Service;

namespace Controller
{
    public partial class AdminForm : Form
    {
        Verification verification;
        public AdminForm(Verification _verification)
        {
            InitializeComponent();
            verification = _verification;
            ShowAdmin();

        }
        public void ShowAdmin()
        {
            if (verification.Status)
            {
                textBox1.Text = "";
                foreach(var key in verification.info.Keys)
                {
                    textBox1.Text += key + " | " + verification.info[key] + " | ";
                }
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 3);
            }
        }
    }
}
