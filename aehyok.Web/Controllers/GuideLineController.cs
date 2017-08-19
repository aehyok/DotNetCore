using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.Web.Controllers
{
    public class GuideLineController : Controller
    {
        /// <summary>
        /// 指标定义页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Define()
        {
            return View();
        }
    }
}