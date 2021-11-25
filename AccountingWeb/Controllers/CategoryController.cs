using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Action"] = "Category List";
            return View("~/Views/Main/Category/Index.cshtml");
        }
        public IActionResult Create()
        {
            ViewData["Action"] = "Create Category";
            return View("~/Views/Main/Category/Create.cshtml");
        }

        public IActionResult Edit(string id)
        {
            ViewData["Action"] = "Edit Category";
            ViewBag.Id = id;
            return View("~/Views/Main/Category/Edit.cshtml");
        }

        public IActionResult Delete(string id)
        {
            ViewData["Action"] = "Delete Category";
            ViewBag.Id = id;
            return View("~/Views/Main/Category/Delete.cshtml");
        }

    }
}
