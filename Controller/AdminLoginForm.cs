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
    public partial class AdminLoginForm : Form
    {
        Verification verification;
        public AdminLoginForm()
        {
            InitializeComponent();
        }

        private void button_Login_Click(object sender, EventArgs e)
        {
            verification = new Verification(textBox_Login.Text, textBox_Password.Text);
            listBox1.Items.Clear();
            if (verification.Status)
            {
                this.Hide();
                ShowDialog(new AdminForm(verification));
                this.Show();
            }
        }
    }
}
