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
    public class RepositoryController : Controller
    {
        private readonly ILogger<RepositoryController> _logger;

        public RepositoryController(ILogger<RepositoryController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Action"] = "Repository List";
            return View("~/Views/Main/Repository/Index.cshtml");
        }
        public IActionResult Create()
        {
            ViewData["Action"] = "Create Repository";
            return View("~/Views/Main/Repository/Create.cshtml");
        }

        public IActionResult Edit(string id)
        {
            ViewData["Action"] = "Edit Repository";
            ViewBag.Id = id;
            return View("~/Views/Main/Repository/Edit.cshtml");
        }

        public IActionResult Delete(string id)
        {
            ViewData["Action"] = "Delete Repository";
            ViewBag.Id = id;
            return View("~/Views/Main/Repository/Delete.cshtml");
        }
    }
}
