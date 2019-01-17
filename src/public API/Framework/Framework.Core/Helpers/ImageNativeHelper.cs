using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Framework.Core.Helpers
{
    public class ImageNativeHelper
    {

        public static ImageNativeHelper CreateInstance() { return new ImageNativeHelper(); }
        protected ImageNativeHelper() { }


        public static string[] ImageExtensions { get; } = new string[] { ".PNG", ".GIF", ".JPG", ".BMP" };

        public Image GetImage(byte[] imageContent)
        {
            using (var memoryStream = new MemoryStream(imageContent))
            {
                return Image.FromStream(memoryStream);
            }
        }

        public Image GetThumbnail(Image image, int width, int height)
        {
            Image.GetThumbnailImageAbort callback =
                    new Image.GetThumbnailImageAbort(ThumbnailCallback);
            var pThumbnail = image.GetThumbnailImage(width, height, callback, new IntPtr());
            return pThumbnail;
        }

        public Image GetThumbnail(Stream imageStream, int with, int height)
        {
            var image = Bitmap.FromStream(imageStream);
            return this.GetThumbnail(image, with, height);
        }

        public Image GetThumbnail(byte[] imageContent, int width, int height)
        {
            var imageStream = new MemoryStream(imageContent);
            return GetThumbnail(imageStream, width, height);
        }

        public Stream ToStream(Image image/*, int with, int height*/)
        {
            var result = new MemoryStream();
            image.Save(result, ImageFormat.Png);
            result.Flush();
            return result;
        }

        public Image CreateBlankImage(int width, int height)
        {
            Bitmap flag = new Bitmap(width, height);
            Graphics flagGraphics = Graphics.FromImage(flag);
            flagGraphics.FillRectangle(Brushes.White, 0, 0, width, height);

            return flag;
        }

        private bool ThumbnailCallback()
        {
            return true;
        }

        public Image DrawText(String text, Font font, Color textColor, Color backColor)
        {
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 0, 0);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;

        }

        public static bool IsImageByExtension(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            return ImageExtensions.Any(imageExtensionItem => imageExtensionItem.Equals(extension, StringComparison.InvariantCultureIgnoreCase));
        }
    }

    public static class ImageExtensionMethods
    {
        public static string GetMimeType(this ImageFormat imageFormat)
        {
            var codecs = ImageCodecInfo.GetImageEncoders();
            return codecs.First(codec => codec.FormatID == imageFormat.Guid).MimeType;
        }

        public static Image GrayScale(this Image image)
        {
            var result = new Bitmap(image);

            // Loop through the images pixels to reset color.
            for (var x = 0; x < result.Width; x++)
            {
                for (var y = 0; y < result.Height; y++)
                {
                    Color pixelColor = result.GetPixel(x, y);
                    int grayScale = (int)((pixelColor.R * 0.3) + (pixelColor.G * 0.59) + (pixelColor.B * 0.11));
                    Color newColor = Color.FromArgb(pixelColor.A, grayScale, grayScale, grayScale);
                    result.SetPixel(x, y, newColor); // Now greyscale
                }
            }

            return result;
        }

        public static Image GrayScale(byte[] imageContent)
        {
            Bitmap result = null;
            using (var fileStream = new MemoryStream(imageContent))
            {
                result = new Bitmap(fileStream);
                fileStream.Flush();
            }
            return result.GrayScale();
        }


        public static MemoryStream ToMemoryStream(this Image image, ImageFormat format = null)
        {
            MemoryStream result = new MemoryStream();
            image.Save(result, format ?? ImageFormat.Png);
            result.Flush();
            return result;
        }

        public static byte[] ToArray(this Image image, ImageFormat format = null)
        {
            using (var result = image.ToMemoryStream())
            {
                image.Save(result, format ?? ImageFormat.Png);
                result.Flush();

                return result.ToArray();
            }
        }

    }
}
