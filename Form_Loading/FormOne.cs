using System;
using System.Windows.Forms;
using Form_Loading.Models;
using Form_Loading.UI;

namespace Form_Loading
{
    /// <summary>Home screen: rotating hero banner plus a card for each category.</summary>
    public partial class FormOne : Form
    {
        private HeroSlideshow slideshow;

        public FormOne()
        {
            InitializeComponent();

            // Each category card opens its collection, wherever it's clicked
            SetUpCard(Catalog.Handbags, panelHandbags, pictureHandbags, labelHandbags, labelHandbagsShop);
            SetUpCard(Catalog.Jewellery, panelJewellery, pictureJewellery, labelJewellery, labelJewelleryShop);
            SetUpCard(Catalog.Decor, panelDecor, pictureDecor, labelDecor, labelDecorShop);
            SetUpCard(Catalog.Pots, panelPots, picturePots, labelPots, labelPotsShop);

            labelViewAll.Click += (s, e) => Nav.Go(this, new FormCategory());
        }

        private void FormOne_Load(object sender, EventArgs e)
        {
            slideshow = new HeroSlideshow(pictureBoxHero);
            slideshow.Start();
            FormClosed += (s, args) => slideshow.Dispose();
        }

        private void SetUpCard(string category, Panel card, PictureBox picture, params Label[] labels)
        {
            // Crop the photo to fill the card without stretching it
            picture.Image = ImageFit.Cover(picture.Image, picture.Size);

            picture.Cursor = Cursors.Hand;
            card.Click += (s, e) => Nav.Go(this, Nav.ForCategory(category));
            picture.Click += (s, e) => Nav.Go(this, Nav.ForCategory(category));
            foreach (Label label in labels)
            {
                label.Cursor = Cursors.Hand;
                label.Click += (s, e) => Nav.Go(this, Nav.ForCategory(category));
            }
        }
    }
}
