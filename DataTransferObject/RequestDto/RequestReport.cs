using Entity.Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.RequestDto
{
    public class RequestReport
    {
        public int ReaportId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        //public bool IsActive { get; set; } = true;
       // public Project? Project { get; set; }
        public int ProjectId { get; set; }
    }
}
