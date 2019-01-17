using FluentValidation.Results;
using System;
using System.Linq;

namespace Framework.FluentValidation
{
    public static class Extensions
    {
        public static T AddValidationResult<T>(this T objRef, ValidationResult result) where T : OperationResponse
        {
            result.Errors.ToList().ForEach(o =>
            {
                objRef.AddMessage(new OperationMessage { MessageType = MessageTypeEnum.Error, Message = o.ErrorMessage, PropertyName = o.PropertyName, MessageCode = o.ErrorCode });
            });


            return objRef;
        }
    }
}
