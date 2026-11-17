using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Form_Loading.Models;

namespace Form_Loading.UI
{
    /// <summary>
    /// Builds the product cards used on every collection page and fills a
    /// grid with them straight from the catalogue.
    /// </summary>
    public static class ProductCards
    {
        public const int Columns = 4;
        private const int Gap = 13;

        /// <summary>
        /// Fills a grid with a card for every product in the category, sized so
        /// exactly four fit across at any screen scaling. Call it from OnLoad,
        /// once the form has its final size. Returns how many cards were added.
        /// </summary>
        public static int FillGrid(FlowLayoutPanel grid, string category)
        {
            int available = grid.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 2;
            int width = available / Columns - Gap;
            var size = new Size(width, width * 210 / 182);

            grid.SuspendLayout();
            int count = 0;
            foreach (Product product in Catalog.InCategory(category))
            {
                grid.Controls.Add(CreateCard(product, size));
                count++;
            }
            grid.ResumeLayout();
            return count;
        }

        /// <summary>One product card: photo, name, price and an Add button.</summary>
        public static Control CreateCard(Product product, Size cardSize)
        {
            Size CardSize = cardSize;
            int ImageHeight = cardSize.Height * 116 / 210;

            var card = new CardPanel
            {
                Size = CardSize,
                Margin = new Padding(0, 0, Gap, Gap),
                BackColor = Theme.Surface,
                Cursor = Cursors.Hand,
                Tag = product.Id,
            };

            var photo = new PictureBox
            {
                Image = ImageFit.Cover(product.Image, new Size(CardSize.Width, ImageHeight)),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(1, 1),
                Size = new Size(CardSize.Width - 2, ImageHeight),
                Cursor = Cursors.Hand,
            };

            var name = new Label
            {
                Text = product.Name,
                UseMnemonic = false,
                AutoEllipsis = true,
                Font = Theme.CardTitleFont,
                ForeColor = Theme.Ink,
                Location = new Point(10, ImageHeight + 9),
                Size = new Size(CardSize.Width - 20, 22),
                Cursor = Cursors.Hand,
            };

            var delivery = new Label
            {
                Text = "Free delivery",
                Font = Theme.SmallFont,
                ForeColor = Theme.Muted,
                AutoSize = true,
                Location = new Point(11, ImageHeight + 31),
            };

            var price = new Label
            {
                Text = product.PriceText,
                Font = Theme.CardPriceFont,
                ForeColor = Theme.Brand,
                AutoSize = true,
                Location = new Point(9, CardSize.Height - 38),
            };

            Button add = Theme.PrimaryButton("Add");
            add.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            add.Size = new Size(70, 30);
            add.Location = new Point(CardSize.Width - add.Width - 10, CardSize.Height - 40);
            add.Name = "buttonAdd";
            add.Click += (s, e) => AddWithFeedback(add, product);

            new ToolTip().SetToolTip(name, product.Name);

            card.Controls.AddRange(new Control[] { photo, name, delivery, price, add });

            // Clicking anywhere except the button opens the product page
            EventHandler open = (s, e) => FormProductDetail.Open(card.FindForm(), product);
            foreach (Control c in new Control[] { card, photo, name, delivery, price })
                c.Click += open;

            return card;
        }

        /// <summary>Adds to the cart and briefly confirms on the button itself.</summary>
        public static void AddWithFeedback(Button button, Product product)
        {
            Cart.Add(product);

            string original = button.Text;
            Color originalBack = button.BackColor;
            button.Text = "Added ✓";
            button.BackColor = Theme.AccentDark;
            button.Enabled = false;

            var timer = new Timer { Interval = 900 };
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                timer.Dispose();
                if (!button.IsDisposed)
                {
                    button.Text = original;
                    button.BackColor = originalBack;
                    button.Enabled = true;
                }
            };
            timer.Start();
        }

        /// <summary>A white card with a thin border that turns teal on hover.</summary>
        private sealed class CardPanel : Panel
        {
            private bool hover;

            public CardPanel()
            {
                DoubleBuffered = true;
                ResizeRedraw = true;
            }

            protected override void OnControlAdded(ControlEventArgs e)
            {
                base.OnControlAdded(e);
                e.Control.MouseEnter += (s, args) => SetHover(true);
                e.Control.MouseLeave += (s, args) => SetHover(ClientRectangle.Contains(PointToClient(Cursor.Position)));
            }

            protected override void OnMouseEnter(EventArgs e)
            {
                base.OnMouseEnter(e);
                SetHover(true);
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                base.OnMouseLeave(e);
                SetHover(ClientRectangle.Contains(PointToClient(Cursor.Position)));
            }

            private void SetHover(bool value)
            {
                if (hover == value) return;
                hover = value;
                Invalidate();
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                using (var pen = new Pen(hover ? Theme.Accent : Theme.Border))
                    e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
            }
        }
    }
}
