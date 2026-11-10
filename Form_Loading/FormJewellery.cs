using System.Windows.Forms;
using Form_Loading.UI;

namespace Form_Loading
{
    /// <summary>Jewellery collection.</summary>
    public partial class FormJewellery : Form
    {
        public FormJewellery()
        {
            InitializeComponent();
            ProductCards.BindDesignerCards(this);
        }
    }
}
