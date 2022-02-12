using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using rpm_joinery.Data;
using rpm_joinery.Models.Services;
using System.Collections.Generic;

namespace rpm_joinery.Controllers
{
    public class ServicesController : Controller
    {
        private ApplicationDbContext _context { get; set; }
        private readonly IHostingEnvironment _honestingEnvironment;

        public ServicesController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _honestingEnvironment = env;

            _services = new Dictionary<string, Service>
            {
                { "Commercial", new Service { Name = "Commerical", Description = "We provide a range of commercial joinery services including:• Shopfitting • Reactive Maintenance • Bathrooms & wet walls • Offices fitting and refurbishment • Commercial flooring • Hotels, • restaraunts and bars • Commercial extensions • new builds", Icon = "", Image = ""  } },
                { "Domestic", new Service { Name = "Domestic", Description = "TBC", Icon = "", Image = ""  } },
                { "Industrial", new Service { Name = "Industrial", Description = "TBC", Icon = "", Image = ""  } },
                { "Kitchens", new Service { Name = "Kitchens", Description = "We can design and expertly fit your dream kitchen and flooring to your exact requirement and taste. We pride ourselves in our expert service and can provide a variety of styles for all budgets. Take a look at some of the kitchens in our portfolio for some inspiration or contact us with an enquiry for a free quote.", Icon = "", Image = ""  } },
                { "Bathrooms", new Service { Name = "Bathrooms", Description = "Whether your bathroom needs revamping, a simple suite installation or a complete reconstruction we can cater to your needs. We cover all jobs needed to bring your dream bathroom to life. Why not have a look at some of the bathrooms and wet walls in our portfolio.", Icon = "", Image = ""  } },
                { "Full Property Refurbishment", new Service { Name = "Full Property Refurbishment", Description = "Whether you are a letting agent or a homeowner we can also provide complete renovation services that can transform a property to fufill its full potential. We recently completed a series of West End flat renovations in Dundee that stand as a testament of we can do for your property. We are happy to take on renovation projects of all sizes, get in touch today.", Icon = "", Image = ""  } },
                { "Windows & Doors", new Service { Name = "Windows & Doors", Description = "At RPM we can supply and install replacement windows and doors, in homes, offices and all other typical scenarios. All our windows and doors are expertly fitted down to the finest detail. Feel free to browse some of the windows and doors in our portfolio that we have completed for our happy customers.", Icon = "", Image = ""  } },
            };

            _serviceNames = new List<string> { "Commerical", "Domestic", "Industrial", "Kitchens", "Bathrooms", "Full Property Reburbishment", "Windows & Doors" };
        }

        private Dictionary<string, Service> _services { get; set; }
        private static List<string> _serviceNames { get; set; }

        [HttpGet]
        public IActionResult Index()
        {
            //Set SEO keywords, description, and other meta data in view bag so can be changed dynamically in layout
            ViewBag.Title = "Services | RPM Joinery and Maintenance";
            ViewBag.MetaDescription = "RPM Joinery and Maintenance Previous Work and Projects";
            ViewBag.MetaKeywords = "RPM joinery maintenance, dundee joiner, rpm projects, rpm joinery, broughty ferry joiner, rpm dundee, kitchen fitting dundee, decking dundee, reliable joiner dundee, flat renovation dundee, bathroom fitting dundee, windows, doors, property management";

            return View();
        }

        [HttpGet]
        public IActionResult Tag([FromQuery] string tag)
        {
            ViewBag.Title = $"{tag} | RPM Joinery and Maintenance";
            ViewBag.MetaDescription = "RPM Joinery and Maintenance is a family run business that take great pride in providing the best possible service to meet all of our consumer's needs.";
            ViewBag.MetaKeywords = $"RPM joinery maintenance, {tag}, trusted joiner dundee, quality cheap joiner dundee angus, about rpm dundee, trusted trader dundee joiner, trusted joiner angus, family run joiner dundee";

            return View();
        }
    }
}
