using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualProgrammingProject
{
    public partial class SignIn : Form
    {
        public Person player;
        private ErrorProvider errorProvider;

        public SignIn()
        {
            InitializeComponent();
            errorProvider = new ErrorProvider();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            player = new Person(textBox1.Text);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
         
            if (textBox1.Text.Trim().Length == 0)
            {
                errorProvider.SetError(textBox1, "Name is required");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(textBox1, null);
                e.Cancel = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
