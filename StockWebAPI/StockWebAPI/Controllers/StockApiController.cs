using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockWebAPI.Common;
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

            PeRatioModel peRatioModel = new PeRatioModel();
            StockApiParaModel paraModel = new StockApiParaModel()
            {
                StockInfo = new StockInfoModel()
                {
                    StockId = para.StockInfo.StockId,
                    StockName = para.StockInfo.StockName,
                    StockType = para.StockInfo.StockType
                }
            };
            for (int season = 1; season <= 4; season++)
            {

                paraModel.Year = para.Year - 1;
                paraModel.Month = season * 3;

                object historyResult = Service.GetHistoryPeRatio(paraModel);

                var historyPeRatio = CommonFunction.ReturnPeRatioMonthAVG(historyResult, paraModel, errMsg);


                peRatioModel.HistoryPeRatio += (float)historyPeRatio;

                if (historyResult == null)
                {
                    errMsg = "本益比查詢問題，請確認股票編號或是更換年份月分";
                    var result = new ApiError("01", errMsg) { PayLoad = predictResponse };
                    return result;
                }
                Thread.Sleep(100);
            }
           
            predictResponse.PeRatioList.HistoryPeRatio = (float)peRatioModel.HistoryPeRatio/4;
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

        [HttpGet("/GetAutoSelectLowPeRatio")]
        public async Task<ActionResult<ApiResult<object>>> GetAutoSelectLowPeRatio()
        {
            string errMsg = string.Empty;
            var potentialStockList = Service.GetAutoPositiveEPSStock(errMsg);

            List<PeRatioModel> peRatioModelList = new List<PeRatioModel>();
            Random rnd = new Random();  //產生亂數初始值

            foreach (var stock in potentialStockList)
            {
                PeRatioModel peRatioModel = new PeRatioModel();
                StockApiParaModel paraModel = new StockApiParaModel()
                {
                    StockInfo = new StockInfoModel()
                    {
                        StockId = stock.StockId,
                        StockName = stock.StockName,
                        StockType = stock.StockType
                    }
                };
                DateTime nowaday = DateTime.Now;
                int seasonCount = 4;
                for (int season = 1; season <= seasonCount; season++)
                {
                    paraModel.Year = nowaday.Year-1;
                    paraModel.Month = season * 3;

                    object historyResult = Service.GetHistoryPeRatio(paraModel);

                    var historyPeRatio = CommonFunction.ReturnPeRatioMonthAVG(historyResult, paraModel, errMsg);
                    
                    
                    peRatioModel.HistoryPeRatio += (float)historyPeRatio;

                    var rand = rnd.Next(1, 4);
                    var delayTime = rand * 900;
                    Thread.Sleep(3100);
                }
                peRatioModel.HistoryPeRatio /= 4;

                paraModel.Year = nowaday.Year;
                paraModel.Month = nowaday.Month; 


                object nowResult = Service.GetHistoryPeRatio(paraModel);
                var nowPeRatio = CommonFunction.ReturnPeRatioMonthAVG(nowResult, paraModel, errMsg);
                Thread.Sleep(3000);
                if (nowPeRatio < peRatioModel.HistoryPeRatio)
                {
                    peRatioModel.CurrentPeRatio = (float)nowPeRatio;
                    peRatioModel.StockInfo = paraModel.StockInfo;
                    peRatioModelList.Add(peRatioModel);
                }
            }
            var result = new ApiResult<object>(peRatioModelList);
            return result;
            
        }

        
    }
}

