using System;
using System.Windows.Forms;
using Form_Loading.Models;
using Form_Loading.UI;

namespace Form_Loading
{
    /// <summary>Sign-in screen. Customers can sign in, create an account, or browse as a guest.</summary>
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            textBoxPassword.UseSystemPasswordChar = true;
            labelGuest.Cursor = Cursors.Hand;
            AcceptButton = buttonLogin;

            // Opening this screen signs out the current customer. The cart is kept.
            UserStore.SignOut();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text.Trim();

            if (username.Length == 0)
            {
                ShowWarning("Please enter your username or email.");
                textBoxUsername.Focus();
                return;
            }
            if (textBoxPassword.Text.Length == 0)
            {
                ShowWarning("Please enter your password.");
                textBoxPassword.Focus();
                return;
            }

            if (UserStore.SignIn(username, textBoxPassword.Text))
            {
                Nav.Go(this, new FormOne());
            }
            else
            {
                ShowWarning("Incorrect username or password.");
                textBoxPassword.Clear();
                textBoxPassword.Focus();
            }
        }

        private void buttonSignUp_Click(object sender, EventArgs e)
        {
            Nav.Go(this, new FormTwo());
        }

        private void labelGuest_Click(object sender, EventArgs e)
        {
            Nav.Go(this, new FormOne());
        }

        private static void ShowWarning(string message)
        {
            MessageBox.Show(message, "Sign In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
