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
        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public HomeContentDataList[] result { get; set; }
    }

    public class HomeContentDataList
    {
        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public long created { get; set; }   //comment

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long id { get; set; }   //comment

        [JsonProperty("limitNum", NullValueHandling = NullValueHandling.Ignore)]
        public int limitNum { get; set; }   //comment

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string name { get; set; }   //comment

        [JsonProperty("panelContents", NullValueHandling = NullValueHandling.Ignore)]
        public HomePanelContent[] panelContents { get; set; }   //comment

        [JsonProperty("position", NullValueHandling = NullValueHandling.Ignore)]
        public int position { get; set; }   //comment

        [JsonProperty("remark", NullValueHandling = NullValueHandling.Ignore)]
        public string remark { get; set; }   //comment

        [JsonProperty("sortOrder", NullValueHandling = NullValueHandling.Ignore)]
        public int sortOrder { get; set; }   //comment

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public int status { get; set; }   //comment

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public int type { get; set; }   //comment

        [JsonProperty("updated", NullValueHandling = NullValueHandling.Ignore)]
        public long updated { get; set; }   //comment

    }
}
