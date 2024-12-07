using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Layer.Entity
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }
        public string? Name { get; set; }
        public Team? Team { get; set; }
        public int TeamId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
