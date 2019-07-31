using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace rpm_joinery.Controllers
{
    public class ProjectController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateProject()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateNewProject()
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteProject()
        {
            throw new NotImplementedException();
        }
    }
}
