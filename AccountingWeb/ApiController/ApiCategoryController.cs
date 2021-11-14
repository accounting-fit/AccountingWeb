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
    public class ApiCategoryController : ControllerBase
    {

        [HttpGet]
        [Route("GetIndexTable")]
        public async Task<IActionResult> GetIndexTable()
        {
            string selectMaterialGoods = @"SELECT * FROM [MaterialGoods] ORDER BY ID Desc";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var categoryEntityModelData = await con.QueryAsync<CategoryEntityModel>(selectMaterialGoods);

                    return Ok(new { ok = false, categoryEntityModelList = categoryEntityModelData.ToList()});
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
        [Route("GetInititalData")]
        public async Task<IActionResult> GetInititalData()
        {
            string selectMaterialGoodsCategory = @"select * from [MaterialGoodsCategory]";
            string selectRepository = @"select * from [Repository]";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var materialGoodsCategoryData = await con.QueryAsync<MaterialGoodsCategoryEntityModel>(selectMaterialGoodsCategory);

                    var repositoryData = await con.QueryAsync<RepositoryEntityModel>(selectRepository);
                    return Ok(new { ok = false, materialGoodsCategoryList = materialGoodsCategoryData.ToList(), repositoryList= repositoryData.ToList() });
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
        [Route("SaveCategory")]
        public async Task<IActionResult> SaveCategory(CategoryViewModel model)
        {
            CategoryEntityModel entity = new CategoryEntityModel()
            {
                ID = SequentialGuid.NewGuid(),
                MaterialGoodsCode = model.matGoodsCode,
                MaterialGoodsCategoryID = string.IsNullOrEmpty(model.matGoodsCatId)?Guid.Empty: Guid.Parse(model.matGoodsCatId),
                MaterialGoodsName = model.matGoodsName,
                Unit = model.unit,
                PurchasePrice = string.IsNullOrEmpty(model.purchasePrice) ? 0 : decimal.Parse(model.purchasePrice),
                SalePrice = string.IsNullOrEmpty(model.salesPrice) ? 0 : decimal.Parse(model.salesPrice),
                RepositoryID = string.IsNullOrEmpty(model.repositoryId) ? Guid.Empty : Guid.Parse(model.repositoryId),
                ReponsitoryAccount = model.repositoryAccountId,
                ExpenseAccount = model.expanceAccountId,
                RevenueAccount = model.revenueAccountId,
                MinimumStock = string.IsNullOrEmpty(model.minimumStock) ? 0 : decimal.Parse(model.minimumStock),
                TaxRate = string.IsNullOrEmpty(model.taxRate) ? 0 : decimal.Parse(model.taxRate),
                ItemSource = model.itemSource,
                SaleDiscountRate = string.IsNullOrEmpty(model.salesDiscountRate) ? 0 : decimal.Parse(model.salesDiscountRate),
                PurchaseDiscountRate = string.IsNullOrEmpty(model.purchaseDiscountRate) ? 0 : decimal.Parse(model.purchaseDiscountRate),
                IsSaleDiscountPolicy = model.NhomisSalesDiscountPolicy,
                IsActive = true,
                WarrantyTime = model.warrantyTime
            };

            string inserQuery = @"INSERT INTO [dbo].[MaterialGoods]
           ([ID],[MaterialGoodsCategoryID],[MaterialGoodsCode],[MaterialGoodsName],[MaterialGoodsType],[MaterialToolType],[Unit],[ConvertUnit],[ConvertRate],[PurchasePrice],[SalePrice],[SalePrice2],[SalePrice3],[FixedSalePrice],[SalePriceAfterTax],[SalePriceAfterTax2],[SalePriceAfterTax3],[IsSalePriceAfterTax],[RepositoryID],[ReponsitoryAccount],[ExpenseAccount],[RevenueAccount],[MinimumStock],[AccountingObjectID],[TaxRate],[SystemMaterialGoodsType],[SaleDescription],[PurchaseDescription],[ItemSource],[MaterialGoodsGSTID],[SaleDiscountRate],[PurchaseDiscountRate],[IsSaleDiscountPolicy],[GuarantyPeriod],[CostMethod],[IsActive],[IsSecurity],[PrintMetarial],[LastPurchasePriceAfterTax],[WarrantyTime],[Quantity],[UnitPrice],[Amount],[AllocationTimes],[AllocatedAmount],[AllocationAccount],[CostSetID],[AllocationType],[AllocationAwaitAccount],[CareerGroupID ])
     VALUES           (@ID,@MaterialGoodsCategoryID,@MaterialGoodsCode,@MaterialGoodsName,@MaterialGoodsType,@MaterialToolType,@Unit,@ConvertUnit,@ConvertRate,@PurchasePrice,@SalePrice,@SalePrice2,@SalePrice3,@FixedSalePrice,@SalePriceAfterTax,@SalePriceAfterTax2,@SalePriceAfterTax3,@IsSalePriceAfterTax,@RepositoryID,@ReponsitoryAccount,@ExpenseAccount,@RevenueAccount,@MinimumStock,@AccountingObjectID,@TaxRate,@SystemMaterialGoodsType,@SaleDescription,@PurchaseDescription,@ItemSource,@MaterialGoodsGSTID,@SaleDiscountRate,@PurchaseDiscountRate,@IsSaleDiscountPolicy,@GuarantyPeriod,@CostMethod,@IsActive,@IsSecurity,@PrintMetarial,@LastPurchasePriceAfterTax,@WarrantyTime,@Quantity,@UnitPrice,@Amount,@AllocationTimes,@AllocatedAmount,@AllocationAccount,@CostSetID,@AllocationType,@AllocationAwaitAccount,@CareerGroupID)";
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

        [HttpPost]
        [Route("DeleteById/{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {

            string deleteQuery = @"Delete  [dbo].[MaterialGoods] where [Id]='" + id + "'" + "";
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
            string selectMaterialGoods = @"SELECT * FROM [MaterialGoods] A
inner join MaterialGoodsCategory B ON A.MaterialGoodsCategoryID=B.ID
Inner join Repository C On A.RepositoryID=C.ID";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    DataTable table = new DataTable();
                    table.Load(await con.ExecuteReaderAsync(selectMaterialGoods));


                    using (var workbook = new XLWorkbook())
                    {
                        
                        var worksheet = workbook.Worksheets.Add("CategoryList");
                        var currentRow = 1;
                        worksheet.Cell(currentRow, 1).Value = "Ma Hang";
                        worksheet.Cell(currentRow, 2).Value = "Ten Hang";
                        worksheet.Cell(currentRow, 3).Value = "Tinh chat";
                        worksheet.Cell(currentRow, 4).Value = "Loi";
                        worksheet.Cell(currentRow, 5).Value = "Don vi tinh";
                        worksheet.Cell(currentRow, 6).Value = "Thoi han BH";
                        worksheet.Cell(currentRow, 7).Value = "So luong ton toi thieu";
                        worksheet.Cell(currentRow, 8).Value = "Don gia mua";
                        worksheet.Cell(currentRow, 9).Value = "Don gia ban";
                        worksheet.Cell(currentRow, 10).Value = "Kho ngam dinh";
                        worksheet.Cell(currentRow, 11).Value = "hue Suat(%)";
                        worksheet.Cell(currentRow, 12).Value = "Tk chi phi";
                        worksheet.Cell(currentRow, 13).Value = "Tai khoan kho";
                        worksheet.Cell(currentRow, 14).Value = "Ty le CKMH(%)";
                        worksheet.Cell(currentRow, 15).Value = "Tk doanh thu";
                        worksheet.Cell(currentRow, 16).Value = "Ty le CKBH(%)";
                        worksheet.Cell(currentRow, 17).Value = "Nguan goc";

                        worksheet.Row(currentRow).Cells(1, 17).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 17).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 17).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 17).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                        worksheet.Row(1).Cells(1, 17).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                        worksheet.Row(1).Cells(1, 17).Style.Font.Bold=true;

                        foreach (DataRow row in table.Rows)
                        {
                            currentRow++;
                            /*
                         ID	MaterialGoodsCategoryID			MaterialGoodsType	MaterialToolType		ConvertUnit	ConvertRate			SalePrice2	SalePrice3	FixedSalePrice	SalePriceAfterTax	SalePriceAfterTax2	SalePriceAfterTax3	IsSalePriceAfterTax	RepositoryID					AccountingObjectID		SystemMaterialGoodsType	SaleDescription	PurchaseDescription		MaterialGoodsGSTID			IsSaleDiscountPolicy	GuarantyPeriod	CostMethod	IsActive	IsSecurity	PrintMetarial	LastPurchasePriceAfterTax		Quantity	UnitPrice	Amount	AllocationTimes	AllocatedAmount	AllocationAccount	CostSetID	AllocationType	AllocationAwaitAccount	CareerGroupID 	ID		MaterialGoodsCategoryName	ParentID	IsParentNode	OrderFixCode	Grade	IsTool	IsActive	IsSecurity	ID	BranchID		RepositoryName	Description	DefaultAccount	IsActive
                         */
                            worksheet.Cell(currentRow, 1).Value = row["MaterialGoodsCode"].ToString();
                            worksheet.Cell(currentRow, 2).Value = row["MaterialGoodsName"].ToString();
                            worksheet.Cell(currentRow, 3).Value = "Custome Code";
                            worksheet.Cell(currentRow, 4).Value = row["MaterialGoodsCategoryCode"].ToString();
                            worksheet.Cell(currentRow, 5).Value = row["Unit"].ToString();
                            worksheet.Cell(currentRow, 6).Value = row["WarrantyTime"].ToString();
                            worksheet.Cell(currentRow, 7).Value = row["MinimumStock"].ToString();
                            worksheet.Cell(currentRow, 8).Value = row["PurchasePrice"].ToString();
                            worksheet.Cell(currentRow, 9).Value = row["SalePrice"].ToString();
                            worksheet.Cell(currentRow, 10).Value =row["RepositoryCode"].ToString();
                            worksheet.Cell(currentRow, 11).Value =row["TaxRate"].ToString();
                            worksheet.Cell(currentRow, 12).Value =row["ExpenseAccount"].ToString();
                            worksheet.Cell(currentRow, 13).Value =row["ReponsitoryAccount"].ToString();
                            worksheet.Cell(currentRow, 14).Value =row["PurchaseDiscountRate"].ToString();
                            worksheet.Cell(currentRow, 15).Value =row["RevenueAccount"].ToString();
                            worksheet.Cell(currentRow, 16).Value =row["SaleDiscountRate"].ToString(); 
                            worksheet.Cell(currentRow, 17).Value =row["ItemSource"].ToString();

                            worksheet.Row(currentRow).Cells(1, 17).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1, 17).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1, 17).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1, 17).Style.Border.LeftBorder = XLBorderStyleValues.Thin;


                        }
                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();

                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "CategoryList.xlsx");
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
        [Route("GetAllMaterialGoodsCategory")]
        public async Task<IActionResult> GetAllMaterialGoodsCategory()
        {
            string SelectedAllDataQuery = @"SELECT ID as Id, MaterialGoodsCategoryCode as Text FROM [MaterialGoodsCategory] ORDER BY MaterialGoodsCategoryCode";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var dataList = await con.QueryAsync<DropDownViewModel>(SelectedAllDataQuery);

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


        [HttpGet]
        [Route("GetAllRepository")]
        public async Task<IActionResult> GetAllRepository()
        {
            string SelectedAllDataQuery = @"SELECT ID as Id, RepositoryCode as Text FROM [Repository] ORDER BY RepositoryCode";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var dataList = await con.QueryAsync<DropDownViewModel>(SelectedAllDataQuery);

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
