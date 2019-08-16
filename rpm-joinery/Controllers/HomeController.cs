using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
using rpm_joinery.Data;
using rpm_joinery.Models;
using rpm_joinery.ViewModels;

namespace rpm_joinery.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context { get; set; }
        private readonly IHostingEnvironment _honestingEnvironment;

        public HomeController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _honestingEnvironment = env;
        }

        public IActionResult Index()
        {
            //Set SEO keywords, description, and other meta data in view bag so can be changed dynamically in layout
            ViewBag.Title = "RPM Joinery and Maintenance | Dundee Joinery Services";
            ViewBag.MetaDescription = "RPM Joinery and Maintenance provide top quality joinery and maintenance services to Dundee, Angus and Fife, all at a great price.";
            ViewBag.MetaKeywords = "RPM joinery maintenance, dundee joiner, rpm dundee, rpm joinery, broughty ferry joiner, rpm dundee, kitchen fitting dundee, decking dundee, reliable joiner dundee, flat renovation dundee, bathroom fitting dundee, windows, doors, property management";


            var projects = _context.Projects
                .Include(i => i.Images)
                .Include(i => i.Tags)
                .Where(i => i.Images.Count > 0)
                .Take(6)
                .ToList();
            return View(projects);
        }

        [Route("privacy")]
        public IActionResult Privacy()
        {
            //Set SEO keywords, description, and other meta data in view bag so can be changed dynamically in layout
            ViewBag.Title = "Privacy Policy | RPM Joinery and Maintenance";
            ViewBag.MetaDescription = "RPM Joinery and Maintenance is a family run business that take great pride in providing the best possible service to meet all of our consumer's needs.";
            ViewBag.MetaKeywords = "RPM joinery maintenance, reliable joiner, trusted joiner dundee, quality cheap joiner dundee angus, about rpm dundee, trusted trader dundee joiner, trusted joiner angus, family run joiner dundee";
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
            var viewModel = new ContactFormViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SendMessage(ContactFormViewModel viewModel)
        { 
            try
            {
                //instantiate a new MimeMessage
                var message = new MimeMessage();
                //Setting the To e-mail address
                message.To.Add(new MailboxAddress("Ross Murray", "rpmdundee@gmail.com"));
                //Setting the From e-mail address
                message.From.Add(new MailboxAddress(viewModel.FullName, viewModel.Email));
                //E-mail subject 
                message.Subject = "New email from RPM Joinery Contact form";
                //E-mail message body
                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = viewModel.Message + " Message was sent by: " + viewModel.FullName + " E-mail: " + viewModel.Email + " Phone: " + viewModel.PhoneNumber
                };

                //Configure the e-mail
                using (var emailClient = new SmtpClient())
                {
                    emailClient.Connect("smtp.gmail.com", 587, false);
                    emailClient.Authenticate("rpmjoinerymailclient@gmail.com", "Rpmjoinery");
                    emailClient.Send(message);
                    emailClient.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                ViewBag.Message = $" Oops! We have a problem here {ex.Message}";
            }

            return RedirectToAction("Contact");

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

        [Authorize]
        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthCookie");
            var projects = _context.Projects
                .Include(i => i.Images)
                .Include(i => i.Tags)
                .Where(i => i.Images.Count > 0)
                .Take(6)
                .ToList();
            return RedirectToAction("Index",projects);
        }
    }
}
