using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core.Helpers
{
    public static class FileHelpers
    {
        /// <summary>
        /// Gets the MIME type by extension.
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <returns></returns>
        public static string GetMimeTypeByExtension(string extension)
        {
            return MimeTypes.Core.MimeTypeMap.GetMimeType(extension);
        }
    }
}
