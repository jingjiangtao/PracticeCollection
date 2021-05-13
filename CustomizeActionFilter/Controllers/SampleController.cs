using CustomizeActionFilter.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomizeActionFilter.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [VersionCheck]
    public class SampleController : ControllerBase
    {
        [HttpGet("NeedVersionFilter")]
        public IActionResult NeedVersionFilter()
        {
            return Ok("OK: Need Version");
        }

        [IgnoreVersionCheck]
        [HttpGet("NoNeedVersionFilter")]
        public IActionResult NoNeedVersionFilter()
        {
            return Ok("OK: No Need Version");
        }
    }
}
