using System;
using System.Drawing;
using System.Windows.Forms;

namespace Form_Loading
{
    /// <summary>
    /// Cycles the home screen banner through the hero images (img0, img1, img2),
    /// starting from a random one.
    /// </summary>
    internal sealed class HeroSlideshow : IDisposable
    {
        private const int ImageCount = 3;
        private const int IntervalMs = 4000;

        private readonly Image[] images = new Image[ImageCount];
        private readonly PictureBox pictureBox;
        private readonly Timer timer;
        private int index;

        public HeroSlideshow(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            for (int i = 0; i < ImageCount; i++)
            {
                var original = (Image)Properties.Resources.ResourceManager.GetObject("img" + i);
                images[i] = UI.ImageFit.Cover(original, pictureBox.Size);
            }

            index = new Random().Next(ImageCount);
            timer = new Timer { Interval = IntervalMs };
            timer.Tick += (s, e) => ShowNext();
        }

        public void Start()
        {
            pictureBox.Image = images[index];
            timer.Start();
        }

        private void ShowNext()
        {
            index = (index + 1) % ImageCount;
            pictureBox.Image = images[index];
        }

        public void Dispose()
        {
            timer.Stop();
            timer.Dispose();
        }
    }
}
