using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseSchema
{
    public class QuickbookState
    {
        public int Id { get; set; }

        public string Ticket { get; set; }

        public string CurrentStep { get; set; }

        public string Value { get; set; }

        public string Key { get; set; }
    }
}
