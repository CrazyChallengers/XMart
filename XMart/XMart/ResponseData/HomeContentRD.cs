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
        public List<HomeContentDataList> result { get; set; }
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

/*
 {
  "code": 0,
  "message": "string",
  "result": [
    {
      "created": "2019-10-28T03:15:16.235Z",
      "id": 0,
      "limitNum": 0,
      "name": "string",
      "panelContents": [
        {
          "created": "2019-10-28T03:15:16.235Z",
          "fullUrl": "string",
          "id": 0,
          "panelId": 0,
          "picUrl": "string",
          "picUrl2": "string",
          "picUrl3": "string",
          "productId": 0,
          "productImageBig": "string",
          "productName": "string",
          "salePrice": 0,
          "sortOrder": 0,
          "subTitle": "string",
          "type": 0,
          "updated": "2019-10-28T03:15:16.236Z"
        }
      ],
      "position": 0,
      "remark": "string",
      "sortOrder": 0,
      "status": 0,
      "type": 0,
      "updated": "2019-10-28T03:15:16.236Z"
    }
  ],
  "success": true,
  "timestamp": 0
}
     */
