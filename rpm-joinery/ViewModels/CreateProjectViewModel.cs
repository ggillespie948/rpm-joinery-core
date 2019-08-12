using Microsoft.AspNetCore.Http;
using rpm_joinery.Models.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rpm_joinery.ViewModels
{
    public class CreateProjectViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name="Services Provided")]
        public string ServicesProvided { get; set; }
        public List<Tag> TagSelectListItems { get; set; }

        [Required]
        public List<string> Tags { get; set; }

        [Required]
        public List<IFormFile> Images { get; set; }

        [Required]
        public List<string> ImageCaptions { get; set; }
    }
}
