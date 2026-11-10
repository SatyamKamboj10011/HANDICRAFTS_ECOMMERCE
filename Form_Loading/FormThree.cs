using System;
using System.Windows.Forms;
using Form_Loading.UI;

namespace Form_Loading
{
    /// <summary>Handbags collection.</summary>
    public partial class FormThree : Form
    {
        public FormThree()
        {
            InitializeComponent();
            ProductCards.BindDesignerCards(this);
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            Nav.Go(this, new FormOne());
        }
    }
}
