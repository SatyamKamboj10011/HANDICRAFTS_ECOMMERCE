using System;
using System.Drawing;

namespace Form_Loading.UI
{
    /// <summary>
    /// Image helpers. WinForms can stretch or letterbox an image, but not
    /// "cover" (fill the box and crop the overflow), so this does it.
    /// </summary>
    public static class ImageFit
    {
        /// <summary>
        /// Returns the centre of the image cropped to the same shape as
        /// <paramref name="box"/>. Show it with PictureBoxSizeMode.StretchImage.
        /// </summary>
        public static Image Cover(Image source, Size box)
        {
            if (source == null || box.Width <= 0 || box.Height <= 0)
                return source;

            double boxRatio = (double)box.Width / box.Height;
            double imageRatio = (double)source.Width / source.Height;

            Rectangle crop;
            if (imageRatio > boxRatio)
            {
                // Image is wider than the box: trim the sides
                int width = (int)Math.Round(source.Height * boxRatio);
                crop = new Rectangle((source.Width - width) / 2, 0, width, source.Height);
            }
            else
            {
                // Image is taller than the box: trim top and bottom
                int height = (int)Math.Round(source.Width / boxRatio);
                crop = new Rectangle(0, (source.Height - height) / 2, source.Width, height);
            }

            // Keep at most 2x the box size: sharp on high-DPI screens, without huge bitmaps
            int outWidth = Math.Min(crop.Width, box.Width * 2);
            int outHeight = (int)Math.Round(outWidth / boxRatio);

            var result = new Bitmap(outWidth, outHeight);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(source, new Rectangle(0, 0, outWidth, outHeight), crop, GraphicsUnit.Pixel);
            }
            return result;
        }
    }
}
