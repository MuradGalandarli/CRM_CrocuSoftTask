﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject.RequestDto
{
    public class RequestUserUpdate
    {
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public string? SurName { get; set; }
      //  public string? Email { get; set; }
     //   public string? Password { get; set; }
        public int TeamId { get; set; }
    }
}
