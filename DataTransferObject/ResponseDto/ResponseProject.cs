﻿using Entity.Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.ResponseDto
{
    public class ResponseProject
    {
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public string? Description { get; set; }
     //   public bool IsActive { get; set; } = true;
        public string? Image { get; set; }
      //  public Team? Team { get; set; }
        public int? TeamId { get; set; }
        public List<ResponseReport>? Reports { get; set; }
    }
}
