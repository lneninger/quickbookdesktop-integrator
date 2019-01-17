using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
//using System.Drawing;
//using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Framework.Core.Helpers
{
    public class ImageHelper
    {
        public static ImageHelper CreateInstance() { return new ImageHelper(); }
        protected ImageHelper() { }


        public static string[] ImageExtensions { get; } = new string[] { ".PNG", ".GIF", ".JPG", ".BMP" };

        public Image<Rgba32> GetImage(byte[] imageContent)
        {
            using (var memoryStream = new MemoryStream(imageContent))
            {
                return Image.Load(memoryStream);
            }
        }

        public Image<Rgba32> GetThumbnail(Image<Rgba32> image, int width, int height)
        {

            image.Mutate(x => x.Resize(width, height));
            return image;
        }

        public Image<Rgba32> GetThumbnail(Stream imageStream, int width, int height)
        {
            using (var memStream = new MemoryStream())
            {
                imageStream.CopyTo(memStream);
                imageStream.Flush();
                var image = GetImage(memStream.GetBuffer());
                return GetThumbnail(image, width, height);
            }
        }

        public Image<Rgba32> GetThumbnail(byte[] imageContent, int width, int height)
        {
            var image = GetImage(imageContent);
            return GetThumbnail(image, width, height);
        }

        public Stream ToStream(Image<Rgba32> image/*, int with, int height*/)
        {
            var result = new MemoryStream();
            image.SaveAsPng(result);
            result.Flush();
            return result;
        }

        public Image<Rgba32> CreateBlankImage(int width, int height)
        {
            var configuration = new Configuration();
            var image = new Image<Rgba32>(configuration, width, height, Rgba32.White);

            return image;
        }

        private bool ThumbnailCallback()
        {
            return true;
        }

        public Image<Rgba32> DrawText(String text, Font font, Rgba32 textColor, Rgba32 backColor)
        {
            Image<Rgba32> img = new Image<Rgba32>(1, 1);
            img.Mutate(ctx => {
                Vector2 center = new Vector2(img.Width / 2, 10); //center horizontally, 10px down 

            ctx.DrawText(text, font, textColor, PointF.Empty);
               
            });
           


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

        public static Image<Rgba32> GrayScale(this Image<Rgba32> image, float amount = 0.5F)
        {
            image.Mutate(ctx => ctx.Grayscale(amount));

            return image;
        }

        public static Image<Rgba32> GrayScale(byte[] imageContent)
        {
            var image = Image.Load(imageContent);
            return image.GrayScale();
        }


        public static MemoryStream ToMemoryStream(this Image<Rgba32> image, ImageFormat format = null)
        {
            MemoryStream result = new MemoryStream();
            image.SaveAsPng(result);
            result.Flush();
            return result;
        }

        public static byte[] ToArray(this Image<Rgba32> image, ImageFormat format = null)
        {
            using (var result = image.ToMemoryStream())
            {
                return result.GetBuffer();
            }
        }



    }
}
