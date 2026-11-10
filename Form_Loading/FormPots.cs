using System.Windows.Forms;
using Form_Loading.Models;
using Form_Loading.UI;

namespace Form_Loading
{
    /// <summary>Handcrafted pots and ceramics. Cards are generated from the catalogue.</summary>
    public partial class FormPots : Form
    {
        public FormPots()
        {
            InitializeComponent();

            foreach (Product product in Catalog.InCategory(Catalog.Pots))
                flowProducts.Controls.Add(ProductCards.CreateCard(product));
        }
    }
}
