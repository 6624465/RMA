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
        //public static string REPORTSUBFOLDER = WebConfigurationManager.AppSettings["ReportPath"].ToString();
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

        public static string ADMINISTRATION_MENU_COMPANYPROFILE = "CompanyProfile";

        public static string ADMINISTRATION_MENU_ROLERIGHTS = "RoleRights";

        public static string ADMINISTRATION_MENU_USERLIST = "UserList";

        public static string MASTER_MENU_CURRENCY = "Currency";
        public static string MASTER_MENU_PRODUCTS = "Products";
        public static string MASTER_MENU_CUSTOMER = "Customer";

        public static string TRANSACTION_MENU_MATERIALINWARD = "MaterialInward";
        public static string TRANSACTION_MENU_MATERIALOUTWARD = "MaterialOutward";
        public static string TRANSACTION_MENU_RMAVALIDATION = "RMAValidation";
        public static string TRANSACTION_MENU_VENDOROUTWARD = "VendorOutward";
        public static string TRANSACTION_MENU_VENDORRETURN = "VendorReturn";
        public static string TRANSACTION_MENU_RMARETURN = "RMAReturn";
        public static string TRANSACTION_MENU_MATERIALINQUIRY = "MaterialInquiry";
        public static string TRANSACTION_MENU_COO = "COO";
        public static string REPORTS_MENU_OPRREPORTS = "OprReports";
        public static string REPORTS_MENU_MGMTREPORTS = "MgmtReports";


        /*COMPANY PROFILE*/

        public static string ADMINISTRATION_ACTION_COMPANYPROFILEEDIT = "CompanyProfileEdit";
        public static string ADMINISTRATION_ACTION_COMPANYPROFILESAVE = "CompanyProfileSave";

        /* USER LIST */
        public static string ADMINISTRATION_ACTION_ADDUSER = "AddUser";
        public static string ADMINISTRATION_ACTION_DELETEUSER = "DeleteUser";

        /* CURRENCY MASTER */
        public static string MASTER_ACTION_LISTCURRENCY = "ListCurrency";
        public static string MASTER_ACTION_ADDCURRENCY = "AddCurrency";
        public static string MASTER_ACTION_SAVECURRENCY = "SaveCurrency";
        public static string MASTER_ACTION_EDITCURRENCY = "EditCurrency";
        public static string MASTER_ACTION_DELETECURRENCY = "DeleteCurrency";

        /* PRODUCT MASTER */
        public static string MASTER_ACTION_LISTPRODUCT = "ListProduct";
        public static string MASTER_ACTION_ADDPRODUCT = "AddProduct";
        public static string MASTER_ACTION_SAVEPRODUCT = "SaveProduct";
        public static string MASTER_ACTION_EDITPRODUCT = "EditProduct";
        public static string MASTER_ACTION_DELETEPRODUCT = "DeleteProduct";
        
        /* CONTACT DATABASE */
        public static string MASTER_ACTION_LISTCUSTOMER = "ListCustomer";
        public static string MASTER_ACTION_ADDCUSTOMER = "AddCustomer";
        public static string MASTER_ACTION_SAVECUSTOMER = "SaveCustomer";
        public static string MASTER_ACTION_EDITCUSTOMER = "EditCustomer";
        public static string MASTER_ACTION_DELETECUSTOMER = "DeleteCustomer";

        /* MATERIAL INWARD */
        public static string TRANSACTION_ACTION_LISTMI = "ListMI";
        public static string TRANSACTION_ACTION_ADDMI = "AddMI";
        public static string TRANSACTION_ACTION_SAVEMI = "SaveMI";
        public static string TRANSACTION_ACTION_EDITMI = "EditMI";
        public static string TRANSACTION_ACTION_DELETEMI = "DeleteMI";

        /* MATERIAL OUTWARD */
        public static string TRANSACTION_ACTION_LISTMO = "ListMO";
        public static string TRANSACTION_ACTION_ADDMO = "AddMO";
        public static string TRANSACTION_ACTION_SAVEMO = "SaveMO";
        public static string TRANSACTION_ACTION_EDITMO = "EditMO";

        /* RMA VALIDATION */
        public static string TRANSACTION_ACTION_LISTRMAVALIDATION = "ListRMAValidation";
        public static string TRANSACTION_ACTION_SAVERMAVALIDATION = "SaveRMAValidation";

        /* VENDOR OUTWARD */
        public static string TRANSACTION_ACTION_LISTVO = "ListVO";
        public static string TRANSACTION_ACTION_SAVEVO = "SaveVO";

        /* VENDRO RETURN */
        public static string TRANSACTION_ACTION_LISTVR = "ListVR";
        public static string TRANSACTION_ACTION_SAVEVR = "SaveVR";

        /* RMA RETURN */
        public static string TRANSACTION_ACTION_LISTRT = "ListRT";
        public static string TRANSACTION_ACTION_ADDRT = "AddRT";
        public static string TRANSACTION_ACTION_SAVERT = "SaveRT";
        public static string TRANSACTION_ACTION_EDITRT = "EditRT";
        public static string TRANSACTION_ACTION_DELETERT = "DeleteRT";

        /* MATERIAL INQUIRY */
        public static string TRANSACTION_ACTION_LISTINQUIRY = "ListInquiry";

        /* COUNTRY OF ORIGIN */
        public static string TRANSACTION_ACTION_LISTCOO = "ListCOO";
        public static string TRANSACTION_ACTION_ADDCOO = "AddCOO";
        public static string TRANSACTION_ACTION_SAVECOO = "SaveCOO";
        public static string TRANSACTION_ACTION_EDITCOO = "EditCOO";

        /* OPERATIONAL REPORTS */
        public static string REPORTS_ACTION_OPRATIONALREPORTS = "OprationalReports";

        /* MANAGEMENT REPORTS */
        public static string REPORTS_ACTION_MANAGEMENTREPORTS = "ManagementReports";


    }
}