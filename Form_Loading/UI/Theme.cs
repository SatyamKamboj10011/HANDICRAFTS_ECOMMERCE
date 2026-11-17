using System.Drawing;
using System.Windows.Forms;

namespace Form_Loading.UI
{
    /// <summary>
    /// The app's design system: one palette, one type scale and shared
    /// button styles, so every screen looks like part of the same product.
    /// </summary>
    public static class Theme
    {
        // Palette
        public static readonly Color Background = Color.FromArgb(247, 243, 236); // warm cream page
        public static readonly Color Surface = Color.White;                      // cards
        public static readonly Color Border = Color.FromArgb(230, 223, 212);
        public static readonly Color Ink = Color.FromArgb(38, 38, 38);           // headings, body text
        public static readonly Color Muted = Color.FromArgb(110, 110, 110);      // secondary text
        public static readonly Color Accent = Color.FromArgb(42, 157, 143);      // teal: actions
        public static readonly Color AccentDark = Color.FromArgb(33, 128, 116);
        public static readonly Color AccentTint = Color.FromArgb(232, 245, 243);
        public static readonly Color Brand = Color.FromArgb(92, 58, 33);         // warm brown: prices, brand
        public static readonly Color Danger = Color.FromArgb(192, 57, 43);

        // Type scale
        public static readonly Font PageTitleFont = new Font("Segoe UI", 18F, FontStyle.Bold);
        public static readonly Font SubtitleFont = new Font("Segoe UI", 10F);
        public static readonly Font TitleFont = new Font("Segoe UI", 16F, FontStyle.Bold);
        public static readonly Font CardTitleFont = new Font("Segoe UI Semibold", 10F);
        public static readonly Font BodyFont = new Font("Segoe UI", 10F);
        public static readonly Font SmallFont = new Font("Segoe UI", 8.5F);
        public static readonly Font PriceFont = new Font("Segoe UI", 14F, FontStyle.Bold);
        public static readonly Font CardPriceFont = new Font("Segoe UI", 11F, FontStyle.Bold);
        public static readonly Font ButtonFont = new Font("Segoe UI", 10F, FontStyle.Bold);

        /// <summary>Filled teal button for the main action on a screen.</summary>
        public static Button PrimaryButton(string text)
        {
            var b = new Button
            {
                Text = text,
                BackColor = Accent,
                ForeColor = Color.White,
                Font = ButtonFont,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(160, 42),
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false,
            };
            b.FlatAppearance.BorderSize = 0;
            b.FlatAppearance.MouseOverBackColor = AccentDark;
            return b;
        }

        /// <summary>Outlined button for secondary actions.</summary>
        public static Button SecondaryButton(string text)
        {
            Button b = PrimaryButton(text);
            b.BackColor = Surface;
            b.ForeColor = Accent;
            b.FlatAppearance.BorderSize = 1;
            b.FlatAppearance.BorderColor = Accent;
            b.FlatAppearance.MouseOverBackColor = AccentTint;
            return b;
        }

        /// <summary>Text-only button, e.g. "Remove".</summary>
        public static Button LinkButton(string text, Color color)
        {
            var b = new Button
            {
                Text = text,
                ForeColor = color,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                AutoSize = true,
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false,
            };
            b.FlatAppearance.BorderSize = 0;
            b.FlatAppearance.MouseOverBackColor = Background;
            b.FlatAppearance.MouseDownBackColor = Border;
            return b;
        }

        /// <summary>Page heading and subtitle, placed to the right of the sidebar.</summary>
        public static void AddPageHeader(Control page, int left, string title, string subtitle)
        {
            page.Controls.Add(new Label
            {
                Text = title,
                Font = PageTitleFont,
                ForeColor = Ink,
                AutoSize = true,
                Location = new Point(left - 3, 14),
            });
            page.Controls.Add(new Label
            {
                Text = subtitle,
                Font = SubtitleFont,
                ForeColor = Muted,
                AutoSize = true,
                Location = new Point(left, 52),
            });
        }
    }
}
