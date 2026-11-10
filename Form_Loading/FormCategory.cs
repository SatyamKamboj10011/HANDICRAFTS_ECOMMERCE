using System.Windows.Forms;
using Form_Loading.Models;
using Form_Loading.UI;

namespace Form_Loading
{
    /// <summary>Category overview: one large tile per collection.</summary>
    public partial class FormCategory : Form
    {
        public FormCategory()
        {
            InitializeComponent();

            OpenOnClick(Catalog.Handbags, panel1, pictureBox1);
            OpenOnClick(Catalog.Pots, panel3, pictureBox2, pictureBox3);
            OpenOnClick(Catalog.Jewellery, panel4, pictureBox4);
            OpenOnClick(Catalog.Decor, panel2, pictureBox5);
        }

        private void OpenOnClick(string category, params Control[] controls)
        {
            ToolTip tip = new ToolTip();
            foreach (Control c in controls)
            {
                c.Cursor = Cursors.Hand;
                tip.SetToolTip(c, "Shop " + category);
                c.Click += (s, e) => Nav.Go(this, Nav.ForCategory(category));
            }
        }
    }
}
