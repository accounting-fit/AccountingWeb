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
    public class ApiRepositoryController : ControllerBase
    {

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            string SelectedAllDataQuery = @"SELECT * FROM [Repository] ORDER BY ID Desc";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var dataList = await con.QueryAsync<RepositoryViewModel>(SelectedAllDataQuery);

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
        public async Task<IActionResult> Save(RepositoryViewModel model)
        {

            RepositoryViewModel entity = new RepositoryViewModel()
            {
                ID = SequentialGuid.NewGuid(),
                BranchID =model.BranchID,
                RepositoryCode = model.RepositoryCode,
                RepositoryName = model.RepositoryName,
                Description = model.Description,
                DefaultAccount=model.DefaultAccount,
                IsActive =true
            };

            string inserQuery = @"INSERT INTO [dbo].[Repository]([ID],[BranchID],[RepositoryCode],[RepositoryName],[Description],[DefaultAccount],[IsActive])
                                VALUES (@ID,@BranchID,@RepositoryCode,@RepositoryName,@Description,@DefaultAccount,@IsActive)";
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
            string selectedData = @"select * from [Repository] where id='" + id + "'" + "";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var singleData = await con.QueryAsync<RepositoryViewModel>(selectedData);
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
        public async Task<IActionResult> Update(RepositoryViewModel model)
        {
            RepositoryViewModel entity = new RepositoryViewModel()
            {
                ID = model.ID,
                BranchID = model.BranchID,
                RepositoryCode = model.RepositoryCode,
                RepositoryName = model.RepositoryName,
                Description = model.Description,
                DefaultAccount = model.DefaultAccount,
                IsActive = model.IsActive
            };

            string updateQuery = @"UPDATE  [dbo].[Repository]
                                SET [BranchID]=@BranchID,
                                    [RepositoryCode]=@RepositoryCode,
                                    [RepositoryName]=@RepositoryName,
                                    [Description]=@Description,
                                    [DefaultAccount]=@DefaultAccount,
                                    [IsActive]=@IsActive where id=@ID";
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

            string deleteQuery = @"Delete  [dbo].[Repository] where [Id]='" + id + "'" + "";
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
            string selectMaterialGoods = @"SELECT * FROM [Repository]";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    DataTable table = new DataTable();
                    table.Load(await con.ExecuteReaderAsync(selectMaterialGoods));


                    using (var workbook = new XLWorkbook())
                    {

                        var worksheet = workbook.Worksheets.Add("RepositoryList");
                        var currentRow = 1;

                        worksheet.Cell(currentRow, 1).Value = "Repository Code";
                        worksheet.Cell(currentRow, 2).Value = "Repository Name";
                        worksheet.Cell(currentRow, 3).Value = "Description";
                        worksheet.Cell(currentRow, 4).Value = "Default Account";
                        worksheet.Cell(currentRow, 5).Value = "IsActive";                        

                        worksheet.Row(currentRow).Cells(1, 5).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                        worksheet.Row(1).Cells(1, 5).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                        worksheet.Row(1).Cells(1, 5).Style.Font.Bold = true;

                        foreach (DataRow row in table.Rows)
                        {
                            currentRow++;
                    
                            worksheet.Cell(currentRow, 1).Value = row["RepositoryCode"]?.ToString();
                            worksheet.Cell(currentRow, 2).Value = row["RepositoryName"]?.ToString();
                            worksheet.Cell(currentRow, 3).Value = row["Description"]?.ToString();
                            worksheet.Cell(currentRow, 4).Value = row["DefaultAccount"]?.ToString();
                            worksheet.Cell(currentRow, 5).Value = row["IsActive"]?.ToString()=="True"?1:0;

                            worksheet.Row(currentRow).Cells(1, 5).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1, 5).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1, 5).Style.Border.LeftBorder = XLBorderStyleValues.Thin;


                        }
                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();

                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "RepositoryList.xlsx");
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
