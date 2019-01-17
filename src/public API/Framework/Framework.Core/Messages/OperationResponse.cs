using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Messages
{
    /// <summary>
    ///  Implementation for Operation Response Result Data Holder
    /// </summary>
    public class OperationResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResponse"/> class.
        /// </summary>
        public OperationResponse()
        {
            this.Messages = new List<OperationMessage>();
        }

        /// <summary>
        /// Gets a value indicating whether this instance is succeed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is succeed; otherwise, <c>false</c>.
        /// </value>
        public bool IsSucceed
        {
            get
            {
                return Messages.Any(o => o.MessageType == MessageTypeEnum.Error) == false;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResponse"/> class.
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="message">The message.</param>
        public OperationResponse(MessageTypeEnum messageType, string message) : this()
        {
            this.Messages.Add(new OperationMessage
            {
                MessageType = messageType,
                Message = message
            });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResponse"/> class.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public OperationResponse(Exception ex)
        {
            this.Messages = new List<OperationMessage>();
            this.Messages.Add(new OperationMessage
            {
                MessageType = MessageTypeEnum.Error,
                Message = "Erro: " + ex.Message
            });
        }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        /// <value>
        /// The messages.
        /// </value>
        public ICollection<OperationMessage> Messages { get; set; }

    }
}
