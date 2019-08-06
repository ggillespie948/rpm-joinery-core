using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using rpm_joinery.Data;
using rpm_joinery.Models.Projects;
using rpm_joinery.ViewModels;
using Microsoft.AspNetCore.Hosting.Internal;
using rpm_joinery.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace rpm_joinery.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext _context { get; set; }
        private readonly IHostingEnvironment _honestingEnvironment;

        public ProjectsController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _honestingEnvironment = env;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var projects = _context.Projects
                .Include(i => i.Images)
                .Include(i => i.Tags)
                .Where(i => i.Images.Count > 0)
                .ToList();
            return View(projects);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var project = _context.Projects.Where(i => i.Id == id).FirstOrDefault();
            if (project == null) return NotFound();
            return View(project);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateProjectViewModel();
            viewModel.TagSelectListItems = new List<Tag>();
            viewModel.TagSelectListItems.Add(new Tag { Name = "Dundee" });
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public JsonResult CreateNewProject(CreateProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newProject = new Project();

                newProject.Title = model.Title;
                newProject.Description = model.Description;
                newProject.ServicesProvided = model.ServicesProvided;
                newProject.Tags = new List<ProjectTag>();
                newProject.Images = new List<ProjectImage>();

                _context.Projects.Add(newProject);
                _context.SaveChanges();

                // Deal with Assigned Tags
                if (model.Tags != null && model.Tags.Count >= 3)
                {
                    var existingTags = _context.Tags.ToList();
                    foreach (string tag in model.Tags)
                    {
                        if (!existingTags.Where(i => i.Name.Contains(tag)).Any())
                        {
                            var newTag = new Tag { Name = tag };
                            _context.Tags.Add(newTag);
                            existingTags.Add(newTag);
                        }

                        var projectTag = new ProjectTag();
                        projectTag.TagId = existingTags.Where(i => i.Name.Contains(tag)).FirstOrDefault().Id;
                        projectTag.Tag = existingTags.Where(i => i.Name.Contains(tag)).FirstOrDefault();
                        projectTag.ProjectId = newProject.Id;
                        projectTag.Project = newProject;
                        newProject.Tags.Add(projectTag);
                    }
                }
                else
                {
                    //todo return error select 3 or more tags
                }

                // Deal with image upload
                if (model.Images != null && model.Images.Count > 0)
                {
                    foreach (IFormFile image in model.Images)
                    {
                        string uploadsFolder = Path.Combine(_honestingEnvironment.WebRootPath, "images");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        image.CopyTo(new FileStream(filePath, FileMode.Create));

                        var projectImage = new ProjectImage();
                        projectImage.ProjectId = newProject.Id;
                        projectImage.ImageFilePath = "/images/" + uniqueFileName;
                        newProject.Images.Add(projectImage);
                    }
                }
                else
                {
                    //todo return error select at least 1 picture
                }

                _context.SaveChanges();
                return Json(new { created = true, message = "Project Added Succesfully" });
            }

            return Json(new { created = false, message = "Please ensure you have filled out every form value" });
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteProject()
        {
            throw new NotImplementedException();
        }
    }
}
