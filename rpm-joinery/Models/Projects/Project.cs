using rpm_joinery.Models.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rpm_joinery.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public Guid CreatedByUserId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string ServicesProvided { get; set; }

        public List<ProjectTag> Tags { get; set; }
        public List<ProjectImage> Images {get; set;}
        
    }
}
