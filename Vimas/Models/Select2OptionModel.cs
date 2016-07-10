﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vimas.Models
{
    public class Select2OptionModel

    {
        public string id { get; set; }
        public string text { get; set; }

    }

    public class Select2PagedResult
    {
        public int Total { get; set; }
        public IEnumerable<Select2OptionModel> Results { get; set; }

    }
}