using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingWeb.Models.EntityModels
{
    public class MaterialGoodsCategoryEntityModel
    {
        public Guid ID { get; set; }
        public string MaterialGoodsCategoryCode { get; set; }
        public string MaterialGoodsCategoryName { get; set; }
        public Guid ParentID { get; set; }
        public bool IsParentNode { get; set; }
        public string OrderFixCode { get; set; }
        public int Grade { get; set; }
        public bool IsTool { get; set; }
        public bool IsActive { get; set; }
        public bool IsSecurity { get; set; }
    }
}
