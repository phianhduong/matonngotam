using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    public class CampsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Get()
        {
            return Ok(new { Name = "phianhduong@gmail.com", FavouriteColor = "Red" });
        }
    }
}
