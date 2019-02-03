using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSchema
{
    public class ExecutionStatusEnum
    {
        public const string Executing = nameof(Enum.EXEC);
        public const string Success = nameof(Enum.SUCC);
        public const string Error = nameof(Enum.ERROR);

        public enum Enum
        {
            EXEC = 1,
            SUCC = 2,
            ERROR = 3
        }
        
    }
}
