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
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            string selectMaterialGoods = @"SELECT * FROM [MaterialGoods] ORDER BY ID Desc";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var categoryEntityModelData = await con.QueryAsync<CategoryEntityModel>(selectMaterialGoods);    
                    
                    foreach(var item in categoryEntityModelData)
                    {
                        item.materialGoodsTypeName = GetMaterialGoodsTypeName(item.MaterialGoodsType);
                    }

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
                MaterialGoodsType =model.materialGoodsType,
                Unit = model.unit,
                PurchasePrice = string.IsNullOrEmpty(model.purchasePrice) ? 0 : decimal.Parse(model.purchasePrice),
                SalePrice = string.IsNullOrEmpty(model.salesPrice) ? 0 : decimal.Parse(model.salesPrice),
                RepositoryID = model.repositoryId,
                ReponsitoryAccount = model.repositoryAccountId,
                ExpenseAccount = model.expanceAccountId,
                RevenueAccount = model.revenueAccountId,
                MinimumStock = string.IsNullOrEmpty(model.minimumStock) ? 0 : decimal.Parse(model.minimumStock),
                TaxRate = string.IsNullOrEmpty(model.taxRate) ? 0 : decimal.Parse(model.taxRate),
                ItemSource = model.itemSource,
                SaleDiscountRate = string.IsNullOrEmpty(model.salesDiscountRate) ? 0 : decimal.Parse(model.salesDiscountRate),
                PurchaseDiscountRate = string.IsNullOrEmpty(model.purchaseDiscountRate) ? 0 : decimal.Parse(model.purchaseDiscountRate),
                IsSaleDiscountPolicy = model.isSalesDiscountPolicy,
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

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            string selectedData = @"select * from [MaterialGoods] where id='" + id + "'" + "";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var dataList = await con.QueryAsync<CategoryEntityModel>(selectedData);
                    var singleData = dataList.FirstOrDefault();

                    CategoryViewModel entity = new CategoryViewModel()
                    {
                        id = singleData.ID,
                        matGoodsCode = singleData.MaterialGoodsCode,
                        matGoodsName = singleData.MaterialGoodsName,
                        matGoodsCatId = singleData.MaterialGoodsCategoryID.ToString(),
                        unit = singleData.Unit,
                        warrantyTime = singleData.WarrantyTime,
                        minimumStock = singleData.MinimumStock.ToString(),
                        purchasePrice = singleData.PurchasePrice.ToString(),
                        salesPrice = singleData.SalePrice.ToString(),
                        repositoryId = singleData.RepositoryID,
                        taxRate = singleData.TaxRate.ToString(),
                        expanceAccountId = singleData.ExpenseAccount,
                        repositoryAccountId = singleData.ReponsitoryAccount,
                        purchaseDiscountRate = singleData.PurchaseDiscountRate.ToString(),
                        revenueAccountId = singleData.RevenueAccount,
                        salesDiscountRate = singleData.SaleDiscountRate.ToString(),
                        isSalesDiscountPolicy = singleData.IsSaleDiscountPolicy,
                        itemSource = singleData.ItemSource,
                        isActive = singleData.IsActive,
                        materialGoodsType=singleData.MaterialGoodsType
                    };
                    return Ok(new { ok = false, SingleData = entity });
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
        public async Task<IActionResult> Update(CategoryViewModel model)
        {
            CategoryEntityModel entity = new CategoryEntityModel()
            {
                ID=model.id,
                MaterialGoodsCode = model.matGoodsCode,
                MaterialGoodsCategoryID = string.IsNullOrEmpty(model.matGoodsCatId) ? Guid.Empty : Guid.Parse(model.matGoodsCatId),
                MaterialGoodsName = model.matGoodsName,
                MaterialGoodsType=model.materialGoodsType,
                Unit = model.unit,
                PurchasePrice = string.IsNullOrEmpty(model.purchasePrice) ? 0 : decimal.Parse(model.purchasePrice),
                SalePrice = string.IsNullOrEmpty(model.salesPrice) ? 0 : decimal.Parse(model.salesPrice),
                RepositoryID = model.repositoryId,
                ReponsitoryAccount = model.repositoryAccountId,
                ExpenseAccount = model.expanceAccountId,
                RevenueAccount = model.revenueAccountId,
                MinimumStock = string.IsNullOrEmpty(model.minimumStock) ? 0 : decimal.Parse(model.minimumStock),
                TaxRate = string.IsNullOrEmpty(model.taxRate) ? 0 : decimal.Parse(model.taxRate),
                ItemSource = model.itemSource,
                SaleDiscountRate = string.IsNullOrEmpty(model.salesDiscountRate) ? 0 : decimal.Parse(model.salesDiscountRate),
                PurchaseDiscountRate = string.IsNullOrEmpty(model.purchaseDiscountRate) ? 0 : decimal.Parse(model.purchaseDiscountRate),
                IsSaleDiscountPolicy = model.isSalesDiscountPolicy,
                IsActive = model.isActive,
                WarrantyTime = model.warrantyTime
            };

            string updateQuery = @"UPDATE  [dbo].[MaterialGoods]
                                SET [MaterialGoodsCode]=@MaterialGoodsCode,
                                    [MaterialGoodsCategoryID]=@MaterialGoodsCategoryID,
                                    [MaterialGoodsName]=@MaterialGoodsName,
                                    [MaterialGoodsType]=@MaterialGoodsType,
                                    [Unit]=@Unit,
                                    [PurchasePrice]=@PurchasePrice,
                                    [SalePrice]=@SalePrice,
                                    [RepositoryID]=@RepositoryID,
                                    [ReponsitoryAccount]=@ReponsitoryAccount,
                                    [ExpenseAccount]=@ExpenseAccount,
                                    [RevenueAccount]=@RevenueAccount,
                                    [MinimumStock]=@MinimumStock,
                                    [TaxRate]=@TaxRate,
                                    [ItemSource]=@PurchasePrice,
                                    [SaleDiscountRate]=@SaleDiscountRate,
                                    [PurchaseDiscountRate]=@PurchaseDiscountRate,
                                    [IsSaleDiscountPolicy]=@IsSaleDiscountPolicy,
                                    [IsActive]=@IsActive ,
                                    [WarrantyTime]=@WarrantyTime
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
            string selectMaterialGoods = @"SELECT A.*,B.MaterialGoodsCategoryCode,C.RepositoryCode FROM [MaterialGoods] A
Left join MaterialGoodsCategory B ON A.MaterialGoodsCategoryID=B.ID
Left join Repository C On A.RepositoryID=C.ID";
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
                        worksheet.Cell(currentRow, 1).Value = "Goods Code";
                        worksheet.Cell(currentRow, 2).Value = " Goods Name";
                        worksheet.Cell(currentRow, 3).Value = "Property";
                        worksheet.Cell(currentRow, 4).Value = "Type";
                        worksheet.Cell(currentRow, 5).Value = "Unit";
                        worksheet.Cell(currentRow, 6).Value = "Warranty Time";
                        worksheet.Cell(currentRow, 7).Value = "Minimum Stock";
                        worksheet.Cell(currentRow, 8).Value = "Purchase Price";
                        worksheet.Cell(currentRow, 9).Value = "Sale Price";
                        worksheet.Cell(currentRow, 10).Value = "Repository";
                        worksheet.Cell(currentRow, 11).Value = "Tax Rate(%)";
                        worksheet.Cell(currentRow, 12).Value = "Expance Accounting";
                        worksheet.Cell(currentRow, 13).Value = "Repository Account";
                        worksheet.Cell(currentRow, 14).Value = "Purchase discount rate(%)";
                        worksheet.Cell(currentRow, 15).Value = "Revenue Account";
                        worksheet.Cell(currentRow, 16).Value = "Sales Discount Rate(%)";
                        worksheet.Cell(currentRow, 17).Value = "Service goods with special tax";
                        worksheet.Cell(currentRow, 18).Value = "Sales Discount Policy";
                        worksheet.Cell(currentRow, 19).Value = "Item Source";
                        worksheet.Cell(currentRow, 20).Value = "IsActive";

                        worksheet.Row(currentRow).Cells(1, 20).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 20).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 20).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 20).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                        worksheet.Row(1).Cells(1, 20).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                        worksheet.Row(1).Cells(1, 20).Style.Font.Bold=true;

                        foreach (DataRow row in table.Rows)
                        {
                            currentRow++;
                            /*
                         ID	MaterialGoodsCategoryID			MaterialGoodsType	MaterialToolType		ConvertUnit	ConvertRate			SalePrice2	SalePrice3	FixedSalePrice	SalePriceAfterTax	SalePriceAfterTax2	SalePriceAfterTax3	IsSalePriceAfterTax	RepositoryID					AccountingObjectID		SystemMaterialGoodsType	SaleDescription	PurchaseDescription		MaterialGoodsGSTID			IsSaleDiscountPolicy	GuarantyPeriod	CostMethod	IsActive	IsSecurity	PrintMetarial	LastPurchasePriceAfterTax		Quantity	UnitPrice	Amount	AllocationTimes	AllocatedAmount	AllocationAccount	CostSetID	AllocationType	AllocationAwaitAccount	CareerGroupID 	ID		MaterialGoodsCategoryName	ParentID	IsParentNode	OrderFixCode	Grade	IsTool	IsActive	IsSecurity	ID	BranchID		RepositoryName	Description	DefaultAccount	IsActive
                         */
                            worksheet.Cell(currentRow, 1).Value = row["MaterialGoodsCode"].ToString();
                            worksheet.Cell(currentRow, 2).Value = row["MaterialGoodsName"].ToString();
                            worksheet.Cell(currentRow, 3).Value = GetMaterialGoodsTypeName(Convert.ToInt32(row["MaterialGoodsType"].ToString()));
                            worksheet.Cell(currentRow, 4).Value = row["MaterialGoodsCategoryCode"].ToString();
                            worksheet.Cell(currentRow, 5).Value = row["Unit"].ToString();
                            worksheet.Cell(currentRow, 6).Value = row["WarrantyTime"].ToString();
                            worksheet.Cell(currentRow, 7).Value = row["MinimumStock"].ToString();
                            worksheet.Cell(currentRow, 8).Value = row["PurchasePrice"].ToString();
                            worksheet.Cell(currentRow, 9).Value = row["SalePrice"].ToString();
                            worksheet.Cell(currentRow, 10).Value =row["RepositoryCode"].ToString();
                            worksheet.Cell(currentRow, 11).Value = GetTaxRateName(Convert.ToInt32(row["TaxRate"]).ToString());
                            worksheet.Cell(currentRow, 12).Value =row["ExpenseAccount"].ToString();
                            worksheet.Cell(currentRow, 13).Value =row["ReponsitoryAccount"].ToString();
                            worksheet.Cell(currentRow, 14).Value =row["PurchaseDiscountRate"].ToString();
                            worksheet.Cell(currentRow, 15).Value =row["RevenueAccount"].ToString();                            
                            worksheet.Cell(currentRow, 16).Value =row["SaleDiscountRate"].ToString();
                            worksheet.Cell(currentRow, 17).Value = "Service goods with special tax";
                            worksheet.Cell(currentRow, 18).Value = row["IsSaleDiscountPolicy"]?.ToString() == "True" ? 1 : 0;
                            worksheet.Cell(currentRow, 19).Value = row["ItemSource"].ToString();
                            worksheet.Cell(currentRow, 20).Value = row["IsActive"]?.ToString() == "True" ? 1 : 0;

                            worksheet.Row(currentRow).Cells(1,20).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1,20).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1,20).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1,20).Style.Border.LeftBorder = XLBorderStyleValues.Thin;


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
                    var dataList = await con.QueryAsync<DropDownGuidStringViewModel>(SelectedAllDataQuery);

                    return Ok(new { ok = false, GetAllMaterialGoodsCategory = dataList.ToList() });
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
                    var dataList = await con.QueryAsync<DropDownGuidStringViewModel>(SelectedAllDataQuery);

                    return Ok(new { ok = false, GetAllRepository = dataList.ToList() });
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



        public List<DropDownStringStringViewModel> AllMaterialGoodsTypeList()
        {
            var allMaterialGoodsTypeList = new List<DropDownStringStringViewModel>(){
                       new DropDownStringStringViewModel{ Id = "1",Text= "Goods, supplies" }
                      ,new DropDownStringStringViewModel{ Id = "2",Text= "Pieces of goods, suppliers" }
                      ,new DropDownStringStringViewModel{ Id = "3",Text= "Service" }
                      ,new DropDownStringStringViewModel{ Id = "4",Text= "Finished product" }
                      ,new DropDownStringStringViewModel{ Id = "5",Text= "Just an interpretation" }
                      ,new DropDownStringStringViewModel{ Id = "6",Text= "Other" }
                };
            return allMaterialGoodsTypeList;
        }


        public string GetMaterialGoodsTypeName(int? id)
        {
            var result = string.Empty;
            var data = AllMaterialGoodsTypeList().Where(a => a.Id == id.ToString()).FirstOrDefault();
            if (data!=null)
            {
                result = data.Text;
            }

           return result;
        }

        [HttpGet]
        [Route("GetAllMaterialGoodsTypeList")]
        public async Task<IActionResult> GetAllMaterialGoodsTypeList()
        {
            try
            {
                var dataList = AllMaterialGoodsTypeList();

                return Ok(new { ok = false, GetAllMaterialGoodsTypeList = dataList.ToList() });
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
            finally
            {

            }
        }



        public List<DropDownStringStringViewModel> AllTaxRateList()
        {
            var allMaterialGoodsTypeList = new List<DropDownStringStringViewModel>(){
                       new DropDownStringStringViewModel { Id= "0",  Text= "0%" }
                      ,new DropDownStringStringViewModel { Id= "5",  Text= "5%" }
                      ,new DropDownStringStringViewModel { Id= "10", Text="10%" }
                      ,new DropDownStringStringViewModel { Id= "-1", Text="No Tax" }
                      ,new DropDownStringStringViewModel { Id= "-2", Text="Uncalculated tax" }                     
                };
            return allMaterialGoodsTypeList;
        }


        public string GetTaxRateName(string id)
        {
            var result = string.Empty;
            var data = AllTaxRateList().Where(a => a.Id == id).FirstOrDefault();
            if (data != null)
            {
                result = data.Text;
            }

            return result;
        }

        [HttpGet]
        [Route("GetAllTaxRateList")]
        public async Task<IActionResult> GetAllTaxRateList()
        {
            try
            {
                var dataList = AllTaxRateList();

                return Ok(new { ok = false, GetAllTaxRateList = dataList.ToList() });
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
            finally
            {

            }
        }

    }
}
