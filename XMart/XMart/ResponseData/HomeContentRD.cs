using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using XMart.Models;
using Newtonsoft.Json;

namespace XMart.ResponseData
{
    public class HomeContentRD : CommonRD
    {
        [JsonProperty("result")]
        public HomeContentDataList[] result { get; set; }
    }

    public class HomeContentDataList
    {
        [JsonProperty("created")]
        public string created { get; set; }   //comment

        [JsonProperty("id")]
        public int id { get; set; }   //comment

        [JsonProperty("limitNum")]
        public int limitNum { get; set; }   //comment

        [JsonProperty("name")]
        public string name { get; set; }   //comment

        [JsonProperty("panelContents")]
        public HomePanelContent[] panelContents { get; set; }   //comment

        [JsonProperty("position")]
        public int position { get; set; }   //comment

        [JsonProperty("remark")]
        public string remark { get; set; }   //comment

        [JsonProperty("sortOrder")]
        public int sortOrder { get; set; }   //comment

        [JsonProperty("status")]
        public int status { get; set; }   //comment

        [JsonProperty("type")]
        public int type { get; set; }   //comment

        [JsonProperty("updated")]
        public string updated { get; set; }   //comment

    }
}
