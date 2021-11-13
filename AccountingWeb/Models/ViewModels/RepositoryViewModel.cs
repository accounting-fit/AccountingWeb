using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingWeb.Models.EntityModels
{
    public class RepositoryViewModel
    {
        public Guid? ID { get; set; }
        public Guid? BranchID { get; set; }
        public string RepositoryCode { get; set; }
        public string RepositoryName { get; set; }
        public string Description { get; set; }
        public string DefaultAccount { get; set; }
        public bool? IsActive { get; set; }
    }
}
