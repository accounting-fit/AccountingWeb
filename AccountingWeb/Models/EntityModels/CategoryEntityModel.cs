using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingWeb.Models.EntityModels
{
    public class CategoryEntityModel
    {
        public Guid ID { get; set; }
        public Guid MaterialGoodsCategoryID { get; set; }
        public string MaterialGoodsCode { get; set; }
        public string MaterialGoodsName { get; set; }
        public int? MaterialGoodsType { get; set; }
        public int MaterialToolType { get; set; }
        public string Unit { get; set; }
        public string ConvertUnit { get; set; }
        public decimal ConvertRate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal SalePrice2 { get; set; }
        public decimal SalePrice3 { get; set; }
        public decimal FixedSalePrice { get; set; }
        public decimal SalePriceAfterTax { get; set; }
        public decimal SalePriceAfterTax2 { get; set; }
        public decimal SalePriceAfterTax3 { get; set; }
        public bool IsSalePriceAfterTax { get; set; }
        public Guid? RepositoryID { get; set; }
        public string ReponsitoryAccount { get; set; }
        public string ExpenseAccount { get; set; }
        public string RevenueAccount { get; set; }
        public decimal MinimumStock { get; set; }
        public Guid AccountingObjectID { get; set; }
        public decimal TaxRate { get; set; }
        public int SystemMaterialGoodsType { get; set; }
        public string SaleDescription { get; set; }
        public string PurchaseDescription { get; set; }
        public string ItemSource { get; set; }
        public Guid MaterialGoodsGSTID { get; set; }
        public decimal SaleDiscountRate { get; set; }
        public decimal PurchaseDiscountRate { get; set; }
        public bool IsSaleDiscountPolicy { get; set; }
        public string GuarantyPeriod { get; set; }
        public int CostMethod { get; set; }
        public bool IsActive { get; set; }
        public bool IsSecurity { get; set; }
        public bool PrintMetarial { get; set; }
        public decimal LastPurchasePriceAfterTax { get; set; }
        public string WarrantyTime { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
        public int AllocationTimes { get; set; }
        public decimal AllocatedAmount { get; set; }
        public string AllocationAccount { get; set; }
        public Guid CostSetID { get; set; }
        public int AllocationType { get; set; }
        public string AllocationAwaitAccount { get; set; }
        public Guid CareerGroupID { get; set; }

        [NotMapped]
        public int? stock { get; set; } = 0;
        [NotMapped]
        public int? stockValue { get; set; } = 0;
        [NotMapped]
        public string materialGoodsTypeName { get; set; }

    }
}
