using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace 第三方认证业务资源端.Controllers
{


    [Route("Home")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            return Json(new { Code = true, Message = "你成功调用了" });
        }

        [Authorize]
        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            return Json(new { Code = true, Message = "你获取了数据" });
        }
    }
}
