using System.Windows.Forms;
using Form_Loading.UI;

namespace Form_Loading
{
    /// <summary>Decorative items collection.</summary>
    public partial class FormDecoration : Form
    {
        public FormDecoration()
        {
            InitializeComponent();
            ProductCards.BindDesignerCards(this);
        }
    }
}
