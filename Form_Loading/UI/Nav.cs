using System;
using System.Windows.Forms;
using Form_Loading.Models;

namespace Form_Loading.UI
{
    /// <summary>
    /// Moves between screens. The new screen opens where the old one was,
    /// and the app exits once the last screen is closed.
    /// </summary>
    public static class Nav
    {
        public static void Go(Form from, Form to)
        {
            if (from != null)
            {
                to.StartPosition = FormStartPosition.Manual;
                to.Location = from.Location;
            }
            else
            {
                to.StartPosition = FormStartPosition.CenterScreen;
            }

            Track(to);
            to.Show();
            from?.Close();
        }

        public static Form ForCategory(string category)
        {
            switch (category)
            {
                case Catalog.Handbags: return new FormThree();
                case Catalog.Jewellery: return new FormJewellery();
                case Catalog.Decor: return new FormDecoration();
                case Catalog.Pots: return new FormPots();
                default: throw new ArgumentException("Unknown category: " + category);
            }
        }

        internal static void Track(Form form)
        {
            form.FormClosed += (s, e) =>
            {
                if (Application.OpenForms.Count == 0)
                    Application.ExitThread();
            };
        }
    }

    /// <summary>Keeps the app running while any screen is open.</summary>
    public class ShopContext : ApplicationContext
    {
        public ShopContext(Form start)
        {
            Nav.Track(start);
            start.StartPosition = FormStartPosition.CenterScreen;
            start.Show();
        }
    }
}
