using System;

namespace DatabaseSchema
{
    public class QuickbookTicket
    {
        public int Id { get; set; }

        public string Ticket { get; set; }

        public bool Authenticated { get; set; }
    }
}
