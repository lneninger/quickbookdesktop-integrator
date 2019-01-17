using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Storage.FileStorage.TemporaryStorage
{
    public class TemporaryFileUpdatedResult
    {
        public virtual Guid UniqueIdentifier { get; set; }

        public virtual string OriginalFileName { get; set; }

        public virtual string TemporaryFileName { get; set; }

        public string ContentType { get; set; }

        public long ContentLength { get; set; }
    }
}
