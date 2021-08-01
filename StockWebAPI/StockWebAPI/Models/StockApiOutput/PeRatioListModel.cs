using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebAPI.Models.StockApiOutput
{
    public class PeRatioListModel
    {
        //產業本益比
        public float IndustryPeRatio { get; set; }

        //法人本益比
        public float LegalPeRatio { get; set; }

        //歷史計算本益比
        public float HistoryPeRatio { get; set; }
    }

    public class PeRatioModel
    {
        public StockInfoModel StockInfo { get; set; } = new StockInfoModel();
        public float CurrentPeRatio { get; set; }
        //產業本益比
        public float IndustryPeRatio { get; set; }

        //法人本益比
        public float LegalPeRatio { get; set; }

        //歷史計算本益比
        public float HistoryPeRatio { get; set; }
    }
}
