using System.Linq;
using System.Windows.Forms;
using Form_Loading.Models;
using Form_Loading.UI;

namespace Form_Loading
{
    /// <summary>All collections: one large photo tile per category.</summary>
    public partial class FormCategory : Form
    {
        public FormCategory()
        {
            InitializeComponent();

            SetUpTile(Catalog.Handbags, panelHandbags, pictureHandbags, labelHandbagsCount, labelHandbags, labelHandbagsArrow);
            SetUpTile(Catalog.Jewellery, panelJewellery, pictureJewellery, labelJewelleryCount, labelJewellery, labelJewelleryArrow);
            SetUpTile(Catalog.Decor, panelDecor, pictureDecor, labelDecorCount, labelDecor, labelDecorArrow);
            SetUpTile(Catalog.Pots, panelPots, picturePots, labelPotsCount, labelPots, labelPotsArrow);
        }

        private void SetUpTile(string category, Panel tile, PictureBox picture, Label count, params Label[] labels)
        {
            // Crop the photo to fill the tile without stretching it
            picture.Image = ImageFit.Cover(picture.Image, picture.Size);
            count.Text = Catalog.InCategory(category).Count() + " items";

            var tip = new ToolTip();
            foreach (Control c in new Control[] { tile, picture, count }.Concat(labels))
            {
                c.Cursor = Cursors.Hand;
                tip.SetToolTip(c, "Shop " + category);
                c.Click += (s, e) => Nav.Go(this, Nav.ForCategory(category));
            }
        }
    }
}
