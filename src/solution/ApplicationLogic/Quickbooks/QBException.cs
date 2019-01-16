using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Quickbooks
{
    public class QBException : Exception
    {
        private QBException() { }

        public QBException(string sMsg)
            : base(sMsg)
        {
        }

        public override string ToString()
        {
            return base.Message;
        }

    }
}
