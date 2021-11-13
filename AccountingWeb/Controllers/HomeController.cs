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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Action"] = "Dashboard";
            return View();
        }

        public IActionResult CategoryIndex()
        {
            ViewData["Action"] = "Index";
            return View();
        }
        public IActionResult CategoryCreate()
        {
            ViewData["Action"] = "Thong tin VTHH";
            return View();
        }
        [HttpGet]
        public IActionResult Blank(int id)
        {
            ViewData["Action"] = GetFunctionName(id);
            return View();
        }
        private string GetFunctionName(int id)
        {
            switch (id)
            {
                case 1:
                    return "Dữ liệu";
                case 2:
                    return "Tiền và Ngân hàng";
                case 3:
                    return "Mua hàng";
                case 4:
                    return "Bán hàng";
                case 5:
                    return "Kho";
                case 6:
                    return "Công cụ dụng cụ";
                case 7:
                    return "Tài sản cố định";
                case 8:
                    return "Hợp đồng";
                case 9:
                    return "Tổng hợp";
                case 10:
                    return "Tiền lương";
                case 11:
                    return "Quản lý hóa đơn";
                case 12:
                    return "Giá thành";
                case 13:
                    return "Charts";
                case 14:
                    return "Report Tables";
                default:
                    return "Blank";
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
