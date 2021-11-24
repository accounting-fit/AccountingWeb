using AccountingWeb.GlobalElemnts;
using AccountingWeb.Models.CommonModels;
using AccountingWeb.Models.ViewModels;
using ClosedXML.Excel;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingWeb.ApiController
{
    [Route("[controller]")]
    [ApiController]
    public class ApiAccountingObjectBankAccountController : ControllerBase
    {
        [HttpGet]
        [Route("GetAll/{id}")]
        public async Task<IActionResult> GetAll(string id)
        {
            string SelectedAllDataQuery = @"SELECT * FROM [AccountingObjectBankAccount] WHERE AccountingObjectID='" + id+"' ORDER BY ID Desc";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var dataList = await con.QueryAsync<AccountingObjectBankAccountViewModel>(SelectedAllDataQuery);

                    return Ok(new { ok = false, AllDataList = dataList.ToList() });
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
        public async Task<IActionResult> Save(AccountingObjectBankAccountViewModel model)
        {

            AccountingObjectBankAccountViewModel entity = new AccountingObjectBankAccountViewModel()
            {
                ID = SequentialGuid.NewGuid(),
                AccountingObjectID=model.AccountingObjectID,
                BankAccount=model.BankAccount,
                BankName=model.BankName,
                BankBranchName=model.BankBranchName,
                AccountHolderName=model.AccountHolderName,               
                OrderPriority = 0,
                IsSelect=false
            };

            string inserQuery = @"INSERT INTO [dbo].[AccountingObjectBankAccount]([ID],[AccountingObjectID],[BankAccount],[BankName],[BankBranchName],[AccountHolderName],[OrderPriority],[IsSelect])
                                VALUES (@ID,@AccountingObjectID,@BankAccount,@BankName,@BankBranchName,@AccountHolderName,@OrderPriority,@IsSelect)";
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
            string selectedData = @"select * from [AccountingObjectBankAccount] where id='" + id + "'" + "";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var singleData = await con.QueryAsync<AccountingObjectBankAccountViewModel>(selectedData);
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
        public async Task<IActionResult> Update(AccountingObjectBankAccountViewModel model)
        {
            AccountingObjectBankAccountViewModel entity = new AccountingObjectBankAccountViewModel()
            {
                ID = model.ID,
                AccountingObjectID = model.AccountingObjectID,
                BankAccount = model.BankAccount,
                BankName = model.BankName,
                BankBranchName = model.BankBranchName,
                AccountHolderName=model.AccountHolderName,
                IsSelect = model.IsSelect,
                OrderPriority = model.OrderPriority
            };

            string updateQuery = @"UPDATE  [dbo].[AccountingObjectBankAccount]
                                SET [AccountingObjectID]=@AccountingObjectID,
                                    [BankAccount]=@BankAccount,
                                    [BankName]=@BankName,
                                    [BankBranchName]=@BankBranchName,
                                    [AccountHolderName]=@AccountHolderName,
                                    [IsSelect]=@IsSelect, 
                                    [OrderPriority]=@OrderPriority
                                    where id=@ID";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
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

            string deleteQuery = @"Delete  [dbo].[AccountingObjectBankAccount] where [Id]='" + id + "'" + "";
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
        [Route("ExportExcel/{id}")]
        public async Task<FileResult> ExportExcel(string id)
        {
            string selectMaterialGoods = @"SELECT * FROM [AccountingObjectBankAccount] WHERE AccountingObjectID='" + id + "' ORDER BY ID Desc"; ;
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    DataTable table = new DataTable();
                    table.Load(await con.ExecuteReaderAsync(selectMaterialGoods));


                    using (var workbook = new XLWorkbook())
                    {

                        var worksheet = workbook.Worksheets.Add("BankAccountList");
                        var currentRow = 1;

                        worksheet.Cell(currentRow, 1).Value = "Account Number";
                        worksheet.Cell(currentRow, 2).Value = "Bank Name";
                        worksheet.Cell(currentRow, 3).Value = "Bank Branch Name";
                        worksheet.Cell(currentRow, 4).Value = "Account Holder Name";
                        worksheet.Cell(currentRow, 5).Value = "Is Select";
                        worksheet.Cell(currentRow, 6).Value = "Order Priority";

                        worksheet.Row(currentRow).Cells(1, 6).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                        worksheet.Row(1).Cells(1, 6).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                        worksheet.Row(1).Cells(1, 6).Style.Font.Bold = true;

                        foreach (DataRow row in table.Rows)
                        {
                            currentRow++;

                            worksheet.Cell(currentRow, 1).Value = row["BankAccount"]?.ToString();
                            worksheet.Cell(currentRow, 2).Value = row["BankName"]?.ToString();
                            worksheet.Cell(currentRow, 3).Value = row["BankBranchName"]?.ToString();
                            worksheet.Cell(currentRow, 4).Value = row["AccountHolderName"]?.ToString();
                            worksheet.Cell(currentRow, 5).Value = row["IsSelect"]?.ToString() == "True" ? 1 : 0;
                            worksheet.Cell(currentRow, 6).Value = row["OrderPriority"]?.ToString();

                            worksheet.Row(currentRow).Cells(1, 6).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1, 6).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;


                        }
                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();

                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "BankAccountList.xlsx");
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
    }
}
