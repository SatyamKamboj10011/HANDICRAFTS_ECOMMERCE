using System;
using System.Drawing;
using System.Windows.Forms;
using Form_Loading.Models;
using Form_Loading.UI;

namespace Form_Loading
{
    /// <summary>
    /// Product page: large photo, description, quantity picker and Add to Cart.
    /// Opens as a dialog over the category screen.
    /// </summary>
    public class FormProductDetail : Form
    {
        private readonly Product product;
        private readonly NumericUpDown quantity;
        private readonly Label status;
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
            ClientSize = new Size(760, 420);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            BackgroundImage = Theme.PaperBackground;
            BackgroundImageLayout = ImageLayout.Stretch;

            var photo = new PictureBox
            {
                Image = product.Image,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                Location = new Point(24, 24),
                Size = new Size(340, 372),
            };

            int x = 392;
            int width = ClientSize.Width - x - 24;

            var category = new Label
            {
                Text = product.Category.ToUpperInvariant(),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.DimGray,
                BackColor = Color.Transparent,
                Location = new Point(x, 28),
                AutoSize = true,
            };
            var name = new Label
            {
                Text = product.Name,
                UseMnemonic = false,
                Font = Theme.TitleFont,
                ForeColor = Theme.Heading,
                BackColor = Color.Transparent,
                Location = new Point(x, 50),
                Size = new Size(width, 60),
            };
            var price = new Label
            {
                Text = product.PriceText,
                Font = Theme.PriceFont,
                ForeColor = Theme.Price,
                BackColor = Color.Transparent,
                Location = new Point(x, 112),
                AutoSize = true,
            };
            var description = new Label
            {
                Text = product.Description,
                UseMnemonic = false,
                Font = Theme.BodyFont,
                BackColor = Color.Transparent,
                Location = new Point(x, 150),
                Size = new Size(width, 80),
            };
            var delivery = new Label
            {
                Text = "✔ Free delivery across New Zealand",
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = Theme.Price,
                BackColor = Color.Transparent,
                Location = new Point(x, 236),
                AutoSize = true,
            };
            var qtyLabel = new Label
            {
                Text = "Quantity",
                Font = Theme.BodyFont,
                BackColor = Color.Transparent,
                Location = new Point(x, 279),
                AutoSize = true,
            };
            quantity = new NumericUpDown
            {
                Minimum = 1,
                Maximum = Cart.MaxQuantity,
                Value = 1,
                Font = Theme.BodyFont,
                Location = new Point(x + 75, 276),
                Width = 60,
            };

            Button add = Theme.PrimaryButton("Add to Cart 🛒");
            add.Location = new Point(x, 320);
            add.Click += AddToCart;

            Button viewCart = Theme.SecondaryButton("View Cart");
            viewCart.Location = new Point(x + 165, 320);
            viewCart.Click += (s, e) => { goToCart = true; Close(); };

            status = new Label
            {
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Theme.Price,
                BackColor = Color.Transparent,
                Location = new Point(x, 370),
                AutoSize = true,
            };

            Controls.AddRange(new Control[] { photo, category, name, price, description, delivery, qtyLabel, quantity, add, viewCart, status });
            AcceptButton = add;
        }

        private void AddToCart(object sender, EventArgs e)
        {
            int qty = (int)quantity.Value;
            Cart.Add(product, qty);
            status.Text = string.Format("Added {0} to your cart ✔  ({1} item{2} in cart)",
                qty, Cart.Count, Cart.Count == 1 ? "" : "s");
        }
    }
}
