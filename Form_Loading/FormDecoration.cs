using System.Windows.Forms;
using Form_Loading.Models;
using Form_Loading.UI;

namespace Form_Loading
{
    /// <summary>Decorative Items collection. Cards are generated from the catalogue.</summary>
    public partial class FormDecoration : Form
    {
        public FormDecoration()
        {
            InitializeComponent();
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            // Cards are sized from the grid's final width, so they're added after scaling
            int count = ProductCards.FillGrid(flowProducts, Catalog.Decor);
            labelSubtitle.Text = count + " items  ·  " + labelSubtitle.Text;
        }
    }
}
