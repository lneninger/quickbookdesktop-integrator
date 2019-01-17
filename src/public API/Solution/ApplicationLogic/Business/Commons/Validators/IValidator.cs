using Framework.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLogic.Business.Commons.Validators
{
    public interface IValidator<T>: FluentValidation.IValidator<T>
    {
        
    }
}
