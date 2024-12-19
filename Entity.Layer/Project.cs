using Entity.Layer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Layer
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public Team? Team { get; set; }
        public int? TeamId { get; set; }
        public List<Report>? Reports { get; set; }
    }
}
