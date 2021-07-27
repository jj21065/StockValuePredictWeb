using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockWebAPI.Models;
using StockWebAPI.Models.GovData;
using StockWebAPI.Models.StockApiInput;
using StockWebAPI.Models.StockApiOutput;
using StockWebAPI.Service;

namespace StockWebAPI.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class StockApiController : ControllerBase
    {
        BasicFinanceService Service = new BasicFinanceService();
        /// <summary>
        /// Get Monthly profit report
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/GetMonthlyProfit")]
        public async Task<ActionResult<List<MonthlyProfitModel>>> GetMonthlyProfit()
        {
            
            List<MonthlyProfitModel> resultList = new List<MonthlyProfitModel>();

            resultList = Service.GetMonthlyProfitData();
            
            return resultList;
        }


        /// <summary>
        /// get total profit detail
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/GetProfitDetail")]
        public async Task<ActionResult<List<ProfitDetailModel>>> GetProfitDetail()
        {
            
            List<ProfitDetailModel> resultList = new List<ProfitDetailModel>();

            resultList = Service.GetFinanceData();

            return resultList;
        }
        [HttpPost("/MonthlyRevenue")]
        public async Task<ActionResult<List<float>>> GetMonthlyRevenue(StockApiParaModel para)
        {
            List<float> resultList = new List<float>();
            List<KeyValuePair<int, int>> d = new List<KeyValuePair<int, int>>();
            d.Add(new KeyValuePair<int, int>(para.Year, para.Month));
            resultList = Service.GetStockMonthRevenue(d,para.StockInfo.StockId);
            return resultList;
        }

        [HttpPost("/PredictValueInfo")]
        public async Task<ActionResult<ApiResult<object>>> GetPredictValueInfo(StockApiParaModel para)
        {

            string errMsg = string.Empty;
            StockValuePredictModel predictResponse = Service.GetCompanyPredictValue(para,errMsg);

            object historyResult = Service.GetHistoryPeRatio(para);

            if(historyResult == null)
            {
                errMsg = "本益比查詢問題，請確認股票編號或是更換年份月分";
                var result = new ApiError("01", errMsg) { PayLoad = predictResponse };
                return result;
            }
            var dataSize = 0;
            var historyPeRatio = 0d;
            if (para.StockInfo.StockType.ToLower() == CompanyType.SII.ToString().ToLower())
            {
                GovWebResult obj = (GovWebResult)(historyResult);
                foreach (var data in obj.data)
                {
                    if (data != null)
                    {
                        if (data[3] != null)
                        {
                            try
                            {
                                double peRatio = Convert.ToDouble(data[3].ToString());
                                historyPeRatio += peRatio;
                                dataSize++;
                            }
                            catch (Exception ex)
                            {
                                errMsg = ex.ToString();
                            }
                        }
                    }
                }
            }
            else if(para.StockInfo.StockType.ToLower() == CompanyType.OTC.ToString().ToLower())
            {
                OTCWebResult obj = (OTCWebResult)(historyResult);
                foreach (var data in obj.aaData)
                {
                    if (data != null)
                    {
                        if (data[1] != null)
                        {
                            try
                            {
                                double peRatio = Convert.ToDouble(data[1].ToString());
                                historyPeRatio += peRatio;
                                dataSize++;
                            }
                            catch (Exception ex)
                            {
                                errMsg = ex.ToString();
                            }
                        }
                    }
                }
            }

            historyPeRatio = historyPeRatio / dataSize;
            predictResponse.PeRatioList.HistoryPeRatio = (float)historyPeRatio;
            predictResponse.PeRatioList.IndustryPeRatio = 10;
            predictResponse.PeRatioList.LegalPeRatio = 15;


            if (errMsg != string.Empty)
            {
                var result = new ApiError("01", errMsg) { PayLoad = predictResponse };

                return result;
            }
            else
            {
                var result = new ApiResult<object>(predictResponse);
                return result;
            }   
        }

        [HttpGet("/GetStockInfo")]
        public async Task<ActionResult<ApiResult<object>>> GetStockInfo()
        {
            string errMsg = string.Empty;
            var response=  Service.GetStockInfo();
            if (errMsg != string.Empty)
            {
                var result = new ApiError("01", errMsg) { PayLoad = response };

                return result;
            }
            else
            {
                var result = new ApiResult<object>(response);
                return result;
            }
        }
    }
}

