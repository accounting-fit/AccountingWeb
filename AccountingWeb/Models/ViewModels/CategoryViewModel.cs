using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingWeb.Models.ViewModels
{
    public class CategoryViewModel
    {
        public string Nhom { get; set; }
        public bool NhomisSalesDiscountPolicy { get; set; }
        public string customCode { get; set; }
        public string expanceAccountId { get; set; }
        public string itemSource { get; set; }
        public string matGoodsCatId { get; set; }
        public string matGoodsCode { get; set; }
        public string matGoodsName { get; set; }
        public string minimumStock { get; set; }
        public string purchaseDiscountRate { get; set; }
        public string purchasePrice { get; set; }
        public string repositoryAccountId { get; set; }
        public string repositoryId { get; set; }
        public string revenueAccountId { get; set; }
        public string salesDiscountRate { get; set; }
        public string salesPrice { get; set; }
        public string taxRate { get; set; }
        public string warrantyTime { get; set; }
        public string unit { get; set; }
    }
}
