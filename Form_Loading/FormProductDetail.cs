using System;
using System.Drawing;
using System.Windows.Forms;
using Form_Loading.Models;
using Form_Loading.UI;

namespace Form_Loading
{
    /// <summary>
    /// Product page: large photo, description, quantity stepper and Add to Cart.
    /// Opens as a dialog over the collection page.
    /// </summary>
    public class FormProductDetail : Form
    {
        private readonly Product product;
        private readonly Label quantityLabel;
        private readonly Label status;
        private int quantity = 1;
        private bool goToCart;

        public static void Open(Form owner, Product product)
        {
            using (var detail = new FormProductDetail(product))
            {
                detail.ShowDialog(owner);
                if (detail.goToCart && owner != null)
                    Nav.Go(owner, new FormCart());
            }
        }

        public FormProductDetail(Product product)
        {
            this.product = product;

            Text = product.Name + " - Handcraft Heaven";
            ClientSize = new Size(780, 430);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Theme.Surface;
            DoubleBuffered = true;

            // Photo fills the left half edge to edge
            var photoSize = new Size(380, 430);
            var photo = new PictureBox
            {
                Image = ImageFit.Cover(product.Image, photoSize),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = Point.Empty,
                Size = photoSize,
            };

            int x = 412;
            int width = ClientSize.Width - x - 32;

            var category = new Label
            {
                Text = product.Category.ToUpperInvariant(),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Theme.Accent,
                Location = new Point(x, 34),
                AutoSize = true,
            };
            var name = new Label
            {
                Text = product.Name,
                UseMnemonic = false,
                Font = Theme.TitleFont,
                ForeColor = Theme.Ink,
                Location = new Point(x - 2, 56),
                Size = new Size(width, 64),
            };
            var price = new Label
            {
                Text = product.PriceText,
                Font = Theme.PriceFont,
                ForeColor = Theme.Brand,
                Location = new Point(x - 2, 122),
                AutoSize = true,
            };
            var description = new Label
            {
                Text = product.Description,
                UseMnemonic = false,
                Font = Theme.BodyFont,
                ForeColor = Theme.Muted,
                Location = new Point(x, 166),
                Size = new Size(width, 72),
            };
            var delivery = new Label
            {
                Text = "✓  Free delivery across New Zealand",
                Font = new Font("Segoe UI Semibold", 9F),
                ForeColor = Theme.Accent,
                Location = new Point(x, 242),
                AutoSize = true,
            };

            // Quantity stepper:  [ − ]  1  [ + ]
            var qtyCaption = new Label
            {
                Text = "Quantity",
                Font = new Font("Segoe UI Semibold", 9.5F),
                ForeColor = Theme.Ink,
                Location = new Point(x, 290),
                AutoSize = true,
            };
            Button minus = StepperButton("−", new Point(x + 80, 283));
            quantityLabel = new Label
            {
                Text = "1",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Theme.Ink,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(x + 114, 283),
                Size = new Size(40, 32),
            };
            Button plus = StepperButton("+", new Point(x + 156, 283));
            minus.Click += (s, e) => SetQuantity(quantity - 1);
            plus.Click += (s, e) => SetQuantity(quantity + 1);

            Button add = Theme.PrimaryButton("Add to Cart");
            add.Location = new Point(x, 336);
            add.Size = new Size(176, 44);
            add.Click += AddToCart;

            Button viewCart = Theme.SecondaryButton("View Cart");
            viewCart.Location = new Point(x + 188, 336);
            viewCart.Size = new Size(148, 44);
            viewCart.Click += (s, e) => { goToCart = true; Close(); };

            status = new Label
            {
                Font = new Font("Segoe UI Semibold", 9F),
                ForeColor = Theme.Accent,
                Location = new Point(x, 392),
                AutoSize = true,
            };

            Controls.AddRange(new Control[] { photo, category, name, price, description, delivery,
                qtyCaption, minus, quantityLabel, plus, add, viewCart, status });
            AcceptButton = add;
        }

        private static Button StepperButton(string text, Point location)
        {
            Button b = Theme.SecondaryButton(text);
            b.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            b.Location = location;
            b.Size = new Size(32, 32);
            b.Padding = Padding.Empty;
            return b;
        }

        private void SetQuantity(int value)
        {
            quantity = Math.Max(1, Math.Min(Cart.MaxQuantity, value));
            quantityLabel.Text = quantity.ToString();
        }

        private void AddToCart(object sender, EventArgs e)
        {
            Cart.Add(product, quantity);
            status.Text = string.Format("✓  Added {0} to your cart  ·  {1} item{2} in cart",
                quantity, Cart.Count, Cart.Count == 1 ? "" : "s");
        }
    }
}
