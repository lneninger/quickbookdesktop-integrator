using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLogic.Business.Commons.DTOs
{
    public class EnumItemDTO<TKey>
    {
        public TKey Key { get; set; }

        public string  Value { get; set; }

        public Dictionary<string, object> Extras { get; set; }
    }
}
