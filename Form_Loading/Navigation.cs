using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Form_Loading.Models;
using Form_Loading.UI;

namespace Form_Loading
{
    /// <summary>
    /// Sidebar shown on every shop screen: logo (home), page links with the
    /// current page highlighted, cart with a live item count, and the signed-in user.
    /// </summary>
    public partial class Navigation : UserControl
    {
        private static readonly Color ActiveBack = Color.FromArgb(42, 157, 143);

        public Navigation()
        {
            InitializeComponent();

            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Click += (s, e) => GoTo(new FormOne());
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Skip runtime wiring when shown in the Visual Studio designer
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return;

            HighlightCurrentPage();

            // Unsubscribe first so a recreated handle never subscribes twice
            Cart.Changed -= UpdateState;
            UserStore.CurrentChanged -= UpdateState;
            Cart.Changed += UpdateState;
            UserStore.CurrentChanged += UpdateState;
            UpdateState(this, EventArgs.Empty);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            // Static events would otherwise keep closed screens alive
            Cart.Changed -= UpdateState;
            UserStore.CurrentChanged -= UpdateState;
            base.OnHandleDestroyed(e);
        }

        private void HighlightCurrentPage()
        {
            Form page = FindForm();
            Button active = null;
            if (page is FormOne) active = buttonHome;
            else if (page is FormCategory) active = buttonCategories;
            else if (page is FormThree) active = buttonHandbags;
            else if (page is FormCart) active = buttonCart;

            if (active != null)
            {
                active.BackColor = ActiveBack;
                active.FlatAppearance.MouseOverBackColor = ActiveBack;
                active.ForeColor = Color.White;
                active.Font = new Font(active.Font, FontStyle.Bold);
            }
        }

        private void UpdateState(object sender, EventArgs e)
        {
            buttonCart.Text = Cart.Count == 0 ? "Cart" : "Cart (" + Cart.Count + ")";

            bool signedIn = UserStore.IsSignedIn;
            buttonLogin.Text = signedIn ? "Sign Out" : "Sign In";
            buttonRegister.Visible = !signedIn;
            // Close the gap left by the hidden Create Account button
            buttonLogin.Top = signedIn ? buttonRegister.Top : buttonRegister.Bottom + 4;
            labelUserCaption.Text = signedIn ? "Signed in as" : "Browsing as";
            labelUser.Text = signedIn ? UserStore.Current.Name : "Guest";
            labelAvatar.Text = labelUser.Text.Substring(0, 1).ToUpperInvariant();
        }

        private void GoTo(Form next)
        {
            Form current = FindForm();
            if (current != null && current.GetType() == next.GetType())
            {
                next.Dispose(); // already on this screen
                return;
            }
            Nav.Go(current, next);
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            GoTo(new FormOne());
        }

        private void buttonCategories_Click(object sender, EventArgs e)
        {
            GoTo(new FormCategory());
        }

        private void buttonHandbags_Click(object sender, EventArgs e)
        {
            GoTo(new FormThree());
        }

        private void buttonCart_Click(object sender, EventArgs e)
        {
            GoTo(new FormCart());
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            GoTo(new FormTwo());
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            // Sign In and Sign Out both go to the sign-in screen, which signs out on open
            GoTo(new FormMain());
        }
    }
}
