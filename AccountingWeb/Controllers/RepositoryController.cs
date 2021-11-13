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
            return View();
        }
        public IActionResult Create()
        {
            ViewData["Action"] = "Create Repository";
            return View();
        }

        public IActionResult Edit(string id)
        {
            ViewData["Action"] = "Edit Repository";
            ViewBag.Id = id;
            return View();
        }

        public IActionResult Delete(string id)
        {
            ViewData["Action"] = "Delete Repository";
            ViewBag.Id = id;
            return View();
        }
    }
}
