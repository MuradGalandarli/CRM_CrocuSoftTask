using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Layer.Entity
{
    public class Reaport
    {
        [Key]
        public int ReaportId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public Project? Project { get; set; }
        public int ProjectId { get; set; }
    }
}
