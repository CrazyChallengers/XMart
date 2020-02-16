﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using XMart.Models;

namespace XMart.ResponseData
{
    public class CategoryRD : CommonRD
    {
        [JsonProperty("result")]
        public List<Category> result { get; set; }   //comment
    }

}
