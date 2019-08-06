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

        [Route("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("about")]
        public IActionResult About()
        {
            //Set SEO keywords, description, and other meta data in view bag so can be changed dynamically in layout
            ViewBag.Title = "About | RPM Joinery and Maintenance";
            ViewBag.MetaDescription = "RPM Joinery and Maintenance is a family run business that take great pride in providing the best possible service to meet all of our consumer's needs.";
            ViewBag.MetaKeywords = "RPM joinery maintenance, reliable joiner, trusted joiner dundee, quality cheap joiner dundee angus, about rpm dundee, trusted trader dundee joiner, trusted joiner angus, family run joiner dundee";
            return View();
        }

        [Route("contact")]
        public IActionResult Contact()
        {
            ViewBag.Title = "Contact Us | RPM Joinery and Maintenance";
            ViewBag.MetaDescription = "Contact RPM Joinery and Maintenance directly. We are happy to discuss the details of any potential work you may need conducted.";
            ViewBag.MetaKeywords = "RPM Joinery, RPM joienry contact, contact a joiner in dundee, emergency joiner dundee, contact joiner angus";
            return View();
        }

        [Route("services")]
        public IActionResult Services()
        {
            ViewBag.Title = "Services | RPM Joinery and Maintenance";
            ViewBag.MetaDescription = "RPM Joinery and Maintenance offer a wide range of services ranging from domestic to commercial.We offer services in fitting kitchens, bathrooms and more.";
            ViewBag.MetaKeywords = "RPM services, kitchen dundee, bathrooms dundee, maintenance dundee, decking, joinery services, joinery carpenter dundee angus perth";
            return View();
        }

        [Route("Cookies")]
        public IActionResult Cookies()
        {
            ViewBag.Title = "Cookie Policy | RPM Joinery and Maintenance";
            ViewBag.MetaDescription = "RPM Joinery and Maintenance Cookie policy";
            ViewBag.MetaKeywords = "Cookie policy rpm";
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
