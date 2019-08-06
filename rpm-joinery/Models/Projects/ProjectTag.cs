using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rpm_joinery.Models.Projects
{
    public class ProjectTag
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
