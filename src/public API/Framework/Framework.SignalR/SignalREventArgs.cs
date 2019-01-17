using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.SignalR
{
    public class SignalREventArgs
    {
        public SignalREventArgs(string eventName, string action, string entityName, object entity)
        {
            this.EventName = eventName;
            this.Action = action;
            this.EntityName = entityName;
            this.Entity = entity;
        }

        public string EventName { get; set; }

        public string Action { get; set; }

        public string EntityName { get; set; }

        public object Entity { get; set; }
    }
}
