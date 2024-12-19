using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Layer.Entity
{
    public class AppUser:IdentityUser
    {
     
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public string? RefreshToken { get; set; }
        public string? Role { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public int? TeamId { get; set; } 
        public Team? Team { get; set; }

    }
}
