using System;
using System.Windows.Forms;
using Form_Loading.UI;

namespace Form_Loading
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ShopContext keeps the app alive while any screen is open,
            // so screens can be opened and closed freely as the user navigates.
            Application.Run(new ShopContext(new FormMain()));
        }
    }
}
