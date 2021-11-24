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
        

        public IActionResult Index(string id)
        {
            ViewData["Action"] = "Bank Account List";
            ViewBag.AccountingObjectID = id;
            TempData["AccountingObjectID"] = id;
            return View();
        }
        public IActionResult Create(string id)
        {
            ViewData["Action"] = "Add Bank Account";
            ViewBag.AccountingObjectID = id;
            return View();
        }

        public IActionResult Edit(string id)
        {
            ViewData["Action"] = "Edit Bank Account";
            ViewBag.Id = id;
            return View();
        }

        public IActionResult Delete(string id)
        {
            ViewData["Action"] = "Delete Bank Account";
            ViewBag.Id = id;
            ViewBag.AccountObjectID = TempData["AccountingObjectID"];
            return View();
        }


    }
}
