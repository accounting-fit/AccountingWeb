using AccountingWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingWeb.Controllers
{
    public class AccountingObjectBankAccountController : Controller
    {
        private readonly ILogger<AccountingObjectBankAccountController> _logger;

        public AccountingObjectBankAccountController(ILogger<AccountingObjectBankAccountController> logger)
        {
            _logger = logger;
        }  
        public IActionResult Create(string id)
        {
            ViewData["Action"] = "Add Bank Account";
            ViewBag.AccountingObjectId = id;
            return View();
        }

      
    }
}
