﻿using System;
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
        public IActionResult Tag([FromQuery] string tag)
        {
            var projects = _context.Projects
                .Include(i => i.Images)
                .Include(i => i.Tags)
                .Where(i=>i.Tags.Any(j=>j.Tag.Name == tag))
                .ToList();
            if (projects == null) return NotFound();

            ViewBag.Tag = tag;
            return View(projects);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var project = _context.Projects.Where(i => i.Id == id)
                .Include(i => i.Images)
                .Include(i => i.Tags)
                .FirstOrDefault();

            if (project == null) return NotFound();

            var tags = _context.Tags.ToList();
            Random rnd = new Random();
            foreach (var tag in tags)
            {
                tag.FontSize = rnd.Next(12, 30);
            }

            var viewModel = new ProjectDetailsViewModel
            {
                ProjectId = project.Id,
                Project = project,
                BrowseTags = tags,
            }; // todo: add previous and next

            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateProjectViewModel();
            viewModel.TagSelectListItems = _context.Tags.ToList();
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(CreateProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newProject = new Project();

                newProject.Title = model.Title;
                newProject.Description = model.Description;
                newProject.ServicesProvided = model.ServicesProvided;
                newProject.Tags = new List<ProjectTag>();
                newProject.Images = new List<ProjectImage>();
                newProject.CreatedOn = DateTime.Today;

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
                return View("Index");
            }
            model.TagSelectListItems = _context.Tags.ToList();
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            var project = _context.Projects.Where(i => i.Id == id).FirstOrDefault();
            if (project == null) return NotFound();

            var viewModel = new EditProjectViewModel();
            viewModel.ProjectId = id;
            viewModel.Project = project;
            viewModel.TagSelectListItems = _context.Tags.ToList();
            viewModel.Title = project.Title;
            viewModel.Description = project.Description;
            viewModel.ServicesProvided = project.ServicesProvided;
            viewModel.Tags = new List<string>();
            foreach(var tag in project.Tags)
            {
                viewModel.Tags.Add(tag.Tag.Name);
            }

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(EditProjectViewModel viewModel)
        {
            var project = _context.Projects.Where(i => i.Id == viewModel.ProjectId).FirstOrDefault();
            if (project == null) return NotFound();

            if (ModelState.IsValid)
            {
                project.Title = viewModel.Title;
                project.Description = viewModel.Description;
                project.ServicesProvided = viewModel.ServicesProvided;
                project.Tags = new List<ProjectTag>();
                project.Images = new List<ProjectImage>();
                project.CreatedOn = DateTime.Today;


                // Deal with Assigned Tags
                if (viewModel.Tags != null && viewModel.Tags.Count > 0)
                {
                    var existingTags = _context.Tags.ToList();
                    foreach (string tag in viewModel.Tags)
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
                        projectTag.ProjectId = project.Id;
                        projectTag.Project = project;
                        project.Tags.Add(projectTag);
                    }
                }
                else
                {
                    //todo return error select 0 or more tags
                }

                // Deal with image upload
                if (viewModel.Images != null && viewModel.Images.Count > 0)
                {
                    foreach (IFormFile image in viewModel.Images)
                    {
                        if(!project.Images.Where(i=>i.ImageFilePath.Contains(image.FileName)).Any())
                        {
                            string uploadsFolder = Path.Combine(_honestingEnvironment.WebRootPath, "images");
                            string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                            image.CopyTo(new FileStream(filePath, FileMode.Create));

                            var projectImage = new ProjectImage();
                            projectImage.ProjectId = project.Id;
                            projectImage.ImageFilePath = "/images/" + uniqueFileName;
                            project.Images.Add(projectImage);
                        }
                    }
                }
                else
                {
                    //todo return error select at least 1 picture
                }

                _context.SaveChanges();
                return View("Index");
            }
            viewModel.TagSelectListItems = _context.Tags.ToList();
            return View(viewModel);
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteProjectImage(int id)
        {
            var projectImage = _context.ProjectImages.Where(i => i.Id == id).FirstOrDefault();
            if (projectImage == null) return NotFound();

            string fileLocation = Path.Combine(_honestingEnvironment.WebRootPath, projectImage.ImageFilePath);
            System.IO.File.Delete(fileLocation);

            _context.ProjectImages.Remove(projectImage);
            _context.SaveChanges();

            return Json(new { deleted = true });
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteProject(int id)
        {
            var project = _context.Projects.Where(i => i.Id == id).FirstOrDefault();
            if (project == null) return NotFound();

            _context.Projects.Remove(project);
            _context.SaveChanges();

            ViewBag.Notification = "Project Deleted.";
            return View("Index");
        }
    }
}
