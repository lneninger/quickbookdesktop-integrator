using Ghostscript.NET;
using Ghostscript.NET.Processor;
using Ghostscript.NET.Rasterizer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Framework.Core.Helpers
{
    public static class PdfHelper
    {
        public static Image ToThumbnail(Stream stream, int dpi, int pageNumber = 1)
        {
            GhostscriptVersionInfo gvi = new GhostscriptVersionInfo(@"./_libs/gsdll64.dll");

            GhostscriptProcessor proc = new GhostscriptProcessor(gvi);

            using (GhostscriptRasterizer rasterizer = new GhostscriptRasterizer())
            {
                rasterizer.Open(stream);

                var result = rasterizer.GetPage(dpi, dpi, pageNumber);

                return result;
            }
        }

        public static Image ToThumbnail(byte[] pdfContent, int dpi, int pageNumber = 1)
        {
            using (var pdfStream = new MemoryStream(pdfContent))
            {
                return ToThumbnail(pdfStream, dpi, pageNumber);
            }
        }
    }
}
