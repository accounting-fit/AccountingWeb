using AccountingWeb.GlobalElemnts;
using AccountingWeb.Models.CommonModels;
using AccountingWeb.Models.ViewModels;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingWeb.ApiController
{
    [Route("[controller]")]
    [ApiController]
    public class ApiAccountingObjectBankAccountController : ControllerBase
    {

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
    }
}
