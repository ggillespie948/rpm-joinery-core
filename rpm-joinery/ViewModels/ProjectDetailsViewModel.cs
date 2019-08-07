using rpm_joinery.Models;
using rpm_joinery.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rpm_joinery.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public List<Tag> BrowseTags { get; set; }

        public int NextProjectId { get; set; }
        public string NextProjectTile { get; set; }

        public int PreviousProjectId { get; set; }
        public string PreviousProjectTile { get; set; }
    }
}
