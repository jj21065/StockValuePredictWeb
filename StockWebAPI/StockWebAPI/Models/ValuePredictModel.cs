using System;

namespace StockWebAPI.Models
{
    public class ValuePredictModel
    {
        string StockId { get; set; }
        string StockName { get; set; }
        DateTime TableCreateDate { get; set; }
        /// <summary>
        /// 近一月營收
        /// </summary>
        double RevenueRecentMonth { get; set; }
        /// <summary>
        /// 預估近一季營收
        /// </summary>
        double RevenueRecentSeasonPredict { get; set; }
        /// <summary>
        /// 近一季毛利率
        /// </summary>
        double GrossMarginRecentSeason { get; set; }
        /// <summary>
        /// 預估毛利
        /// </summary>
        double GrossProfitPredict { get; set; }
        /// <summary>
        /// 近一季營業費用
        /// </summary>
        double OperatingCostRecentSeason { get; set; }
        /// <summary>
        /// 營業利率稅前預估
        /// </summary>
        double OperatingInterestRateBeforeTaxPredict { get; set; }
        /// <summary>
        /// 業外收益預估稅後 = ((營業利率稅前預估) - (業外收支))*0.8 
        /// </summary>
        double ProfitPredict { get; set; }
        /// <summary>
        /// 業外收支
        /// </summary>
        double OutsideProfit { get; set; }
        /// <summary>
        ///  繼續營業單位損益
        /// </summary>
        double ContinureOperatingProfit { get; set; }
        /// <summary>
        ///  近一季EPS = 近一季稅後淨利 * 近一季EPS / 繼續營業單位損益 
        /// </summary>
        double EPSRecentSeason { get; set; }

        double EPSSeasonPredict { get; set; }

        double EPSYearPridict { get; set; }

        /// <summary>
        /// 產業本益比
        /// </summary>
        double IndustryPEratio { get; set; }
        /// <summary>
        /// 法人本益比
        /// </summary>
        double PredictPEratio { get; set; }

    }
}
