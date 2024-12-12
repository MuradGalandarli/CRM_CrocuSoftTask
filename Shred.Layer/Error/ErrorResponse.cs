﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shred.Layer.Error
{
    public class ErrorResponse
    {
        public string? Error { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
    }
}