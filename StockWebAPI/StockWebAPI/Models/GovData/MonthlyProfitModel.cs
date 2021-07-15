using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebAPI.Models.GovData
{
    public class MonthlyProfitModel
    {
        public string 出表日期 { get; set; }
        public string 資料年月 { get; set; }
        public string 公司代號 { get; set; }
        public string 公司名稱 { get; set; }
        public string 產業別 { get; set; }

        [JsonProperty("營業收入-當月營收")]
        public string 營業收入當月營收 { get; set; }

        [JsonProperty("營業收入-上月營收")]
        public string 營業收入上月營收 { get; set; }

        [JsonProperty("營業收入-去年當月營收")]
        public string 營業收入去年當月營收 { get; set; }

        [JsonProperty("營業收入-上月比較增減(%)")]
        public string 營業收入上月比較增減 { get; set; }

        [JsonProperty("營業收入-去年同月增減(%)")]
        public string 營業收入去年同月增減 { get; set; }

        [JsonProperty("累計營業收入-當月累計營收")]
        public string 累計營業收入當月累計營收 { get; set; }

        [JsonProperty("累計營業收入-去年累計營收")]
        public string 累計營業收入去年累計營收 { get; set; }

        [JsonProperty("累計營業收入-前期比較增減(%)")]
        public string 累計營業收入前期比較增減 { get; set; }
        public string 備註 { get; set; }
    }
}
