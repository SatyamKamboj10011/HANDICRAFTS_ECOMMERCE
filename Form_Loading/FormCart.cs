using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Form_Loading.Models;
using Form_Loading.UI;

namespace Form_Loading
{
    /// <summary>
    /// Shopping cart: change quantities, remove items, see the total and check out.
    /// </summary>
    public partial class FormCart : Form
    {
        private const string ColImage = "Image", ColName = "Item", ColPrice = "Price", ColMinus = "Minus",
            ColQty = "Qty", ColPlus = "Plus", ColTotal = "Total", ColRemove = "Remove";

        private DataGridView grid;
        private Label summary;
        private Panel emptyState;
        private Button checkout, clear;

        public FormCart()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Built after the form has scaled to the screen's DPI, using its real size
            BuildLayout();
            ShowCart(this, EventArgs.Empty);

            Cart.Changed += ShowCart;
            FormClosed += (s, args) => Cart.Changed -= ShowCart;
        }

        private void BuildLayout()
        {
            int left = navigation1.Right + 22;
            int width = ClientSize.Width - left - 24;
            int buttonsTop = ClientSize.Height - 56;
            int summaryTop = buttonsTop - 44;
            int gridTop = 60;

            var title = new Label
            {
                Text = "YOUR CART",
                Font = Theme.HeadingFont,
                ForeColor = Theme.Heading,
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(left + width / 2 - 70, 12),
            };

            grid = new DataGridView
            {
                Location = new Point(left, gridTop),
                Size = new Size(width, summaryTop - gridTop - 12),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                ColumnHeadersHeight = 34,
                Font = Theme.BodyFont,
            };
            grid.RowTemplate.Height = 64;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 245, 240);
            grid.DefaultCellStyle.SelectionForeColor = Color.Black;

            grid.Columns.Add(new DataGridViewImageColumn { Name = ColImage, HeaderText = "", Width = 80, ImageLayout = DataGridViewImageCellLayout.Zoom });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = ColName, HeaderText = "Item", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = ColPrice, HeaderText = "Price", Width = 85 });
            grid.Columns.Add(new DataGridViewButtonColumn { Name = ColMinus, HeaderText = "", Text = "−", UseColumnTextForButtonValue = true, Width = 36 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = ColQty, HeaderText = "Qty", Width = 48 });
            grid.Columns.Add(new DataGridViewButtonColumn { Name = ColPlus, HeaderText = "", Text = "+", UseColumnTextForButtonValue = true, Width = 36 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = ColTotal, HeaderText = "Total", Width = 90 });
            grid.Columns.Add(new DataGridViewButtonColumn { Name = ColRemove, HeaderText = "", Text = "Remove", UseColumnTextForButtonValue = true, Width = 80 });

            foreach (string col in new[] { ColPrice, ColQty, ColTotal })
                grid.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.Columns[ColTotal].DefaultCellStyle.Font = new Font(Theme.BodyFont, FontStyle.Bold);
            grid.CellContentClick += GridButtonClicked;

            emptyState = new Panel
            {
                Location = grid.Location,
                Size = grid.Size,
                BackColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                Visible = false,
            };
            var emptyText = new Label
            {
                Text = "Your cart is empty.\nBrowse our handcrafted collections to add something special.",
                Font = Theme.BodyFont,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 180,
            };
            Button browse = Theme.PrimaryButton("Start Shopping");
            browse.Location = new Point((emptyState.Width - browse.Width) / 2, 180);
            browse.Click += (s, e) => Nav.Go(this, new FormCategory());
            emptyState.Controls.Add(browse);
            emptyState.Controls.Add(emptyText);

            summary = new Label
            {
                Font = Theme.PriceFont,
                ForeColor = Theme.Heading,
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(left, summaryTop),
            };

            Button keepShopping = Theme.SecondaryButton("Continue Shopping");
            keepShopping.Width = 170;
            keepShopping.Location = new Point(left, buttonsTop);
            keepShopping.Click += (s, e) => Nav.Go(this, new FormCategory());

            clear = Theme.SecondaryButton("Clear Cart");
            clear.Location = new Point(left + 185, buttonsTop);
            clear.Click += ClearCart;

            checkout = Theme.PrimaryButton("Checkout ➜");
            checkout.Location = new Point(left + width - checkout.Width, buttonsTop);
            checkout.Click += Checkout;

            Controls.AddRange(new Control[] { title, grid, emptyState, summary, keepShopping, clear, checkout });
        }

        private void ShowCart(object sender, EventArgs e)
        {
            grid.Rows.Clear();
            foreach (CartItem item in Cart.Items)
            {
                int row = grid.Rows.Add(item.Product.Image, item.Product.Name, item.Product.PriceText,
                    null, item.Quantity, null, item.LineTotal.ToString("C2", Catalog.Currency), null);
                grid.Rows[row].Tag = item.Product.Id;
            }

            bool empty = Cart.Count == 0;
            grid.Visible = !empty;
            emptyState.Visible = empty;
            checkout.Enabled = !empty;
            clear.Enabled = !empty;

            summary.Text = empty
                ? "Total: " + 0m.ToString("C2", Catalog.Currency)
                : string.Format("{0} item{1}  ·  Free delivery  ·  Total: {2}",
                    Cart.Count, Cart.Count == 1 ? "" : "s", Cart.Total.ToString("C2", Catalog.Currency));
        }

        private void GridButtonClicked(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string id = (string)grid.Rows[e.RowIndex].Tag;
            CartItem item = Cart.Items.First(i => i.Product.Id == id);
            string column = grid.Columns[e.ColumnIndex].Name;

            if (column == ColPlus)
                Cart.SetQuantity(id, item.Quantity + 1);
            else if (column == ColMinus)
                Cart.SetQuantity(id, item.Quantity - 1);
            else if (column == ColRemove)
                Cart.Remove(id);
        }

        private void ClearCart(object sender, EventArgs e)
        {
            if (MessageBox.Show("Remove everything from your cart?", "Clear Cart",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Cart.Clear();
        }

        private void Checkout(object sender, EventArgs e)
        {
            if (!UserStore.IsSignedIn)
            {
                if (MessageBox.Show("Please sign in to place your order.\nYour cart will be saved.\n\nGo to sign in now?",
                        "Sign In Required", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    Nav.Go(this, new FormMain());
                return;
            }

            string total = Cart.Total.ToString("C2", Catalog.Currency);
            if (MessageBox.Show(string.Format("Place order for {0} item{1}, total {2}?",
                    Cart.Count, Cart.Count == 1 ? "" : "s", total),
                    "Confirm Order", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            string orderNumber = "HH-" + DateTime.Now.ToString("yyMMdd-HHmmss");
            Cart.Clear();
            MessageBox.Show(string.Format("Thank you, {0}!\n\nOrder {1} has been placed ({2}).\nA confirmation will be sent to {3}.",
                    UserStore.Current.Name, orderNumber, total, UserStore.Current.Email),
                "Order Placed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
