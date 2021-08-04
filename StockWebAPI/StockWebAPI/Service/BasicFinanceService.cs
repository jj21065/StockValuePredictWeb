using Newtonsoft.Json;
using StockWebAPI.Common;
using StockWebAPI.Models;
using StockWebAPI.Models.GovData;
using StockWebAPI.Models.SqlModels;
using StockWebAPI.Models.StockApiInput;
using StockWebAPI.Models.StockApiOutput;
using StockWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockWebAPI.Service
{
    public class BasicFinanceService
    {
        SqlStockFinanceRepo sqlRepo = new SqlStockFinanceRepo();
        public List<MonthlyProfitModel> GetMonthlyProfitData()
        {
            List<MonthlyProfitModel> incomeList = new List<MonthlyProfitModel>();
            string targetUrl = "https://openapi.twse.com.tw/v1/opendata/t187ap05_L";
            
            HttpRequestControl httpRequestControl = new HttpRequestControl();
            var result = httpRequestControl.HttpRequestMethod(targetUrl, HttpRequestControl.Method.GET);
            
            
            if(result.IndexOf('[') != -1)
            {
                result = result.Replace('[', ' ');
                result = result.Replace(']', ' ');
                string[] resultList = result.Split('\n');
                if(resultList != null)
                {
                    for(int i = 0;i<resultList.Length;i++)
                    {
                        if (resultList[i] != string.Empty)
                        {
                            try
                            {
                                MonthlyProfitModel incomeModel = JsonConvert.DeserializeObject<MonthlyProfitModel>(resultList[i].Remove(resultList[i].Length-1));
                                if (incomeModel != null)
                                {
                                    incomeList.Add(incomeModel);
                                }
                            }catch(Exception ex)
                            {

                            }
                        }
                    }
                }
            }

            
            return incomeList;
         
        }

        public List<ProfitDetailModel> GetFinanceData()
        {

            List<ProfitDetailModel> detailList = new List<ProfitDetailModel>();
            string targetUrl = "https://www.tpex.org.tw/openapi/v1/mopsfin_t187ap06_O_ci";
            HttpRequestControl httpRequestControl = new HttpRequestControl();
            var result = httpRequestControl.HttpRequestMethod(targetUrl, HttpRequestControl.Method.GET);

            if (result.IndexOf('[') != -1)
            {
                result = result.Replace('[', ' ');
                result = result.Replace(']', ' ');
                string[] resultList = result.Split('\n');
                if (resultList != null)
                {
                    for (int i = 0; i < resultList.Length; i++)
                    {
                        if (resultList[i] != string.Empty)
                        {
                            try
                            {
                                ProfitDetailModel incomeModel = JsonConvert.DeserializeObject<ProfitDetailModel>(resultList[i].Remove(resultList[i].Length - 1));
                                if (incomeModel != null)
                                {
                                    detailList.Add(incomeModel);
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
            }
            return detailList;
        }

        public List<float> GetStockMonthRevenue(List<KeyValuePair<int, int>> yearMonth,string stockId)
        {
            List<float> rs = sqlRepo.GetStockMonthlyRevenue(yearMonth, stockId);
            return rs;
        }
    

        public StockValuePredictModel GetCompanyPredictValue(StockApiParaModel paraModel,string errMsg)
        {
            try
            {
                errMsg = string.Empty;
                StockValuePredictModel responseModel = new StockValuePredictModel();
                
                StockApiParaModel inputModel = new StockApiParaModel()
                {
                    StockInfo = paraModel.StockInfo,
                    Year = paraModel.Year,
                    Month = paraModel.Month,
                    Season = paraModel.Season
                };
                
                List<KeyValuePair<int, int>> yearMonths = new List<KeyValuePair<int, int>> ();
                yearMonths.Add(new KeyValuePair<int,int>(paraModel.Year,paraModel.Month));
                yearMonths.Add(new KeyValuePair<int, int>(((paraModel.Month-1>0)?paraModel.Year:paraModel.Year-1), ((paraModel.Month-1)<0)?12:paraModel.Month-1));
                yearMonths.Add(new KeyValuePair<int, int>(((paraModel.Month - 2 >0) ? paraModel.Year: paraModel.Year - 2), ((paraModel.Month - 2) < 0) ? 11 : paraModel.Month - 2));

                List<float> revenueMonths = sqlRepo.GetStockMonthlyRevenue(yearMonths, paraModel.StockInfo.StockId);

                SqlFinanceSeasonDetailModel detailModel = sqlRepo.GetStockSeasonDetail(inputModel);

                if (revenueMonths.Count > 0)
                {
                    float seasonRevenue = revenueMonths.Sum();
                    double marginProfit = (detailModel.Revenue - detailModel.Cost) / detailModel.Revenue;
                    double predictMargin = seasonRevenue * marginProfit;
                    double predictSeasonProfitAfterTax = (predictMargin - detailModel.OperatingFee + detailModel.NonOperatingProfit) * 0.8;

                    //年EPS 預估
                    double predictEPS = predictSeasonProfitAfterTax * detailModel.EPS / detailModel.TotalProfitAfterTax * 4;
                    //double predictValue = paraModel.PERatio * predictEPS;

                    
                    responseModel.StockPara.Year = inputModel.Year;
                    responseModel.StockPara.Month = inputModel.Month;
                    
                    responseModel.StockPara.StockInfo.StockId = detailModel.CompanyId;
                    responseModel.PredictSeasonMarginProfit = marginProfit;
                    responseModel.PredictTotalProfitAfterTax = predictSeasonProfitAfterTax;
                    responseModel.PredictYearEPS = predictEPS;

                   // responseModel.CompanyName = "test";
                    return responseModel;
                }
                errMsg = @"股票編號錯誤或是年/月錯誤";
                return null;
            }catch(Exception ex)
            {
                errMsg = ex.ToString();
                return null;
            }
        }

        public object GetHistoryPeRatio(StockApiParaModel paraModel)
        {
            try
            {
                string stockId = paraModel.StockInfo.StockId.ToString();
                string year = ((paraModel.Year >= 1900) ? paraModel.Year : (paraModel.Year+ 1911)).ToString();
                
                string date = (paraModel.Month == DateTime.Today.Month)?DateTime.Today.ToString("MMdd") : (new DateTime(paraModel.Year, paraModel.Month , 1)).ToString("MMdd");
                string targetUrl = $"https://www.twse.com.tw/exchangeReport/BWIBBU?response=json&date={year+date}&stockNo={stockId}&_=1627194258648";
                if(paraModel.StockInfo.StockType.ToLower() == CompanyType.OTC.ToString().ToLower())
                {
                    string year_otc = ((paraModel.Year >= 1900) ? (paraModel.Year - 1911).ToString() : (paraModel.Year).ToString());
                    targetUrl = $"https://www.tpex.org.tw/web/stock/aftertrading/peratio_stk/pera_result.php?l=zh-tw&d={year_otc}/{paraModel.Month}&stkno={stockId}&_=1627353996499";
                }
                HttpRequestControl httpRequestControl = new HttpRequestControl();
                Dictionary<string, string> headers = new Dictionary<string, string>();
                
                headers.Add("Accept", "application/json, text/javascript, */*; q=0.01");
            
                headers.Add("Host", "www.twse.com.tw");
                headers.Add("Referer", "https://www.twse.com.tw/zh/page/trading/exchange/BWIBBU.html");
                headers.Add("Sec-Ch-Ua", "\"Chromium\";v=\"92\", \" Not A;Brand\";v=\"99\", \"Google Chrome\";v=\"92\"");
                headers.Add("Sec-Ch-Ua-Mobile", "?0");

                headers.Add("Sec-Fetch-Dest", "empty");
                headers.Add("Sec-Fetch-Mode", "cors");
                headers.Add("Sec-Fetch-Site", "ross-site");
                headers.Add("Sec-Fetch-User", "?1");

                headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.107 Safari/537.36");

                var response = httpRequestControl.HttpRequestMethod(targetUrl, HttpRequestControl.Method.GET,null,headers);
                if (paraModel.StockInfo.StockType.ToLower() == CompanyType.SII.ToString().ToLower())
                {
                    GovWebResult webResult = Newtonsoft.Json.JsonConvert.DeserializeObject<GovWebResult>(response);
                    return webResult;
                }
                else if(paraModel.StockInfo.StockType.ToLower() == CompanyType.OTC.ToString().ToLower())
                {
                    OTCWebResult webResult = Newtonsoft.Json.JsonConvert.DeserializeObject<OTCWebResult>(response);
                    return webResult;
                }
                Thread.Sleep(150);
                return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<StockInfoModel> GetStockInfo()
        {
            return sqlRepo.GetStockIdNamePair();
        }

        public List<StockInfoModel> GetAutoPositiveEPSStock(string errMsg)
        {
            var potentialStockList = sqlRepo.GetAutoPositiveEPSStock(); //取出有潛力 EPS穩定且正的

            return potentialStockList;
        }

        public List<PeRatioModel> GetAutoSelectLowPeRatioStock()
        {
            var result = sqlRepo.GetAutoSelectLowPeRatioStock();
            return result;
        }

        public object GetTop20TradingAmount_TWSE()
        {

            string today = DateTime.Today.ToString("yyyyMMdd");
            string targetUrl = $"https://www.twse.com.tw/exchangeReport/MI_INDEX20?response=json&date={today}&_=1627977214277";

            HttpRequestControl httpRequestControl = new HttpRequestControl();
            var result = httpRequestControl.HttpRequestMethod(targetUrl, HttpRequestControl.Method.GET);
            var jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<GovWebResult>(result);
            //Console.WriteLine(result);
            return jsonObj;
        }
        public List<SqlDailyTop20TradingModel> GetTop20TradingAmount_Database()
        {
            List< SqlDailyTop20TradingModel> sqlModels = sqlRepo.GetTop20TradingStock_Sql();
            return sqlModels;
        }
    }
}
