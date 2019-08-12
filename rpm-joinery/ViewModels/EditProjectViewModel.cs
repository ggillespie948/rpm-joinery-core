using Microsoft.AspNetCore.Http;
using rpm_joinery.Models;
using rpm_joinery.Models.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rpm_joinery.ViewModels
{
    public class EditProjectViewModel
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public List<Tag> TagSelectListItems { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Services Provided")]
        public string ServicesProvided { get; set; }
        [Required]
        public List<string> Tags { get; set; }

        public List<IFormFile> Images { get; set; }

        public List<string> ImageCaptions { get; set; }
    }
}
