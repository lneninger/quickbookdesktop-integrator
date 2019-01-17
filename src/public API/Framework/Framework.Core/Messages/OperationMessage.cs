using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Messages
{
    public class OperationMessage
    {
        public MessageTypeEnum MessageType { get; set; }

        public string PropertyName { get; set; }

        public string MessageCode { get; set; }

        public string Message { get; set; }
        public Exception Exception { get; internal set; }
    }
}
