using Microsoft.AspNetCore.Http;
using rpm_joinery.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rpm_joinery.ViewModels
{
    public class CreateProjectViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ServicesProvided { get; set; }
        public List<Tag> TagSelectListItems { get; set; }
        public List<string> Tags { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
