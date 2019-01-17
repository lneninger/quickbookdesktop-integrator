using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLogic.Business.Interfaces
{
    public interface IBusinessService
    {
        IEnumerable<T> GetAll<T>() where T : class, new();
        T GetById<T>() where T : class, new();
    }
}
