using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rpm_joinery.Models.Projects
{
    public class ProjectImage
    {
        [Key]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string ImageFilePath { get; set; }
        public string ImageDescription { get; set; }
        public bool IsPrimaryImage { get; set; }
    }
}
