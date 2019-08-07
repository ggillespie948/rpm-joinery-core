using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rpm_joinery.Models.Projects
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int FontSize { get; set; }

    }
}
