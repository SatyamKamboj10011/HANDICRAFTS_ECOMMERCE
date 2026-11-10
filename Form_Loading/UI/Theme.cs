using System.Drawing;
using System.Windows.Forms;

namespace Form_Loading.UI
{
    /// <summary>Colours and fonts used by screens built in code, matching the designer screens.</summary>
    public static class Theme
    {
        public static readonly Color Heading = Color.FromArgb(64, 64, 0);
        public static readonly Color Price = Color.Green;
        public static readonly Color Accent = Color.MediumAquamarine;
        public static readonly Color Paper = Color.FromArgb(236, 229, 214);

        public static readonly Font HeadingFont = new Font("Cheddar Gothic Rough", 18F, FontStyle.Italic);
        public static readonly Font TitleFont = new Font("Georgia", 16F, FontStyle.Bold);
        public static readonly Font BodyFont = new Font("Segoe UI", 10F);
        public static readonly Font PriceFont = new Font("Georgia", 14F, FontStyle.Bold);
        public static readonly Font ButtonFont = new Font("Segoe UI", 10F, FontStyle.Bold);

        public static Button PrimaryButton(string text)
        {
            return new Button
            {
                Text = text,
                BackColor = Accent,
                Font = ButtonFont,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(150, 40),
                Cursor = Cursors.Hand,
            };
        }

        public static Button SecondaryButton(string text)
        {
            Button b = PrimaryButton(text);
            b.BackColor = Color.White;
            return b;
        }

        public static Image PaperBackground
        {
            get { return Properties.Resources.paper_1074131_19201; }
        }
    }
}
