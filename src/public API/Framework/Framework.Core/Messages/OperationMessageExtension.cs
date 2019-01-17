using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Messages
{
    public static class OperationMessageExtension
    {
        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objRef">The object reference.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageParams">The message parameters.</param>
        /// <returns></returns>
        public static T AddError<T>(this T objRef, string message, params object[] messageParams) where T: OperationResponse
        {
            return objRef.AddMessage(MessageTypeEnum.Error, message, messageParams);
        }

        public static T AddException<T>(this T objRef, string message, Exception ex) where T : OperationResponse
        {
            var result = objRef.AddMessage(MessageTypeEnum.Error, message);
            result.Messages.Last().Exception = ex;

            return result;
        }

        /// <summary>
        /// Adds the warning.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objRef">The object reference.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageParams">The message parameters.</param>
        /// <returns></returns>
        public static T AddWarning<T>(this T objRef, string message, params object[] messageParams) where T : OperationResponse
        {
            return objRef.AddMessage(MessageTypeEnum.Warning, message, messageParams);
        }

        /// <summary>
        /// Adds the success.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objRef">The object reference.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageParams">The message parameters.</param>
        /// <returns></returns>
        public static T AddSuccess<T>(this T objRef, string message, params object[] messageParams) where T : OperationResponse
        {
            return objRef.AddMessage(MessageTypeEnum.Success, message, messageParams);
        }

        /// <summary>
        /// Adds the information.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objRef">The object reference.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageParams">The message parameters.</param>
        /// <returns></returns>
        public static T AddInfo<T>(this T objRef, string message, params object[] messageParams) where T : OperationResponse
        {
            return objRef.AddMessage(MessageTypeEnum.Info, message, messageParams);
        }

        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objRef">The object reference.</param>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="messageTemplate">The message template.</param>
        /// <param name="messageParams">The message parameters.</param>
        /// <returns></returns>
        public static T AddMessage<T>(this T objRef, MessageTypeEnum messageType, string messageTemplate, params object[] messageParams) where T : OperationResponse
        {
            var template = messageTemplate ?? string.Empty;
            var message = string.Format(template, messageParams);

            objRef.Messages.Add(new OperationMessage { MessageType = messageType, Message = message });

            return objRef;
        }

        public static T AddMessage<T>(this T objRef, OperationMessage message) where T : OperationResponse
        {
            if (message != null)
            {
                objRef.Messages.Add(message);
            }

            return objRef;
        }

        /// <summary>
        /// Adds the response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objRef">The object reference.</param>
        /// <param name="inputResponse">The input response.</param>
        /// <returns></returns>
        public static T AddResponse<T>(this T objRef, OperationResponse inputResponse) where T : OperationResponse
        {
            if (inputResponse != null && inputResponse.Messages != null)
            {
                foreach (var message in inputResponse.Messages)
                {
                    objRef.Messages.Add(message);
                }
            }

            return objRef;
        }
    }
}
