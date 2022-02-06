using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using rpm_joinery.Data;

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
        }

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
            return View();
        }
    }
}
