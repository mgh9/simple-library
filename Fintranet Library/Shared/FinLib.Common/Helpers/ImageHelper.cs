using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using FinLib.Common.Exceptions.Business;

namespace FinLib.Common.Helpers
{
    public static class ImageHelper
    {
        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(string imageFileName, int width, int height
            , CompositingMode compositingMode = CompositingMode.SourceCopy
            , CompositingQuality compositingQuality = CompositingQuality.HighQuality
            , InterpolationMode interpolationMode = InterpolationMode.HighQualityBicubic
            , SmoothingMode smoothingMode = SmoothingMode.HighQuality
            , PixelOffsetMode pixelOffsetMode = PixelOffsetMode.HighQuality)
        {
            if (!File.Exists(imageFileName))
            {
                throw new GeneralBusinessLogicException("فایل یافت نشد");
            }

            var img = Image.FromFile(imageFileName);
            return ResizeImage(img, width, height, compositingMode, compositingQuality, interpolationMode, smoothingMode, pixelOffsetMode);
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height
        , CompositingMode compositingMode = CompositingMode.SourceCopy
        , CompositingQuality compositingQuality = CompositingQuality.HighQuality
        , InterpolationMode interpolationMode = InterpolationMode.HighQualityBicubic
        , SmoothingMode smoothingMode = SmoothingMode.HighQuality
        , PixelOffsetMode pixelOffsetMode = PixelOffsetMode.HighQuality)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = compositingMode;
                graphics.CompositingQuality = compositingQuality;
                graphics.InterpolationMode = interpolationMode;
                graphics.SmoothingMode = smoothingMode;
                graphics.PixelOffsetMode = pixelOffsetMode;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
