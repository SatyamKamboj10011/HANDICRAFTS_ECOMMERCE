using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Form_Loading.Models;
using Form_Loading.UI;

namespace Form_Loading
{
    /// <summary>
    /// Shopping cart: item list with quantity controls on the left,
    /// order summary and checkout on the right.
    /// </summary>
    public partial class FormCart : Form
    {
        private const string ColImage = "Image", ColName = "Item", ColMinus = "Minus",
            ColQty = "Qty", ColPlus = "Plus", ColTotal = "Total", ColRemove = "Remove";

        private DataGridView grid;
        private Panel emptyState;
        private Label subtotalValue, totalValue, itemCount;
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
            int right = ClientSize.Width - 24;
            const int summaryWidth = 240;
            const int top = 84;
            int listWidth = right - left - summaryWidth - 16;
            int listHeight = ClientSize.Height - top - 16;

            Theme.AddPageHeader(this, left, "Your cart", "Review your items before checking out");

            // ---- Item list -------------------------------------------------
            grid = new DataGridView
            {
                Location = new Point(left, top),
                Size = new Size(listWidth, listHeight),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Theme.Surface,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Theme.Border,
                EnableHeadersVisualStyles = false,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                ColumnHeadersHeight = 38,
                Font = Theme.BodyFont,
            };
            grid.RowTemplate.Height = 72;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Theme.Surface;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Theme.Muted;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9F);
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Theme.Surface;
            grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
            grid.DefaultCellStyle.BackColor = Theme.Surface;
            grid.DefaultCellStyle.ForeColor = Theme.Ink;
            grid.DefaultCellStyle.SelectionBackColor = Theme.Surface; // no blue selection bar
            grid.DefaultCellStyle.SelectionForeColor = Theme.Ink;
            grid.DefaultCellStyle.Padding = new Padding(8, 0, 0, 0);

            grid.Columns.Add(new DataGridViewImageColumn { Name = ColImage, HeaderText = "", Width = 68, ImageLayout = DataGridViewImageCellLayout.Zoom });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = ColName, HeaderText = "ITEM", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            grid.Columns.Add(StepperColumn(ColMinus, "−"));
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = ColQty, HeaderText = "QTY", Width = 38 });
            grid.Columns.Add(StepperColumn(ColPlus, "+"));
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = ColTotal, HeaderText = "TOTAL", Width = 80 });
            grid.Columns.Add(new DataGridViewButtonColumn { Name = ColRemove, HeaderText = "", Text = "✕", UseColumnTextForButtonValue = true, Width = 34, FlatStyle = FlatStyle.Flat });

            grid.Columns[ColImage].DefaultCellStyle.Padding = new Padding(8, 8, 0, 8);
            grid.Columns[ColName].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            grid.Columns[ColName].DefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F);
            grid.Columns[ColQty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.Columns[ColQty].DefaultCellStyle.Padding = Padding.Empty;
            grid.Columns[ColQty].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.Columns[ColQty].HeaderCell.Style.Padding = Padding.Empty;
            grid.Columns[ColTotal].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grid.Columns[ColTotal].DefaultCellStyle.ForeColor = Theme.Brand;
            grid.Columns[ColTotal].DefaultCellStyle.SelectionForeColor = Theme.Brand;
            grid.Columns[ColRemove].DefaultCellStyle.ForeColor = Theme.Muted;
            grid.Columns[ColRemove].DefaultCellStyle.Padding = new Padding(2, 20, 6, 20);
            grid.CellContentClick += GridButtonClicked;

            // ---- Empty state -----------------------------------------------
            emptyState = new Panel
            {
                Location = grid.Location,
                Size = grid.Size,
                BackColor = Theme.Surface,
                Visible = false,
            };
            emptyState.Controls.Add(new Label
            {
                Text = "Your cart is empty",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Theme.Ink,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 120),
                Size = new Size(emptyState.Width, 32),
            });
            emptyState.Controls.Add(new Label
            {
                Text = "Browse our collections to find something handmade you'll love.",
                Font = Theme.BodyFont,
                ForeColor = Theme.Muted,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 156),
                Size = new Size(emptyState.Width, 24),
            });
            Button browse = Theme.PrimaryButton("Start Shopping");
            browse.Location = new Point((emptyState.Width - browse.Width) / 2, 200);
            browse.Click += (s, e) => Nav.Go(this, new FormCategory());
            emptyState.Controls.Add(browse);

            // ---- Order summary ---------------------------------------------
            int sx = right - summaryWidth;
            var summary = new Panel
            {
                Location = new Point(sx, top),
                Size = new Size(summaryWidth, 300),
                BackColor = Theme.Surface,
            };
            int pad = 20, inner = summaryWidth - pad * 2;

            summary.Controls.Add(new Label
            {
                Text = "Order summary",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Theme.Ink,
                AutoSize = true,
                Location = new Point(pad - 2, 18),
            });
            Label unused;
            itemCount = SummaryRow(summary, "Items", 60, pad, inner, out unused);
            subtotalValue = SummaryRow(summary, "Subtotal", 88, pad, inner, out unused);
            Label delivery = SummaryRow(summary, "Delivery", 116, pad, inner, out unused);
            delivery.Text = "Free";
            delivery.ForeColor = Theme.Accent;

            summary.Controls.Add(new Label // divider
            {
                BackColor = Theme.Border,
                Location = new Point(pad, 148),
                Size = new Size(inner, 1),
            });
            Label totalCaption;
            totalValue = SummaryRow(summary, "Total", 160, pad, inner, out totalCaption);
            totalCaption.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            totalCaption.ForeColor = Theme.Ink;
            totalValue.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            totalValue.ForeColor = Theme.Brand;

            checkout = Theme.PrimaryButton("Checkout");
            checkout.Location = new Point(pad, 200);
            checkout.Size = new Size(inner, 44);
            checkout.Click += Checkout;
            summary.Controls.Add(checkout);

            Button keepShopping = Theme.LinkButton("Continue shopping", Theme.Accent);
            keepShopping.Location = new Point(pad - 4, 254);
            keepShopping.Click += (s, e) => Nav.Go(this, new FormCategory());
            summary.Controls.Add(keepShopping);

            clear = Theme.LinkButton("Clear cart", Theme.Danger);
            clear.Click += ClearCart;
            summary.Controls.Add(clear);
            clear.Location = new Point(summaryWidth - pad - clear.PreferredSize.Width + 4, 254);

            Controls.AddRange(new Control[] { grid, emptyState, summary });
        }

        private static DataGridViewButtonColumn StepperColumn(string name, string text)
        {
            var col = new DataGridViewButtonColumn
            {
                Name = name,
                HeaderText = "",
                Text = text,
                UseColumnTextForButtonValue = true,
                Width = 32,
                FlatStyle = FlatStyle.Flat,
            };
            col.DefaultCellStyle.Padding = new Padding(2, 22, 2, 22);
            col.DefaultCellStyle.ForeColor = Theme.Accent;
            col.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            return col;
        }

        /// <summary>Adds a "caption ........ value" row to the summary card and returns the value label.</summary>
        private static Label SummaryRow(Control parent, string caption, int y, int x, int width, out Label captionLabel)
        {
            captionLabel = new Label
            {
                Text = caption,
                Font = Theme.BodyFont,
                ForeColor = Theme.Muted,
                AutoSize = true,
                Location = new Point(x, y),
            };
            var value = new Label
            {
                Font = new Font("Segoe UI Semibold", 10F),
                ForeColor = Theme.Ink,
                TextAlign = ContentAlignment.TopRight,
                Location = new Point(x + width - 140, y),
                Size = new Size(140, 26),
            };
            parent.Controls.Add(captionLabel);
            parent.Controls.Add(value);
            return value;
        }

        private void ShowCart(object sender, EventArgs e)
        {
            grid.Rows.Clear();
            foreach (CartItem item in Cart.Items)
            {
                Image thumb = ImageFit.Cover(item.Product.Image, new Size(60, 56));
                string name = item.Product.Name + Environment.NewLine + item.Product.PriceText + " each";
                int row = grid.Rows.Add(thumb, name, null, item.Quantity, null,
                    item.LineTotal.ToString("C2", Catalog.Currency), null);
                grid.Rows[row].Tag = item.Product.Id;
            }
            grid.ClearSelection();

            bool empty = Cart.Count == 0;
            grid.Visible = !empty;
            emptyState.Visible = empty;
            checkout.Enabled = !empty;
            checkout.BackColor = empty ? Theme.Border : Theme.Accent;
            clear.Visible = !empty;

            string total = Cart.Total.ToString("C2", Catalog.Currency);
            itemCount.Text = Cart.Count.ToString();
            subtotalValue.Text = total;
            totalValue.Text = total;
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
