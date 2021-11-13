using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingWeb.Models.ViewModels
{
    public class AccountingObjectViewModel
    {

        public Guid? ID { get; set; }
        public Guid? BranchID { get; set; }
        public string AccountingObjectCode { get; set; }
        public string AccountingObjectCategory { get; set; }
        public string EmployeeBirthday { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string BankAccount { get; set; }
        public string BankName { get; set; }
        public string TaxCode { get; set; }
        public string Description { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string ContactSex { get; set; }
        public string ContactMobile { get; set; }
        public string ContactEmail { get; set; }
        public string ContactHomeTel { get; set; }
        public string ContactOfficeTel { get; set; }
        public string ContactAddress { get; set; }
        public string ScaleType { get; set; }
        public string ObjectType { get; set; }
        public bool? IsEmployee { get; set; }
        public string IdentificationNo { get; set; }
        public string IssueDate { get; set; }
        public string IssueBy { get; set; }
        public Guid? DepartmentID { get; set; }
        public bool? IsInsured { get; set; }
        public bool? IsLabourUnionFree { get; set; }
        public string FamilyDeductionAmount { get; set; }
        public string MaximizaDebtAmount { get; set; }
        public string DueTime { get; set; }
        public Guid? AccountObjectGroupID { get; set; }
        public Guid? PaymentClauseID { get; set; }
        public bool? IsActive { get; set; }
        public string NumberOfDependent { get; set; }
        public string AgreementSalary { get; set; }
        public string InsuranceSalary { get; set; }
        public string SalaryCoefficient { get; set; }
        public bool? IsUnofficialStaff { get; set; }
        public string AccountingObjectName { get; set; }
    }
}
