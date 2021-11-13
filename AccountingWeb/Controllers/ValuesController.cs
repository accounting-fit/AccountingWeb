using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [Route("GetApi")]
        public async Task<IActionResult> GetApi()
        {
            return Ok(new { ok = true, msg = "Hello" });

        }
            [HttpPost]
            [Route("PostApi")]
            public async Task<IActionResult> PostApi(string a)
            {
                return Ok(new { ok = true, msg = a });
            }
        }
}
