using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Quickbooks
{
    public enum ENEdition
    {
        edUS = 0,
        edCA = 1,
        edUK = 2,
    }

    static class QBEdition
    {
        public static readonly string[] codes = { "US", "CA", "UK" };

        public static string getEdition(ENEdition ed)
        {
            return codes[(int)ed];
        }
    }
}
