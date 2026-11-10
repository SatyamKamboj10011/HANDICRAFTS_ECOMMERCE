using System;
using System.Windows.Forms;
using Form_Loading.Models;
using Form_Loading.UI;

namespace Form_Loading
{
    /// <summary>Home screen: rotating hero banner plus a tile for each category.</summary>
    public partial class FormOne : Form
    {
        private HeroSlideshow slideshow;

        public FormOne()
        {
            InitializeComponent();

            // Each category tile is a picture with a caption on top; both open the category
            OpenOnClick(Catalog.Handbags, pictureBox2, label1);
            OpenOnClick(Catalog.Jewellery, pictureBox3, label2);
            OpenOnClick(Catalog.Decor, pictureBox5, label5, label6);
            OpenOnClick(Catalog.Pots, pictureBox4, label3, label4);

            label7.Cursor = Cursors.Hand;
            label7.Click += (s, e) => Nav.Go(this, new FormCategory());
        }

        private void FormOne_Load(object sender, EventArgs e)
        {
            slideshow = new HeroSlideshow(pictureBox1);
            slideshow.Start();
            FormClosed += (s, args) => slideshow.Dispose();
        }

        private void OpenOnClick(string category, params Control[] controls)
        {
            foreach (Control c in controls)
            {
                c.Cursor = Cursors.Hand;
                c.Click += (s, e) => Nav.Go(this, Nav.ForCategory(category));
            }
        }
    }
}
