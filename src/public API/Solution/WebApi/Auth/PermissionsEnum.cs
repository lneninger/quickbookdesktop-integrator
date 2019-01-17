using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiverdaleMainApp2_0.Auth
{
    public class PermissionsEnum
    {
        // Customer
        /// <summary>
        /// 
        /// </summary>
        public const string Customer_Read = nameof(Enum.Customer_Read);

        /// <summary>
        /// 
        /// </summary>
        public const string Customer_Modify = nameof(Enum.Customer_Modify);

        /// <summary>
        /// 
        /// </summary>
        public const string Customer_Manage = nameof(Enum.Customer_Manage);

        // Product
        /// <summary>
        /// 
        /// </summary>
        public const string Product_Read = nameof(Enum.Product_Read);

        /// <summary>
        /// 
        /// </summary>
        public const string Product_Modify = nameof(Enum.Product_Modify);

        /// <summary>
        /// 
        /// </summary>
        public const string Product_Manage = nameof(Enum.Product_Manage);

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


        // Sale Opportunity
        /// <summary>
        /// 
        /// </summary>
        public const string SaleOpportunity_Read = nameof(Enum.SaleOpportunity_Read);

        /// <summary>
        /// 
        /// </summary>
        public const string SaleOpportunity_Modify = nameof(Enum.SaleOpportunity_Modify);

        /// <summary>
        /// 
        /// </summary>
        public const string SaleOpportunity_Manage = nameof(Enum.SaleOpportunity_Manage);

        // Quote
        /// <summary>
        /// 
        /// </summary>
        public const string Quote_Read = nameof(Enum.Quote_Read);

        /// <summary>
        /// 
        /// </summary>
        public const string Quote_Modify = nameof(Enum.Quote_Modify);

        /// <summary>
        /// 
        /// </summary>
        public const string Quote_Manage = nameof(Enum.Quote_Manage);

        /// <summary>
        /// 
        /// </summary>
        public enum Enum
        {
            // Customer
            /// <summary>
            /// 
            /// </summary>
            Customer_Read,

            /// <summary>
            /// 
            /// </summary>
            Customer_Modify,

            /// <summary>
            /// 
            /// </summary>
            Customer_Manage,

            // Product

            /// <summary>
            /// 
            /// </summary>
            Product_Read,

            /// <summary>
            /// 
            /// </summary>
            Product_Modify,

            /// <summary>
            /// 
            /// </summary>
            Product_Manage,

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

            // Quote
            /// <summary>
            /// 
            /// </summary>
            Quote_Read,

            /// <summary>
            /// 
            /// </summary>
            Quote_Modify,

            /// <summary>
            /// 
            /// </summary>
            Quote_Manage,

            /// <summary>
            /// 
            /// </summary>
            SaleOpportunity_Read,

            /// <summary>
            /// 
            /// </summary>
            SaleOpportunity_Modify,

            /// <summary>
            /// 
            /// </summary>
            SaleOpportunity_Manage,
        }
    }
}
