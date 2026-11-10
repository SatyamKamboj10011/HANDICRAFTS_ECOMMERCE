using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Form_Loading.Models;

namespace Form_Loading.UI
{
    /// <summary>
    /// Connects product cards to the catalogue: fills in the real name and
    /// price, opens the product page when the picture is clicked, and adds
    /// to the cart when the 🛒 icon is clicked.
    /// </summary>
    public static class ProductCards
    {
        private const string CartIcon = "🛒";

        // Card panels on the designer screens, in display order, and the product each one shows
        private static readonly Dictionary<string, string[]> designerCards = new Dictionary<string, string[]>
        {
            { nameof(FormThree),      new[] { "panel1:HB01", "panel2:HB02", "panel3:HB03", "panel4:HB04", "panel7:HB05", "panel5:HB06", "panel9:HB07", "panel8:HB08" } },
            { nameof(FormJewellery),  new[] { "panel2:JW01", "panel3:JW02", "panel5:JW03", "panel4:JW04", "panel6:JW05", "panel9:JW06", "panel11:JW07", "panel15:JW08" } },
            { nameof(FormDecoration), new[] { "panel4:DC01", "panel1:DC02", "panel3:DC03", "panel2:DC04", "panel5:DC05", "panel7:DC06", "panel6:DC07", "panel8:DC08" } },
        };

        /// <summary>Wires up every product card on a designer-built category screen.</summary>
        public static void BindDesignerCards(Form form)
        {
            foreach (string entry in designerCards[form.GetType().Name])
            {
                string[] parts = entry.Split(':');
                Control panel = FindOutsideSidebar(form, parts[0]);
                Product product = Catalog.Find(parts[1]);
                if (panel != null && product != null)
                    Bind(panel, product);
            }
        }

        /// <summary>
        /// Finds a control by name, ignoring the sidebar (which has its own
        /// panel1, panel2... that would otherwise match first).
        /// </summary>
        private static Control FindOutsideSidebar(Control parent, string name)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Navigation) continue;
                if (c.Name == name) return c;
                Control found = FindOutsideSidebar(c, name);
                if (found != null) return found;
            }
            return null;
        }

        /// <summary>Fills one card panel with a product and makes it interactive.</summary>
        public static void Bind(Control panel, Product product)
        {
            ToolTip tip = new ToolTip();

            foreach (Control c in panel.Controls)
            {
                if (c is PictureBox pic)
                {
                    pic.Image = product.Image ?? pic.Image;
                    pic.Cursor = Cursors.Hand;
                    pic.Click += (s, e) => FormProductDetail.Open(panel.FindForm(), product);
                    tip.SetToolTip(pic, "View details");
                }
                else if (c is Label label)
                {
                    label.UseMnemonic = false; // show "&" literally in names
                    if (label.Text == CartIcon)
                    {
                        label.Cursor = Cursors.Hand;
                        tip.SetToolTip(label, "Add to cart");
                        label.Click += (s, e) => AddWithFeedback(label, product);
                    }
                    else if (label.Text.StartsWith("$"))
                    {
                        label.AutoSize = true;
                        label.Text = product.PriceText;
                    }
                    else if (label.Text == "Woolen Hand Bag")
                    {
                        // Long names get an ellipsis instead of spilling outside the card
                        label.AutoSize = false;
                        label.AutoEllipsis = true;
                        label.Width = panel.ClientSize.Width - label.Left - 4;
                        label.Text = product.Name;
                        tip.SetToolTip(label, product.Name);
                    }
                }
            }
        }

        /// <summary>Adds to the cart and briefly swaps the icon to a tick so the click feels confirmed.</summary>
        public static void AddWithFeedback(Label icon, Product product)
        {
            Cart.Add(product);

            string original = icon.Text;
            Color originalColor = icon.ForeColor;
            icon.Text = "✔";
            icon.ForeColor = Theme.Price;

            Timer timer = new Timer { Interval = 900 };
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                timer.Dispose();
                if (!icon.IsDisposed)
                {
                    icon.Text = original;
                    icon.ForeColor = originalColor;
                }
            };
            timer.Start();
        }

        /// <summary>
        /// Builds a card in the same style as the designer cards, for screens
        /// created in code (used by the Pots page).
        /// </summary>
        public static Panel CreateCard(Product product)
        {
            Panel card = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                Size = new Size(160, 197),
                Margin = new Padding(10),
            };
            card.Controls.Add(new PictureBox
            {
                BorderStyle = BorderStyle.Fixed3D,
                Location = new Point(9, 9),
                Size = new Size(136, 106),
                SizeMode = PictureBoxSizeMode.StretchImage,
            });
            card.Controls.Add(new Label
            {
                Text = "Woolen Hand Bag",
                Font = new Font("Dubai", 9F),
                Location = new Point(5, 118),
                Size = new Size(140, 25),
            });
            card.Controls.Add(new Label
            {
                Text = "$",
                Font = new Font("Georgia", 9F, FontStyle.Bold),
                ForeColor = Theme.Price,
                Location = new Point(6, 140),
            });
            card.Controls.Add(new Label
            {
                Text = "Free Delivery",
                Font = new Font("Gabriola", 7.8F, FontStyle.Bold),
                Location = new Point(6, 158),
                Size = new Size(90, 23),
            });
            card.Controls.Add(new Label
            {
                Text = CartIcon,
                Font = new Font("Microsoft Sans Serif", 10.8F),
                Location = new Point(114, 162),
                Size = new Size(32, 22),
            });

            Bind(card, product);
            return card;
        }
    }
}
