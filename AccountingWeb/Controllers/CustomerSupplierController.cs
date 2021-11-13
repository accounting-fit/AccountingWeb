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
    public class CustomerSupplierController : Controller
    {
        private readonly ILogger<CustomerSupplierController> _logger;

        public CustomerSupplierController(ILogger<CustomerSupplierController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Action"] = "Customer & Supplier List";
            return View();
        }
        public IActionResult Create()
        {
            ViewData["Action"] = "Create Customer & Supplier";
            return View();
        }

        public IActionResult Edit(string id)
        {
            ViewData["Action"] = "Edit Customer & Supplier";
            ViewBag.Id = id;
            return View();
        }

        public IActionResult Delete(string id)
        {
            ViewData["Action"] = "Delete Customer & Supplier";
            ViewBag.Id = id;
            return View();
        }
    }
}
