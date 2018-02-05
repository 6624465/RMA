using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZY.RMAS.Contract;

namespace EZY.RMAS.DataFactory
{
    public class DashboardDAL
    {
        private Database db;

        public DashboardDAL()
        {
            db = DatabaseFactory.CreateDatabase("RMAS");
        }

        public List<MaterialInwardProductQty> GetMaterialInwardProductQtyMonthWise(MaterialInwardDashboardDTO dashBoardParams)
        {
            return db.ExecuteSprocAccessor(DBRoutine.MATERIALINWARDPRODUCTQTY, MapBuilder<MaterialInwardProductQty>.BuildAllProperties()
                , dashBoardParams.Branch
                , dashBoardParams.ProductCode
                , dashBoardParams.Year
                , dashBoardParams.FromMonth, dashBoardParams.ToMonth).ToList();
        }
        public List<MaterialInwardProductCost> GetMaterialInwardProductCostMonthWise(MaterialInwardDashboardDTO dashBoardParams)
        {
            return db.ExecuteSprocAccessor(DBRoutine.MATERIALINWARDPRODUCTCOST, MapBuilder<MaterialInwardProductCost>.BuildAllProperties()
                , dashBoardParams.Branch
                 , dashBoardParams.ProductCode
                , dashBoardParams.Year
               , dashBoardParams.FromMonth, dashBoardParams.ToMonth).ToList();
        }
        public List<MaterialInwardInvoices> GetMaterialInwardInvoicesMonthWise(MaterialInwardDashboardDTO dashBoardParams)
        {
            return db.ExecuteSprocAccessor(DBRoutine.MATERIALINWARDINVOICES, MapBuilder<MaterialInwardInvoices>.BuildAllProperties()
                , dashBoardParams.Branch
                 , dashBoardParams.ProductCode
                , dashBoardParams.Year
               , dashBoardParams.FromMonth, dashBoardParams.ToMonth).ToList();
        }
        public List<MaterialInwardProductQty> GetMaterialOutwardProductQtyMonthWise(MaterialInwardDashboardDTO dashBoardParams)
        {
            return db.ExecuteSprocAccessor(DBRoutine.MATERIALOUTWARDPRODUCTQTY, MapBuilder<MaterialInwardProductQty>.BuildAllProperties()
                , dashBoardParams.Branch
                 , dashBoardParams.ProductCode
                , dashBoardParams.Year
               , dashBoardParams.FromMonth, dashBoardParams.ToMonth).ToList();
        }
        public List<MaterialInwardProductCost> GetMaterialOutwardProductCostMonthWise(MaterialInwardDashboardDTO dashBoardParams)
        {
            return db.ExecuteSprocAccessor(DBRoutine.MATERIALOUTWARDPRODUCTCOST, MapBuilder<MaterialInwardProductCost>.BuildAllProperties()
                , dashBoardParams.Branch
                 , dashBoardParams.ProductCode
                , dashBoardParams.Year
                , dashBoardParams.FromMonth, dashBoardParams.ToMonth).ToList();
        }
        public List<MaterialInwardInvoices> GetMaterialOutwardInvoicesMonthWise(MaterialInwardDashboardDTO dashBoardParams)
        {
            return db.ExecuteSprocAccessor(DBRoutine.MATERIALOUTWARDINVOICES, MapBuilder<MaterialInwardInvoices>.BuildAllProperties()
                , dashBoardParams.Branch
                 , dashBoardParams.ProductCode
                , dashBoardParams.Year
                  , dashBoardParams.FromMonth, dashBoardParams.ToMonth).ToList();
        }
        public List<RMAGenerationReturnedValues> GetRMAGenerationValues(RMAGenerationReceiveDTO rmaGenerationReceiveDTO)
        {
            return db.ExecuteSprocAccessor(DBRoutine.RMAGENERATIONVALUES, MapBuilder<RMAGenerationReturnedValues>.BuildAllProperties(),
                rmaGenerationReceiveDTO.Branch,
                rmaGenerationReceiveDTO.ProductCode,
                rmaGenerationReceiveDTO.FromMonth,
                rmaGenerationReceiveDTO.ToMonth,
                rmaGenerationReceiveDTO.Year).ToList();
        }
        public List<RMAGenerationReturnedValues> GetRMAReturnedValues(RMAGenerationReceiveDTO rmaGenerationReceiveDTO)
        {
            return db.ExecuteSprocAccessor(DBRoutine.RMARETURNEDVALUES, MapBuilder<RMAGenerationReturnedValues>.BuildAllProperties(),
                rmaGenerationReceiveDTO.Branch,
                rmaGenerationReceiveDTO.ProductCode,
                rmaGenerationReceiveDTO.FromMonth,
                rmaGenerationReceiveDTO.ToMonth,
                rmaGenerationReceiveDTO.Year).ToList();
        }
        public List<RMACreditNoteProductCategory> GetRMACreditNoteProductCategory(RMACreditNoteDTO rmaCreditNoteDTO)
        {
            return db.ExecuteSprocAccessor(DBRoutine.RMACREDITNOTEPRODUCTCATEGORY, MapBuilder<RMACreditNoteProductCategory>.BuildAllProperties(),
                rmaCreditNoteDTO.Branch,
                rmaCreditNoteDTO.ProductCode,
                rmaCreditNoteDTO.FromMonth,
                rmaCreditNoteDTO.ToMonth,
                rmaCreditNoteDTO.Year).ToList();
        }
        public List<RMACreditNoteValue> GetRMACreditNoteValue(RMACreditNoteDTO rmaCreditNoteDTO)
        {
            return db.ExecuteSprocAccessor(DBRoutine.RMACREDITNOTEVALUE, MapBuilder<RMACreditNoteValue>.BuildAllProperties(),
                rmaCreditNoteDTO.Branch,
                rmaCreditNoteDTO.ProductCode,
                rmaCreditNoteDTO.FromMonth,
                rmaCreditNoteDTO.ToMonth,
                rmaCreditNoteDTO.Year).ToList();
        }

        public List<MaterialInandOutYear> GetYear()
        {
            return db.ExecuteSprocAccessor(DBRoutine.GETMATERIALINANDOUTYEAR, MapBuilder<MaterialInandOutYear>.BuildAllProperties()).ToList();
        }

        public List<ProductsCategoryRMAReceiptandValue> GetProductCategoryRMAReceipt(ProductCategoryRMAReceiptandValueDTO productCategoryRMAPriceandValueDTO)
        {
            return db.ExecuteSprocAccessor(DBRoutine.PRODUCTSCATEGORYRMARECEIPT, MapBuilder<ProductsCategoryRMAReceiptandValue>.MapAllProperties().DoNotMap(x => x.ProductValue).Build(),
                productCategoryRMAPriceandValueDTO.Branch,
                productCategoryRMAPriceandValueDTO.ProductCode,
                productCategoryRMAPriceandValueDTO.Year,
                productCategoryRMAPriceandValueDTO.FromMonth,
                productCategoryRMAPriceandValueDTO.ToMonth).ToList();
        }
        public List<ProductsCategoryRMAReceiptandValue> GetProductCategoryRMAValue(ProductCategoryRMAReceiptandValueDTO productCategoryRMAPriceandValueDTO)
        {
            return db.ExecuteSprocAccessor(DBRoutine.PRODUCTSCATEGORYRMAVALUE, MapBuilder<ProductsCategoryRMAReceiptandValue>.MapAllProperties().DoNotMap(x => x.ProductQty).Build(),
                productCategoryRMAPriceandValueDTO.Branch,
                productCategoryRMAPriceandValueDTO.ProductCode,
                productCategoryRMAPriceandValueDTO.Year,
                productCategoryRMAPriceandValueDTO.FromMonth,
                productCategoryRMAPriceandValueDTO.ToMonth).ToList();
        }
        public List<ProductCategoryRMAYear> GetRMAYear()
        {
            return db.ExecuteSprocAccessor(DBRoutine.PRODUCTCATEGORYRMAYEAR, MapBuilder<ProductCategoryRMAYear>.BuildAllProperties()).ToList();
        }
        public List<ProductCategoryMonthWiseRMAPriceandValue> GetMonthWiseProductCategoryRMAReceipt(ProductCategoryMonthWiseRMAReceiptandValueDTO productCategoryMonthWiseRMAPriceandValueDTO)
        {
            return db.ExecuteSprocAccessor(DBRoutine.PRODUCTSCATEGORYMONTHWISERMARECEIPT, MapBuilder<ProductCategoryMonthWiseRMAPriceandValue>.MapAllProperties().DoNotMap(x => x.ProductValue).Build(),
                productCategoryMonthWiseRMAPriceandValueDTO.Branch,
                productCategoryMonthWiseRMAPriceandValueDTO.ProductCode,
                productCategoryMonthWiseRMAPriceandValueDTO.FromMonth,
                productCategoryMonthWiseRMAPriceandValueDTO.ToMonth,
                productCategoryMonthWiseRMAPriceandValueDTO.Year).ToList();
        }
        public List<ProductCategoryMonthWiseRMAPriceandValue> GetMonthWiseProductCategoryRMAValue(ProductCategoryMonthWiseRMAReceiptandValueDTO productCategoryMonthWiseRMAPriceandValueDTO)
        {
            return db.ExecuteSprocAccessor(DBRoutine.PRODUCTSCATEGORYMONTHWISERMAVALUE, MapBuilder<ProductCategoryMonthWiseRMAPriceandValue>.MapAllProperties().DoNotMap(x => x.ProductQty).Build(),
                productCategoryMonthWiseRMAPriceandValueDTO.Branch,
                productCategoryMonthWiseRMAPriceandValueDTO.ProductCode,
                productCategoryMonthWiseRMAPriceandValueDTO.FromMonth,
                productCategoryMonthWiseRMAPriceandValueDTO.ToMonth,
                productCategoryMonthWiseRMAPriceandValueDTO.Year).ToList();
        }
        public List<ProductCategoryRMAYear> GetRMAOutwardYear()
        {
            return db.ExecuteSprocAccessor(DBRoutine.RMAOUTWARDYEAR, MapBuilder<ProductCategoryRMAYear>.BuildAllProperties()).ToList();
        }
    }
}
