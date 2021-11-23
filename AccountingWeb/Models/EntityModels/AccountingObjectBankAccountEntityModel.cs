using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingWeb.Models.EntityModels
{
    public class AccountingObjectBankAccountEntityModel
    {
        public Guid ID { get; set; }
        public Guid AccountingObjectID { get; set; }
        public string BankAccount { get; set; }
        public string BankName { get; set; }
        public string BankBranchName { get; set; }
        public string AccountHolderName { get; set; }
        public int? OrderPriority { get; set; }
        public bool? IsSelect { get; set; }

    }
}
