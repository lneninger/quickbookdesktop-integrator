using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.EF.Design
{
    public abstract class BaseAbstractMap
    {
        public BaseAbstractMap(ModelBuilder modelBuilder)
        {
            this.ModelBuilder = modelBuilder;
        }

        public ModelBuilder ModelBuilder { get; }
    }
}
