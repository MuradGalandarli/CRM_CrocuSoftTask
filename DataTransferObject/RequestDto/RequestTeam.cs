﻿using Entity.Layer.Entity;
using Entity.Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.RequestDto
{
    public class RequestTeam
    {
        public int TeamId { get; set; }     
        public string? Name { get; set; }
        // public bool IsActive { get; set; } = true;
       
        //  public List<Project>? Projects { get; set; }
    }
}
