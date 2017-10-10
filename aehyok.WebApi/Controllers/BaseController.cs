using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace aehyok.WebApi.Controllers
{
    /// <summary>
    /// Web Api »ù´¡¿ØÖÆÆ÷
    /// </summary>
    [AuthorizeBearer]
    public class BaseController : Controller
    {
    }
}