using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.SignalR
{
    public class SignalREvents{

        //public const string UserAuthentication = nameof(Enum.USER_AUTHENTICATION);
        //public const string DataChanged = nameof(Enum.DATA_CHANGED);

        //public enum Enum {
        //    USER_AUTHENTICATION,
        //    DATA_CHANGED,
        //}


        public static class DATA_CHANGED
        {
            public static string Identifier = nameof(DATA_CHANGED);

            public enum ActionEnum
            {
                ADDED_ITEM,
                UPDATED_ITEM,
                VOID_ITEM,
                DELETED_ITEM,
            }
        }
    }

    
}
