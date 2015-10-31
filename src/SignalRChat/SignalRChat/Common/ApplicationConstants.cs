using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat.Common
{
    public static class ApplicationConstants
    {
        /// <summary>
        /// All store 
        /// </summary>
        public const string STORENUMBER_ALL = "0000";
        /// <summary>
        /// select default value
        /// </summary>
        public const string SELECT_DEFAULT = "-1";
        /// <summary>
        /// Super User
        /// </summary>
        public const int SUPER_USER = 1;
        /// <summary>
        /// Store associate
        /// </summary>
        public const int STORE_ASSOCIATE = 2;
        /// <summary>
        ///  Customer call center user
        /// </summary>
        public const int CUSTOMER_CALL_CENTER_USER = 3;
        /// <summary>
        /// Customer
        /// </summary>
        public const int CUSTOMER = 4;

        public const string CUSTOMER_CLIENTID = "Customer";

        public const string REGULAR_NULL_NUMBER = @"^\s*$|^\d*$";

        public const string REGULAR_FOUR_NUMBER = @"^\d{4}$";

        public const string REGULAR_INVALID_STORE_NUMBER = @"[^-1$]";

        public const string REGULAR_EMAIL = @"^[a-z0-9]+([._\\-]*[a-z0-9])*@([a-z0-9]+[-a-z0-9]*[a-z0-9]+.){1,63}[a-z0-9]+$";

        public const string REGULAR_PASSWORD = @"(?=^.{8,32}$)(?=(?:.*?\d){1})(?=(?:.*?[a-zA-Z]){1})(?!.*\s)[0-9a-zA-Z_&.@]*$";

        public const string REGULAR_USER = @"^[0-9a-zA-Z]{1,32}";

        public const string SYSTEM_USER = "SYSTEM";

        #region Status
        /// <summary>
        /// In Progress
        /// </summary>
        public const byte IN_PROGRESS = 1;
        /// <summary>
        /// Completed
        /// </summary>
        public const byte COMPLETED = 2;
        /// <summary>
        /// Despatched To store
        /// </summary>
        public const byte DESPATCHED_TO_STORE = 3;
        /// <summary>
        /// Ready For Collection
        /// </summary>
        public const byte READY_FOR_COLLECTION = 4;
        /// <summary>
        /// Collected
        /// </summary>
        public const byte COLLECTED = 5;
        /// <summary>
        /// Return To NDC
        /// </summary>
        public const byte RETURN_TO_NDC = 6;
        /// <summary>
        /// Extended Collection
        /// </summary>
        public const byte EXTENDED_COLLECTION = 7;
        /// <summary>
        /// Uncollected
        /// </summary>
        public const byte UNCOLLECTED = 8;
        #endregion

        public const string COOKIE_STORENUMBER = "COOKIE_STORENUMBER";

        public const string COOKIE_TOKEN = "COOKIE_TOKEN";

        public const string WINDOWS_CE = "Windows CE";

        public const string XP_IE6 = "MSIE 6.0; Windows NT 5.1";

        public const int APICLIENTTIMEOUT_DEFAULT = 30;

        public const string SUCCESSFLAG = "1";

        public const string ERRORFLAG = "0";

        public const string CARTONSTATUS = "2";

        public const string SUCCESSMSG = "Success";
        public const string USERUPLOADSHEETNAME = "USERS";

        public const string STOREUPLOADSHEETNAME = "STORES";

        public const string LOCATIONUPLOADSHEETNAME = "LOCATIONS";

        public const string USERUPLOADFILEPATH = "~/TemplateFiles/Bnc_Users.xlsx";

        public const string STOREUPLOADFILEPATH = "~/TemplateFiles/BnC_Stores.xlsx";

        public const string LOCATIONUPLOADFILEPATH = "~/TemplateFiles/BnC_Locations.xlsx";

        public const string REPORTBULKRECEIPTTITLE = "Bulk Receipting Status Report";

        public const string REPORTPUTAWAYTITLE = "Put Away Status Report";

        public const string REPORTBULKRECEIPTRDLCPATH = "~/ReportRdlc/BulkReceiptReport.rdlc";

        public const string REPORTPUTAWAYRDLCPATH = "~/ReportRdlc/PutAwayReport.rdlc";

        public const string USERSTATUSACTIVE = "ACTIVE";

        public const string USERSTATUSINACTIVE = "INACTIVE";

        public const string FIRSTSEARCH = "true";
        public const string FIRSTFALSE = "false";

        public const byte BULKRECEIPT = 1;

        public const byte NOSHOW = 2;

        public const string LOGTITLE = "WebApplication Exception";

    }
}