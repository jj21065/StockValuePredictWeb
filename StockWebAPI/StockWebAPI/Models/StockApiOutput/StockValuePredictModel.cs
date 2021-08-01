using StockWebAPI.Models.StockApiInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebAPI.Models.StockApiOutput
{
    public class StockValuePredictModel
    {
        public StockApiParaModel StockPara { get; set; } = new StockApiParaModel();
        public PeRatioListModel PeRatioList { get; set; } = new PeRatioListModel();
        /// <summary>
        /// 毛利率預估
        /// </summary>
        public double PredictSeasonMarginProfit { get; set; }
        public double PredictTotalProfitAfterTax { get; set; }
        public double PredictYearEPS { get; set; }
        
    }
}
