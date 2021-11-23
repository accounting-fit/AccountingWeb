using AccountingWeb.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using AccountingWeb.GlobalElemnts;
using AccountingWeb.Models.EntityModels;
using System.IO;
using ClosedXML.Excel;
using System.Data;
using AccountingWeb.Models.CommonModels;

namespace AccountingWeb.ApiController
{
    [Route("[controller]")]
    [ApiController]
    public class ApiCustomerSupplierController : ControllerBase
    {

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            string SelectedAllDataQuery = @"SELECT * FROM [AccountingObject] ORDER BY ID Desc";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var dataList = await con.QueryAsync<AccountingObjectViewModel>(SelectedAllDataQuery);

                    return Ok(new { ok = false, AllDataList = dataList.ToList()});
                }
                catch (Exception ex)
                {

                    return BadRequest(ex);
                }
                finally
                {
                    await con.CloseAsync();
                }
            }

        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save(AccountingObjectViewModel model)
        {

            AccountingObjectViewModel entity = new AccountingObjectViewModel()
            {
                ID = SequentialGuid.NewGuid(),
                BranchID = model.BranchID,
                AccountingObjectCode = model.AccountingObjectCode,
                AccountingObjectCategory = model.AccountingObjectCategory,
                EmployeeBirthday = model.EmployeeBirthday,
                Address = model.Address,
                Tel = model.Tel,
                Fax = model.Fax,
                Email = model.Email,
                Website = model.Website,
                BankAccount = model.BankAccount,
                BankName = model.BankName,
                TaxCode = model.TaxCode,
                Description = model.Description,
                ContactName = model.ContactName,
                ContactTitle = model.ContactTitle,
                ContactSex = model.ContactSex,
                ContactMobile = model.ContactMobile,
                ContactEmail = model.ContactEmail,
                ContactHomeTel = model.ContactHomeTel,
                ContactOfficeTel = model.ContactOfficeTel,
                ContactAddress = model.ContactAddress,
                ScaleType = model.ScaleType,
                ObjectType = model.ObjectType,
                IsEmployee = model.IsEmployee??false,
                IdentificationNo = model.IdentificationNo,
                IssueDate = model.IssueDate,
                IssueBy = model.IssueBy,
                DepartmentID = model.DepartmentID,
                IsInsured = model.IsInsured??false,
                IsLabourUnionFree = model.IsLabourUnionFree??false,
                FamilyDeductionAmount = model.FamilyDeductionAmount??"0",
                MaximizaDebtAmount = model.MaximizaDebtAmount,
                DueTime = model.DueTime,
                AccountObjectGroupID = model.AccountObjectGroupID,
                PaymentClauseID = model.PaymentClauseID,
                IsActive = true,
                NumberOfDependent = model.NumberOfDependent,
                AgreementSalary = model.AgreementSalary,
                InsuranceSalary = model.InsuranceSalary,
                SalaryCoefficient = model.SalaryCoefficient,
                IsUnofficialStaff = model.IsUnofficialStaff,
                AccountingObjectName = model.AccountingObjectName
            };

            string inserQuery = @"INSERT INTO dbo.AccountingObject
                                 (ID,BranchID,AccountingObjectCode,AccountingObjectCategory,EmployeeBirthday
                                 ,Address ,Tel ,Fax ,Email ,Website ,BankAccount ,BankName ,TaxCode ,Description
                                 ,ContactName ,ContactTitle ,ContactSex ,ContactMobile ,ContactEmail ,ContactHomeTel ,ContactOfficeTel
                                 ,ContactAddress ,ScaleType ,ObjectType ,IsEmployee ,IdentificationNo ,IssueDate
                                 ,IssueBy ,DepartmentID ,IsInsured ,IsLabourUnionFree ,FamilyDeductionAmount ,MaximizaDebtAmount
                                 ,DueTime ,AccountObjectGroupID ,PaymentClauseID ,IsActive ,NumberOfDependent
                                 ,AgreementSalary ,InsuranceSalary ,SalaryCoefficient ,IsUnofficialStaff ,AccountingObjectName)
                                 Values(
                                 @ID,@BranchID,@AccountingObjectCode,@AccountingObjectCategory,@EmployeeBirthday
                                 ,@Address ,@Tel ,@Fax ,@Email ,@Website ,@BankAccount ,@BankName ,@TaxCode ,@Description
                                 ,@ContactName ,@ContactTitle ,@ContactSex ,@ContactMobile ,@ContactEmail ,@ContactHomeTel ,@ContactOfficeTel
                                 ,@ContactAddress ,@ScaleType ,@ObjectType ,@IsEmployee ,@IdentificationNo ,@IssueDate
                                 ,@IssueBy ,@DepartmentID ,@IsInsured ,@IsLabourUnionFree ,@FamilyDeductionAmount ,@MaximizaDebtAmount
                                 ,@DueTime ,@AccountObjectGroupID ,@PaymentClauseID ,@IsActive ,@NumberOfDependent
                                 ,@AgreementSalary ,@InsuranceSalary ,@SalaryCoefficient ,@IsUnofficialStaff ,@AccountingObjectName)
                                 ";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                using (var trn = await con.BeginTransactionAsync())
                {
                    try
                    {
                        int rowAffect = await con.ExecuteAsync(inserQuery, entity, trn);
                        await trn.CommitAsync();
                        if (rowAffect > 0)
                        {
                            return Ok(new { ok = true });
                        }
                        else
                        {
                            return Ok(new { ok = false });
                        }
                    }
                    catch (Exception ex)
                    {
                        await trn.RollbackAsync();
                        return BadRequest(ex);
                    }
                    finally
                    {
                        await con.CloseAsync();
                    }
                }
            }

        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {             
            string selectedData = @"select * from [AccountingObject] where id='" + id + "'" + "";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var singleData = await con.QueryAsync<AccountingObjectViewModel>(selectedData);
                    return Ok(new { ok = false, SingleData = singleData.FirstOrDefault() });
                }
                catch (Exception ex)
                {

                    return BadRequest(ex);
                }
                finally
                {
                    await con.CloseAsync();
                }
            }

        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(AccountingObjectViewModel model)
        {
            AccountingObjectViewModel entity = new AccountingObjectViewModel()
            {
                ID = model.ID,
                BranchID = model.BranchID,
                AccountingObjectCode = model.AccountingObjectCode,
                AccountingObjectCategory = model.AccountingObjectCategory,
                EmployeeBirthday = model.EmployeeBirthday,
                Address = model.Address,
                Tel = model.Tel,
                Fax = model.Fax,
                Email = model.Email,
                Website = model.Website,
                BankAccount = model.BankAccount,
                BankName = model.BankName,
                TaxCode = model.TaxCode,
                Description = model.Description,
                ContactName = model.ContactName,
                ContactTitle = model.ContactTitle,
                ContactSex = model.ContactSex,
                ContactMobile = model.ContactMobile,
                ContactEmail = model.ContactEmail,
                ContactHomeTel = model.ContactHomeTel,
                ContactOfficeTel = model.ContactOfficeTel,
                ContactAddress = model.ContactAddress,
                ScaleType = model.ScaleType,
                ObjectType = model.ObjectType,
                IsEmployee = model.IsEmployee,
                IdentificationNo = model.IdentificationNo,
                IssueDate = model.IssueDate,
                IssueBy = model.IssueBy,
                DepartmentID = model.DepartmentID,
                IsInsured = model.IsInsured,
                IsLabourUnionFree = model.IsLabourUnionFree,
                FamilyDeductionAmount = model.FamilyDeductionAmount ?? "0",
                MaximizaDebtAmount = model.MaximizaDebtAmount,
                DueTime = model.DueTime,
                AccountObjectGroupID = model.AccountObjectGroupID,
                PaymentClauseID = model.PaymentClauseID,
                IsActive = model.IsActive,
                NumberOfDependent = model.NumberOfDependent,
                AgreementSalary = model.AgreementSalary,
                InsuranceSalary = model.InsuranceSalary,
                SalaryCoefficient = model.SalaryCoefficient,
                IsUnofficialStaff = model.IsUnofficialStaff,
                AccountingObjectName = model.AccountingObjectName
            };

            string updateQuery = @"UPDATE [dbo].[AccountingObject]
                                   SET [BranchID] =@BranchID
                                      ,[AccountingObjectCode] =@AccountingObjectCode
                                      ,[AccountingObjectCategory] =@AccountingObjectCategory
                                      ,[EmployeeBirthday] =@EmployeeBirthday
                                      ,[Address] =@Address
                                      ,[Tel] =@Tel
                                      ,[Fax] =@Fax
                                      ,[Email] =@Email
                                      ,[Website] =@Website
                                      ,[BankAccount] =@BankAccount
                                      ,[BankName] =@BankName
                                      ,[TaxCode] =@TaxCode
                                      ,[Description] =@Description
                                      ,[ContactName] =@ContactName
                                      ,[ContactTitle] =@ContactTitle
                                      ,[ContactSex] =@ContactSex
                                      ,[ContactMobile] =@ContactMobile
                                      ,[ContactEmail] =@ContactEmail
                                      ,[ContactHomeTel] =@ContactHomeTel
                                      ,[ContactOfficeTel] =@ContactOfficeTel
                                      ,[ContactAddress] =@ContactAddress
                                      ,[ScaleType] =@ScaleType
                                      ,[ObjectType] =@ObjectType
                                      ,[IsEmployee] =@IsEmployee
                                      ,[IdentificationNo] =@IdentificationNo
                                      ,[IssueDate] =@IssueDate
                                      ,[IssueBy] =@IssueBy
                                      ,[DepartmentID] =@DepartmentID
                                      ,[IsInsured] =@IsInsured
                                      ,[IsLabourUnionFree] =@IsLabourUnionFree
                                      ,[FamilyDeductionAmount] =@FamilyDeductionAmount
                                      ,[MaximizaDebtAmount] =@MaximizaDebtAmount
                                      ,[DueTime] =@DueTime
                                      ,[AccountObjectGroupID] =@AccountObjectGroupID
                                      ,[PaymentClauseID] =@PaymentClauseID
                                      ,[IsActive] =@IsActive
                                      ,[NumberOfDependent] =@NumberOfDependent
                                      ,[AgreementSalary] =@AgreementSalary
                                      ,[InsuranceSalary] =@InsuranceSalary
                                      ,[SalaryCoefficient] =@SalaryCoefficient
                                      ,[IsUnofficialStaff] =@IsUnofficialStaff
                                      ,[AccountingObjectName] =@AccountingObjectName where id=@ID";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))            {
                await con.OpenAsync();
                using (var trn = await con.BeginTransactionAsync())
                {
                    try
                    {
                        int rowAffect = await con.ExecuteAsync(updateQuery, entity, trn);
                        await trn.CommitAsync();
                        if (rowAffect > 0)
                        {
                            return Ok(new { ok = true });
                        }
                        else
                        {
                            return Ok(new { ok = false });
                        }
                    }
                    catch (Exception ex)
                    {
                        await trn.RollbackAsync();
                        return BadRequest(ex);
                    }
                    finally
                    {
                        await con.CloseAsync();
                    }
                }
            }

        }

        [HttpPost]
        [Route("DeleteById/{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {

            string deleteQuery = @"Delete  [dbo].[AccountingObject] where [Id]='" + id + "'" + "";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                using (var trn = await con.BeginTransactionAsync())
                {
                    try
                    {
                        int rowAffect = await con.ExecuteAsync(deleteQuery, null, trn);
                        await trn.CommitAsync();
                        if (rowAffect > 0)
                        {
                            return Ok(new { ok = true });
                        }
                        else
                        {
                            return Ok(new { ok = false });
                        }
                    }
                    catch (Exception ex)
                    {
                        await trn.RollbackAsync();
                        return BadRequest(ex);
                    }
                    finally
                    {
                        await con.CloseAsync();
                    }
                }
            }
        }

        [HttpGet]
        [Route("ExportExcel")]
        public async Task<FileResult> ExportExcel()
        {
            string selectMaterialGoods = @"SELECT * FROM [AccountingObject]";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    DataTable table = new DataTable();
                    table.Load(await con.ExecuteReaderAsync(selectMaterialGoods));


                    using (var workbook = new XLWorkbook())
                    {

                        var worksheet = workbook.Worksheets.Add("CustomerSupplierList");
                        var currentRow = 1;
                        worksheet.Cell(currentRow, 1).Value = "AccountingObjectCode";
                        worksheet.Cell(currentRow, 2).Value = "Tel";
                        worksheet.Cell(currentRow, 3).Value = "Fax";
                        worksheet.Cell(currentRow, 4).Value = "AccountingObjectName";
                        worksheet.Cell(currentRow, 5).Value = "Address";


                        worksheet.Cell(currentRow, 6).Value = "Email";
                        worksheet.Cell(currentRow, 7).Value = "Website";
                        worksheet.Cell(currentRow, 8).Value = "MaximizaDebtAmount";
                        worksheet.Cell(currentRow, 9).Value = "AccountingObjectName";
                        worksheet.Cell(currentRow, 10).Value = "PaymentClauseID";

                        worksheet.Cell(currentRow, 11).Value = "AccountObjectGroupID";
                        worksheet.Cell(currentRow, 12).Value = "IsEmployee";
                        worksheet.Cell(currentRow, 13).Value = "ContactName";
                        worksheet.Cell(currentRow, 14).Value = "ContactTitle";
                        worksheet.Cell(currentRow, 15).Value = "ContactSex";

                        worksheet.Cell(currentRow, 16).Value = "ContactAddress";
                        worksheet.Cell(currentRow, 17).Value = "ContactMobile";
                        worksheet.Cell(currentRow, 18).Value = "ContactEmail";
                        worksheet.Cell(currentRow, 19).Value = "ContactHomeTel";
                        worksheet.Cell(currentRow, 20).Value = "ContactOfficeTel";


                        worksheet.Cell(currentRow, 21).Value = "IdentificationNo";
                        worksheet.Cell(currentRow, 22).Value = "IssueDate";
                        worksheet.Cell(currentRow, 23).Value = "IssueBy";
                        worksheet.Cell(currentRow, 24).Value = "TaxCode";
                        worksheet.Cell(currentRow, 25).Value = "ScaleType";

                        worksheet.Cell(currentRow, 26).Value = "ObjectType";
                        worksheet.Cell(currentRow, 27).Value = "IsActive";



                        worksheet.Row(currentRow).Cells(1, 27).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 27).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 27).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 27).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                        worksheet.Row(1).Cells(1, 27).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                        worksheet.Row(1).Cells(1, 27).Style.Font.Bold = true;

                        foreach (DataRow row in table.Rows)
                        {
                            currentRow++;
                            worksheet.Cell(currentRow, 1).Value = row["AccountingObjectCode"]?.ToString();
                            worksheet.Cell(currentRow, 2).Value = row["Tel"]?.ToString();
                            worksheet.Cell(currentRow, 3).Value = row["Fax"]?.ToString();
                            worksheet.Cell(currentRow, 4).Value = row["AccountingObjectName"]?.ToString();
                            worksheet.Cell(currentRow, 5).Value = row["Address"]?.ToString();


                            worksheet.Cell(currentRow, 6).Value = row["Email"]?.ToString();
                            worksheet.Cell(currentRow, 7).Value = row["Website"]?.ToString();
                            worksheet.Cell(currentRow, 8).Value = row["MaximizaDebtAmount"]?.ToString();
                            worksheet.Cell(currentRow, 9).Value = row["AccountingObjectName"]?.ToString();
                            worksheet.Cell(currentRow, 10).Value = row["PaymentClauseID"]?.ToString();

                            worksheet.Cell(currentRow, 11).Value = row["AccountObjectGroupID"]?.ToString();
                            worksheet.Cell(currentRow, 12).Value = row["IsEmployee"]?.ToString();
                            worksheet.Cell(currentRow, 13).Value = row["ContactName"]?.ToString();
                            worksheet.Cell(currentRow, 14).Value = row["ContactTitle"]?.ToString();
                            worksheet.Cell(currentRow, 15).Value = row["ContactSex"]?.ToString();

                            worksheet.Cell(currentRow, 16).Value = row["ContactAddress"]?.ToString();
                            worksheet.Cell(currentRow, 17).Value = row["ContactMobile"]?.ToString();
                            worksheet.Cell(currentRow, 18).Value = row["ContactEmail"]?.ToString();
                            worksheet.Cell(currentRow, 19).Value = row["ContactHomeTel"]?.ToString();
                            worksheet.Cell(currentRow, 20).Value = row["ContactOfficeTel"]?.ToString();


                            worksheet.Cell(currentRow, 21).Value = row["IdentificationNo"]?.ToString();
                            worksheet.Cell(currentRow, 22).Value = row["IssueDate"]?.ToString();
                            worksheet.Cell(currentRow, 23).Value = row["IssueBy"]?.ToString();
                            worksheet.Cell(currentRow, 24).Value = row["TaxCode"]?.ToString();
                            worksheet.Cell(currentRow, 25).Value = row["ScaleType"]?.ToString();

                            worksheet.Cell(currentRow, 26).Value = row["ObjectType"]?.ToString();
                            worksheet.Cell(currentRow, 27).Value = row["IsActive"]?.ToString();

                           

                            worksheet.Row(currentRow).Cells(1,27).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1,27).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1,27).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1,27).Style.Border.LeftBorder = XLBorderStyleValues.Thin;


                        }
                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();

                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "CustomerSupplierList.xlsx");
                        }
                    }
                }
                catch (Exception ex)
                {

                    return null;
                }
                finally
                {
                    await con.CloseAsync();
                }
            }

        }

        [HttpGet]
        [Route("GeneratedCode")]
        public async Task<IActionResult> GeneratedCode()
        {
            string countDataQuery = @"SELECT COUNT(1) FROM [AccountingObject]";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var count = await con.QueryAsync<int>(countDataQuery);

                    return Ok(new { ok = false, GeneratedCode =(count.FirstOrDefault()+1).ToString("000000") });
                }
                catch (Exception ex)
                {

                    return BadRequest(ex);
                }
                finally
                {
                    await con.CloseAsync();
                }
            }
        }

        [HttpGet]
        [Route("GeneratedTaxCode")]
        public  IActionResult GeneratedTaxCode()
        {
            Random random = new Random();
            int provineCode = random.Next(0, 100);
            int randomcode=  random.Next(0, 10000000);
            int testCode=random.Next(0, 10);
            int branchCode= random.Next(0, 10);
            var GeneratedTaxCode = provineCode.ToString("00") + randomcode.ToString("0000000") + testCode.ToString("0") +"-"+ branchCode.ToString("000");
                try
                {                 

                    return Ok(new { ok = false, GeneratedTaxCode = GeneratedTaxCode });
                }
                catch (Exception ex)
                {

                    return BadRequest(ex);
                }
                finally
                {
                   
                }            
        }


        [HttpGet]
        [Route("GetAllAccountingObjectGroup")]
        public  async Task<IActionResult> GetAllAccountingObjectGroup()
        {
            string SelectedAllDataQuery = @"SELECT ID as Id, AccountingObjectGroupName as Text FROM [AccountingObjectGroup] ORDER BY AccountingObjectGroupName";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var dataList = await con.QueryAsync<DropDownGuidStringViewModel>(SelectedAllDataQuery);

                    return Ok(new { ok = false, GetAllAccountingObjectGroup = dataList.ToList() });
                }
                catch (Exception ex)
                {

                    return BadRequest(ex);
                }
                finally
                {
                    await con.CloseAsync();
                }
            }

        }




        [HttpGet]
        [Route("GetAllPaymentClause")]
        public async Task<IActionResult> GetAllPaymentClause()
        {
            string SelectedAllDataQuery = @"SELECT ID as Id, PaymentClauseName as Text FROM [PaymentClause] ORDER BY PaymentClauseName";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var dataList = await con.QueryAsync<DropDownGuidStringViewModel>(SelectedAllDataQuery);

                    return Ok(new { ok = false, GetAllPaymentClause = dataList.ToList() });
                }
                catch (Exception ex)
                {

                    return BadRequest(ex);
                }
                finally
                {
                    await con.CloseAsync();
                }
            }

        }
        
    }
}
