using Framework.Core.Messages;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Helpers
{
    public static class OperationResponseExtensions
    {
        public static T AddModelState<T>(this T objRef, ModelStateDictionary model) where T: OperationResponse {
            foreach (var modelError in model)
            {
                objRef.AddError($"{modelError.Key}, {modelError.Value.AttemptedValue}");
            }

            return objRef;
        }
    }
}
