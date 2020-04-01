using Microsoft.AspNetCore.Mvc;
using System;

namespace LibraryApi.Controllers
{
    public class TimeController : Controller
    {
        [HttpGet("/time")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 10)]
        public ActionResult GetTime()
        {
            var response = new { time = DateTime.Now };
            return Ok(response);
        }
    }
}
