﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_swagger_authentication.Models
{
    public class Values
    {
        [Key]
        public int Id { get; set; }

        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }
    }
}
