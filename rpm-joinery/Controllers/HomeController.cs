using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rpm_joinery.Models;

namespace rpm_joinery.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //Set SEO keywords, description, and other meta data in view bag so can be changed dynamically in layout
            ViewBag.Title = "RPM Joinery and      Maintenance | Dundee Joinery Services";
            ViewBag.MetaDescription = "RPM Joinery and Maintenance provide top quality joinery and maintenance services to Dundee, Angus and Fife, all at a great price.";
            ViewBag.MetaKeywords = "RPM joinery maintenance, dundee joiner, dundee joinery, lochee joiner, broughty ferry joiner, rpm dundee, kitchen fitting dundee, decking dundee, reliable joiner dundee, flat renovation dundee, bathroom fitting dundee, windows, doors, property management";


            // Retrieve the 3 most recent projects from the data base and display in the preview of the index home page 
            List<Project> projects = new List<Project>();
            projects.Reverse();
            List<Project> recentProjects = new List<Project>();
            int projectCounter = 0;         

            foreach (Project proj in projects)
            {
                recentProjects.Add(proj);
                projectCounter++;

                if (projectCounter > 2)
                    break;
            }

            return View(recentProjects);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Cookies()
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
