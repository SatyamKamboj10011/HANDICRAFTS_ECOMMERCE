using System;
using System.ComponentModel;
using System.Windows.Forms;
using Form_Loading.Models;
using Form_Loading.UI;

namespace Form_Loading
{
    /// <summary>
    /// Sidebar shown on every shop screen: logo (home), login/sign out,
    /// collections, cart with a live item count, and the signed-in user.
    /// </summary>
    public partial class Navigation : UserControl
    {
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

        private void UpdateState(object sender, EventArgs e)
        {
            buttonCart.Text = Cart.Count == 0 ? "🛒 Cart" : "🛒 Cart (" + Cart.Count + ")";
            buttonLogin.Text = UserStore.IsSignedIn ? "👤 Sign Out" : "👤 Login";
            labelUser.Text = UserStore.IsSignedIn ? UserStore.Current.Name : "Guest";
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

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            GoTo(new FormMain());
        }

        private void buttonHandbags_Click(object sender, EventArgs e)
        {
            GoTo(new FormThree());
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            GoTo(new FormTwo());
        }

        private void buttonCategories_Click(object sender, EventArgs e)
        {
            GoTo(new FormCategory());
        }

        private void buttonCart_Click(object sender, EventArgs e)
        {
            GoTo(new FormCart());
        }
    }
}
