using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core.GeneralValidations
{
    public static class InternetFieldNormalization
    {
        public static string NormalizeEmail(string email)
        {
            var splitted = (email ?? string.Empty).Split('@');
            return splitted.Length == 2 ? $"{splitted[0]}@{splitted[1].ToLower()}" : string.Join("@", splitted);
        }
    }
}
