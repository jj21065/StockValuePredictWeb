using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebAPI.Models.GovData
{
    public class ProfitDetailModel
    {
  
        public string Date { get; set; }
        public string Year { get; set; }
        public string Season { get; set; }
        public string SecuritiesCompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string 營業收入 { get; set; }
        public string 營業成本 { get; set; }

        [JsonProperty("原始認列生物資產及農產品之利益（損失）")]
        public string 原始認列生物資產及農產品之利益損失 { get; set; }

        [JsonProperty("生物資產當期公允價值減出售成本之變動利益（損失）")]
        public string 生物資產當期公允價值減出售成本之變動利益損失 { get; set; }

        [JsonProperty("營業毛利（毛損）")]
        public string 營業毛利毛損 { get; set; }

        [JsonProperty("未實現銷貨（損）益")]
        public string 未實現銷貨損益 { get; set; }

        [JsonProperty("已實現銷貨（損）益")]
        public string 已實現銷貨損益 { get; set; }

        [JsonProperty("營業毛利（毛損）淨額")]
        public string 營業毛利毛損淨額 { get; set; }
        public string 營業費用 { get; set; }
        public string 其他收益及費損淨額 { get; set; }

        [JsonProperty("營業利益（損失）")]
        public string 營業利益損失 { get; set; }
        public string 營業外收入及支出 { get; set; }

        [JsonProperty("稅前淨利（淨損）")]
        public string 稅前淨利淨損 { get; set; }

        [JsonProperty("所得稅費用（利益）")]
        public string 所得稅費用利益 { get; set; }

        [JsonProperty("繼續營業單位本期淨利（淨損）")]
        public string 繼續營業單位本期淨利淨損 { get; set; }
        public string 停業單位損益 { get; set; }
        public string 合併前非屬共同控制股權損益 { get; set; }

        [JsonProperty("本期淨利（淨損）")]
        public string 本期淨利淨損 { get; set; }

        [JsonProperty("其他綜合損益（淨額）")]
        public string 其他綜合損益淨額 { get; set; }
        public string 合併前非屬共同控制股權綜合損益淨額 { get; set; }
        public string 本期綜合損益總額 { get; set; }

        [JsonProperty("淨利（淨損）歸屬於母公司業主")]
        public string 淨利淨損歸屬於母公司業主 { get; set; }

        [JsonProperty("淨利（淨損）歸屬於共同控制下前手權益")]
        public string 淨利淨損歸屬於共同控制下前手權益 { get; set; }

        [JsonProperty("淨利（淨損）歸屬於非控制權益")]
        public string 淨利淨損歸屬於非控制權益 { get; set; }
        public string 綜合損益總額歸屬於母公司業主 { get; set; }
        public string 綜合損益總額歸屬於共同控制下前手權益 { get; set; }
        public string 綜合損益總額歸屬於非控制權益 { get; set; }

        [JsonProperty("基本每股盈餘（元）")]
        public string 基本每股盈餘元 { get; set; }
        


    }
}
