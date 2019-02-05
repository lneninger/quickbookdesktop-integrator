using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel.Commons.Enums
{
    public class AccountTypeEnum
    {
        public const string IncomeAccount = nameof(Enum.INCOME);
        public const string InventoryAccount = nameof(Enum.INVENTORY);

        public enum Enum {
            INCOME,
            INVENTORY
        }
    }
}
