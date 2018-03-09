using EZY.RMAS.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace RMA.Web
{
    public static class UTILITY
    {
        public static string REPORTSUBFOLDER = WebConfigurationManager.AppSettings["ReportSubFolder"].ToString();
        public static string SSN_USER_OBJECT = "SSN_USER";
        public static string SSN_USER_SECURABLES = "SSN_SECURABLES";

        public static short CONFIG_CUSTOMERTYPE_MANUFACTURER = 101;
        public static short CONFIG_CUSTOMERTYPE_VENDOR = 102;
        public static short CONFIG_CUSTOMERTYPE_CUSTOMER = 103;

        public static short CONFIG_INVOICETYPE_VENDOR = 120;
        public static short CONFIG_INVOICETYPE_CUSTOMER = 121;

        public static short CONFIG_REPLACEMENTTYPE_REPLACEMENT = 125;
        public static short CONFIG_REPLACEMENTTYPE_CREDITNOTE = 126;
        public static short CONFIG_REPLACEMENTTYPE_OPEN = 127;

        public static string CONFIG_LOOKUPCATEGORY_CUSTOMERTYPE = "CustomerType";
        public static string CONFIG_LOOKUPCATEGORY_INVOICETYPE = "InvoiceType";
        public static string CONFIG_LOOKUPCATEGORY_REPLACEMENTTYPE = "ReplacementType";
        public static string CONFIG_LOOKUPCATEGORY_CATEGORY = "CategoryGroup";
        //public static string CONFIG_LOOKUPCATEGORY_PRODUCTCATEGORY = "ProductCategory";
        public static string CONFIG_LOOKUPCATEGORY_CURRENCY = "Currency";
        public static string DEFAULT_DECLARANT = "Elaine Krishnan";
        


        //public static string SECURABLE_ADMINISTRATION = "Administration";
        //public static string SECURABLE_COMPANYPROFILE = "CompanyProfile";
        //public static string SECURABLE_COMPANYPROFILEEDIT = "CompanyProfileEdit";
        //public static string SECURABLE_COMPANYPROFILESAVE = "CompanyProfileSave";
        //public static string SECURABLE_CONFIG = "Config";
        //public static string SECURABLE_CUSTOMERADD = "CustomerAdd";
        //public static string SECURABLE_CUSTOMERDELETE = "CustomerDelete";
        //public static string SECURABLE_CUSTOMEREDIT = "CustomerEdit";
        //public static string SECURABLE_CUSTOMERINVOICEADD = "CustomerInvoiceAdd";
        //public static string SECURABLE_CUSTOMERINVOICEDELETE = "CustomerInvoiceDelete";
        //public static string SECURABLE_CUSTOMERINVOICEEDIT = "CustomerInvoiceEdit";
        //public static string SECURABLE_CUSTOMERINVOICELIST = "CustomerInvoiceList";
        //public static string SECURABLE_CUSTOMERINVOICESAVE = "CustomerInvoiceSave";
        //public static string SECURABLE_CUSTOMERS = "Customers";
        //public static string SECURABLE_CUSTOMERSAVE = "CustomerSave";
        //public static string SECURABLE_MANAGEMENTREPORTS = "ManagementReports";
        //public static string SECURABLE_MASTER = "Master";
        //public static string SECURABLE_OPERATIONALREPORTS = "OperationalReports";
        //public static string SECURABLE_PRODUCTADD = "ProductAdd";
        //public static string SECURABLE_PRODUCTDELETE = "ProductDelete";
        //public static string SECURABLE_PRODUCTEDIT = "ProductEdit";
        //public static string SECURABLE_PRODUCTS = "Products";
        //public static string SECURABLE_PRODUCTSAVE = "ProductSave";
        //public static string SECURABLE_REPORTS = "Reports";
        //public static string SECURABLE_ROLERIGHTS = "RoleRights";
        //public static string SECURABLE_SERIALNOINVOICE = "SerialNoInvoice";
        //public static string SECURABLE_TRANSACTIONS = "Transactions";
        //public static string SECURABLE_USERSLIST = "UsersList";
        //public static string SECURABLE_VENDORINVOICEADD = "VendorInvoiceAdd";
        //public static string SECURABLE_VENDORINVOICEDELETE = "VendorInvoiceDelete";
        //public static string SECURABLE_VENDORINVOICEEDIT = "VendorInvoiceEdit";
        //public static string SECURABLE_VENDORINVOICELIST = "VendorInvoiceList";
        //public static string SECURABLE_VENDORINVOICESAVE = "VendorInvoiceSave";

        //public static DateTime SingaporeTime(this DateTime value)
        //{
        //    string nzTimeZoneKey = "SE Asia Standard Time";
        //    TimeZoneInfo nzTimeZone = TimeZoneInfo.FindSystemTimeZoneById(nzTimeZoneKey);
        //    return TimeZoneInfo.ConvertTimeFromUtc(value, nzTimeZone);
        //}

        public static DateTime SINGAPORETIME
        {
            get
            {
                string nzTimeZoneKey = "SE Asia Standard Time";
                TimeZoneInfo nzTimeZone = TimeZoneInfo.FindSystemTimeZoneById(nzTimeZoneKey);
                return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, nzTimeZone);
            }
            
        }

        public static bool HasRight(string securableItem)
        {
            var securables = (List<Securables>)System.Web.HttpContext.Current.Session[UTILITY.SSN_USER_SECURABLES];

            var obj = securables.Where(x => x.SecurableItem == securableItem)
                        .FirstOrDefault();

            if (obj != null)
                return true;
            else
                return false;

        }

        public static bool HasRight(string securableItem, string actionType)
        {
            var securables = (List<Securables>)System.Web.HttpContext.Current.Session[UTILITY.SSN_USER_SECURABLES];

            var obj = securables.Where(x => x.SecurableItem == securableItem && x.ActionType == actionType)
                        .FirstOrDefault();

            if (obj != null)
                return true;
            else
                return false;

        }
    }

    public static class SECURABLE
    {
        /* securable key generator sql code
        Set nocount On;
        Select * into #TempLookup From security.Securables 
        Where GroupID <> ''
        Select * From #TempLookup


        Declare @GroupID nvarchar(30);
        Declare @SecurableItem nvarchar(30);
        Declare @ActionType nvarchar(30);
        While (Select Count(0) From #TempLookup) > 0
        Begin
            Select Top(1) @GroupID = GroupID, 
				          @SecurableItem = SecurableItem,
				          @ActionType = ActionType From #TempLookup Order By Sequence asc
            print 'public static string ' + UPPER(@GroupID) + '_' + UPPER(@ActionType) + '_' + UPPER(@SecurableItem) + ' = "' + @SecurableItem + '";'
	        --print '<add key="' + UPPER(@lookupCode) + '_' + convert(nvarchar(20), @branchId) + '" value="' + convert(nvarchar(20), @lookupId) + '" />'

            Delete From #TempLookup Where GroupID = @GroupID and SecurableItem = @SecurableItem;
        End

        Drop table #TempLookup
        Set nocount Off; 
        */

        public static string _TOPMENU_MASTER = "Master";
        public static string _TOPMENU_TRANSACTIONS = "Transactions";
        public static string _TOPMENU_ADMINISTRATION = "Administration";
        public static string _TOPMENU_REPORTS = "Reports";

        public static string MASTER_MENU_PRODUCTS = "Products";
        public static string MASTER_MENU_CUSTOMERS = "Customers";
        public static string TRANSACTIONS_MENU_VENDORINVOICELIST = "VendorInvoiceList";
        public static string TRANSACTIONS_MENU_CUSTOMERINVOICELIST = "CustomerInvoiceList";
        public static string TRANSACTIONS_MENU_SERIALNOINVOICE = "SerialNoInvoice";
        public static string ADMINISTRATION_MENU_COMPANYPROFILE = "CompanyProfile";
        public static string ADMINISTRATION_MENU_ROLERIGHTS = "RoleRights";
        public static string ADMINISTRATION_MENU_USERSLIST = "UsersList";
        public static string ADMINISTRATION_MENU_CONFIG = "Config";
        public static string REPORTS_MENU_OPERATIONALREPORTS = "OperationalReports";
        public static string REPORTS_MENU_MANAGEMENTREPORTS = "ManagementReports";

        public static string MASTER_ACTION_PRODUCTEDIT = "ProductEdit";
        public static string MASTER_ACTION_PRODUCTSAVE = "ProductSave";
        public static string MASTER_ACTION_PRODUCTDELETE = "ProductDelete";
        public static string MASTER_ACTION_PRODUCTADD = "ProductAdd";
        public static string MASTER_ACTION_CUSTOMEREDIT = "CustomerEdit";
        public static string MASTER_ACTION_CUSTOMERSAVE = "CustomerSave";
        public static string MASTER_ACTION_CUSTOMERDELETE = "CustomerDelete";
        public static string MASTER_ACTION_CUSTOMERADD = "CustomerAdd";
        public static string TRANSACTIONS_ACTION_VENDORINVOICEEDIT = "VendorInvoiceEdit";
        public static string TRANSACTIONS_ACTION_VENDORINVOICESAVE = "VendorInvoiceSave";
        public static string TRANSACTIONS_ACTION_VENDORINVOICEDELETE = "VendorInvoiceDelete";
        public static string TRANSACTIONS_ACTION_VENDORINVOICEADD = "VendorInvoiceAdd";
        public static string TRANSACTIONS_ACTION_CUSTOMERINVOICEEDIT = "CustomerInvoiceEdit";
        public static string TRANSACTIONS_ACTION_CUSTOMERINVOICESAVE = "CustomerInvoiceSave";
        public static string TRANSACTIONS_ACTION_CUSTOMERINVOICEDELETE = "CustomerInvoiceDelete";
        public static string TRANSACTIONS_ACTION_CUSTOMERINVOICEADD = "CustomerInvoiceAdd";
        public static string ADMINISTRATION_ACTION_COMPANYPROFILEEDIT = "CompanyProfileEdit";
        public static string ADMINISTRATION_ACTION_COMPANYPROFILESAVE = "CompanyProfileSave";

    }
}