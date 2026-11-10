using System;
using System.Windows.Forms;
using Form_Loading.Models;
using Form_Loading.UI;

namespace Form_Loading
{
    /// <summary>Create-account screen.</summary>
    public partial class FormTwo : Form
    {
        public FormTwo()
        {
            InitializeComponent();

            textBoxPassword.UseSystemPasswordChar = true;
            AcceptButton = buttonCreateAccount;
        }

        private void buttonCreateAccount_Click(object sender, EventArgs e)
        {
            string error = UserStore.Register(textBoxName.Text, textBoxEmail.Text, textBoxPassword.Text);
            if (error != null)
            {
                MessageBox.Show(error, "Create Account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Welcome to Handcraft Heaven, " + UserStore.Current.Name + "!",
                "Account Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Nav.Go(this, new FormOne());
        }

        private void buttonSignIn_Click(object sender, EventArgs e)
        {
            Nav.Go(this, new FormMain());
        }
    }
}
