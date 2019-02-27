using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickbooksIntegratorAPI.Auth
{
    public class PermissionsEnum
    {
        /// Inventory Item
        /// <summary>
        /// 
        /// </summary>
        public const string InventoryItem_Read = nameof(Enum.InventoryItem_Read);

        /// <summary>
        /// 
        /// </summary>
        public const string InventoryItem_Modify = nameof(Enum.InventoryItem_Modify);

        /// <summary>
        /// 
        /// </summary>
        public const string InventoryItem_Manage = nameof(Enum.InventoryItem_Manage);


        /// Price Level
        /// <summary>
        /// 
        /// </summary>
        public const string PriceLevel_Read = nameof(Enum.PriceLevel_Read);

        /// <summary>
        /// 
        /// </summary>
        public const string PriceLevel_Modify = nameof(Enum.PriceLevel_Modify);

        /// <summary>
        /// 
        /// </summary>
        public const string PriceLevel_Manage = nameof(Enum.PriceLevel_Manage);


        // User
        /// <summary>
        /// 
        /// </summary>
        public const string User_Read = nameof(Enum.User_Read);

        /// <summary>
        /// 
        /// </summary>
        public const string User_Modify = nameof(Enum.User_Modify);

        /// <summary>
        /// 
        /// </summary>
        public const string User_Manage = nameof(Enum.User_Manage);

        // User Role
        /// <summary>
        /// 
        /// </summary>
        public const string UserRole_Read = nameof(Enum.UserRole_Read);

        /// <summary>
        /// 
        /// </summary>
        public const string UserRole_Modify = nameof(Enum.UserRole_Modify);

        /// <summary>
        /// 
        /// </summary>
        public const string UserRole_Manage = nameof(Enum.UserRole_Manage);
        /// <summary>
        /// 
        /// </summary>
        public enum Enum
        {
            // InventoryItem
            /// <summary>
            /// 
            /// </summary>
            InventoryItem_Read,

            /// <summary>
            /// 
            /// </summary>
            InventoryItem_Modify,

            /// <summary>
            /// 
            /// </summary>
            InventoryItem_Manage,

            // InventoryItem
            /// <summary>
            /// 
            /// </summary>
            PriceLevel_Read,

            /// <summary>
            /// 
            /// </summary>
            PriceLevel_Modify,

            /// <summary>
            /// 
            /// </summary>
            PriceLevel_Manage,

            // User
            /// <summary>
            /// 
            /// </summary>
            User_Read,

            /// <summary>
            /// 
            /// </summary>
            User_Modify,

            /// <summary>
            /// 
            /// </summary>
            User_Manage,

            // User Role
            /// <summary>
            /// 
            /// </summary>
            UserRole_Read,

            /// <summary>
            /// 
            /// </summary>
            UserRole_Modify,

            /// <summary>
            /// 
            /// </summary>
            UserRole_Manage,

        }
    }
}
