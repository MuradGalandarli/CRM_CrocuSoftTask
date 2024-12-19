using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Layer.Entity
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; } = true;
        public List<AppUser>? AppUsers { get; set; }
        public List<Project>? Projects { get; set; }


       
    }
}
