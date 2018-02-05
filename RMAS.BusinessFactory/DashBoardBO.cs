using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZY.RMAS.Contract;
using EZY.RMAS.DataFactory;

namespace EZY.RMAS.BusinessFactory
{
    public class DashBoardBO
    {
        DashboardDAL dashBoardDAL;
        public DashBoardBO()
        {
            dashBoardDAL = new DashboardDAL();
        }

        public List<MaterialInwardProductQty> GetMaterialInwardProductQtyMonthWise(MaterialInwardDashboardDTO dashBoardParams)
        {
            return dashBoardDAL.GetMaterialInwardProductQtyMonthWise(dashBoardParams);
        }
        public List<MaterialInwardProductCost> GetMaterialInwardProductCostMonthWise(MaterialInwardDashboardDTO dashBoardParams)
        {
            return dashBoardDAL.GetMaterialInwardProductCostMonthWise(dashBoardParams);
        }
        public List<MaterialInwardInvoices> GetMaterialInwardInvoicesMonthWise(MaterialInwardDashboardDTO dashBoardParams)
        {
            return dashBoardDAL.GetMaterialInwardInvoicesMonthWise(dashBoardParams);
        }
        public List<MaterialInwardProductQty> GetMaterialOutwardProductQtyMonthWise(MaterialInwardDashboardDTO dashBoardParams)
        {
            return dashBoardDAL.GetMaterialOutwardProductQtyMonthWise(dashBoardParams);
        }
        public List<MaterialInwardProductCost> GetMaterialOutwardProductCostMonthWise(MaterialInwardDashboardDTO dashBoardParams)
        {
            return dashBoardDAL.GetMaterialOutwardProductCostMonthWise(dashBoardParams);
        }
        public List<MaterialInwardInvoices> GetMaterialOutwardInvoicesMonthWise(MaterialInwardDashboardDTO dashBoardParams)
        {
            return dashBoardDAL.GetMaterialOutwardInvoicesMonthWise(dashBoardParams);
        }
        public List<RMAGenerationReturnedValues> GetRMAGenerationValues(RMAGenerationReceiveDTO rmaGenerationReceiveDTO)
        {
            return dashBoardDAL.GetRMAGenerationValues(rmaGenerationReceiveDTO);
        }
        public List<RMAGenerationReturnedValues> GetRMAReturnedValues(RMAGenerationReceiveDTO rmaGenerationReceiveDTO)
        {
            return dashBoardDAL.GetRMAReturnedValues(rmaGenerationReceiveDTO);
        }
        public List<RMACreditNoteProductCategory> GetRMACreditNoteProductCategory(RMACreditNoteDTO rmaCreditNoteDTO)
        {
            return dashBoardDAL.GetRMACreditNoteProductCategory(rmaCreditNoteDTO);
        }
        public List<RMACreditNoteValue> GetRMACreditNoteValue(RMACreditNoteDTO rmaCreditNoteDTO)
        {
            return dashBoardDAL.GetRMACreditNoteValue(rmaCreditNoteDTO);
        }
        public List<ProductCategoryRMAYear> GetRMAOutwardYear()
        {
            return dashBoardDAL.GetRMAOutwardYear();
        }
        public List<MaterialInandOutYear> GetYear()
        {
            return dashBoardDAL.GetYear();
        }
        public List<ProductsCategoryRMAReceiptandValue> GetProductCategoryRMAReceipt(ProductCategoryRMAReceiptandValueDTO productCategoryRMAPriceandValueDTO)
        {
            return dashBoardDAL.GetProductCategoryRMAReceipt(productCategoryRMAPriceandValueDTO);
        }
        public List<ProductsCategoryRMAReceiptandValue> GetProductCategoryRMAValue(ProductCategoryRMAReceiptandValueDTO productCategoryRMAPriceandValueDTO)
        {
            return dashBoardDAL.GetProductCategoryRMAValue(productCategoryRMAPriceandValueDTO);
        }
        public List<ProductCategoryRMAYear> GetRMAYear()
        {
            return dashBoardDAL.GetRMAYear();
        }
        public List<ProductCategoryMonthWiseRMAPriceandValue> GetMonthWiseProductCategoryRMAReceipt(ProductCategoryMonthWiseRMAReceiptandValueDTO productCategoryMonthWiseRMAPriceandValueDTO)
        {
            return dashBoardDAL.GetMonthWiseProductCategoryRMAReceipt(productCategoryMonthWiseRMAPriceandValueDTO);
        }
        public List<ProductCategoryMonthWiseRMAPriceandValue> GetMonthWiseProductCategoryRMAValue(ProductCategoryMonthWiseRMAReceiptandValueDTO productCategoryMonthWiseRMAPriceandValueDTO)
        {
            return dashBoardDAL.GetMonthWiseProductCategoryRMAValue(productCategoryMonthWiseRMAPriceandValueDTO);
        }
    }
}
