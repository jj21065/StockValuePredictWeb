using StockWebAPI.Common.SqlModule;
using StockWebAPI.Models.SqlModels;
using StockWebAPI.Models.StockApiInput;
using StockWebAPI.Models.StockApiOutput;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebAPI.Repository
{
    public class SqlStockFinanceRepo
    {
        
        public List<float> GetStockMonthlyRevenue(List<KeyValuePair<int, int>> yearMonths,string stockId)
        {
            List<float> monthR = new List<float>();
            SqlControl sql = new SqlControl();
            SqlConnectionParameter para = new SqlConnectionParameter()
            {
                DataSource = "tcp:jakedatabase.database.windows.net,1433",
                InitialCatalog = "StockDB", //Y1H|U6Vt#DGW5ifa
                UserId = "Stock_Developer", //Stock_Developer
                Password = "Y1H|U6Vt#DGW5ifa" //Y1H|U6Vt#DGW5ifa
            };

            string conditionString = $" Where CompanyId ='{stockId}' and (";
            foreach (var ym in yearMonths)
            {
                conditionString = conditionString + $" (Year = '{ym.Key}' and Month = '{ym.Value}') or";
            }
            conditionString = conditionString.Remove(conditionString.Length - 3) + ") order by tDateTime desc";
            try
            {
                string tblName = "dbo.MonthRevenueTbl";

                string sqlString = "Select Revenue From " + tblName + conditionString;
                DataTable tb = sql.SqlSelect(sqlString, para);
                foreach (DataRow row in tb.Rows)
                {
                    try
                    {
                        float r = Convert.ToSingle(row["Revenue"]);
                        monthR.Add(r);
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return monthR;
        
        }

        public SqlFinanceSeasonDetailModel GetStockSeasonDetail(StockApiParaModel InputPara)
        {
            SqlControl sql = new SqlControl();
            SqlFinanceSeasonDetailModel financeDetail = new SqlFinanceSeasonDetailModel();
            SqlConnectionParameter para = new SqlConnectionParameter()
            {
                DataSource = "tcp:jakedatabase.database.windows.net,1433",
                InitialCatalog = "StockDB", //Y1H|U6Vt#DGW5ifa
                UserId = "Stock_Developer", //Stock_Developer
                Password = "Y1H|U6Vt#DGW5ifa" //Y1H|U6Vt#DGW5ifa
            };

            string conditionString = $" Where CompanyId ='{InputPara.StockInfo.StockId}' and (";
            
            conditionString = conditionString + $" Year = '{InputPara.Year}' and Season = '{InputPara.Season}') Order by tDateTime desc";
            
            
            try
            {
                string tblName = "dbo.SeasonProfitDetailTbl";
                
                string sqlString = 
                    "Select [CompanyId]    ,[CompanyName]      ,[Year]      ,[Season]      ,[Revenue]      ,[Cost]      ,[OperatingFee]      ,[Profit]      ,[ProfitMargin]      ,[AdditionalProfit]      ,[EPS]      ,[TotalProfitBeforeTax]      ,[TotalProfitAfterTax]      ,[CompanyType]      ,[tDateTime] From " + tblName + conditionString;
                DataTable tb = sql.SqlSelect(sqlString, para);
                if(tb.Rows.Count > 0)
                {
                    DataRow tr = tb.Rows[0];
                    financeDetail = new SqlFinanceSeasonDetailModel()
                    {
                        CompanyId = tr["CompanyId"].ToString(),
                        CompanyName = tr["CompanyName"].ToString(),
                        Year = Convert.ToInt32(tr["Year"]),
                        Season = Convert.ToInt32(tr["Season"]),
                        Revenue = Convert.ToDouble(tr["Revenue"]),
                        Cost = Convert.ToDouble(tr["Cost"]),
                        OperatingFee = Convert.ToDouble(tr["OperatingFee"]),
                        NonOperatingProfit = Convert.ToDouble(tr["AdditionalProfit"]),
                        TotalProfitAfterTax = Convert.ToDouble(tr["TotalProfitAfterTax"]),
                        EPS = Convert.ToDouble(tr["EPS"]),
                    };
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
                
            }
            return financeDetail;
        }

        public List<StockInfoModel> GetStockIdNamePair()
        {
            SqlControl sql = new SqlControl();
            SqlFinanceSeasonDetailModel financeDetail = new SqlFinanceSeasonDetailModel();
            SqlConnectionParameter para = new SqlConnectionParameter()
            {
                DataSource = "tcp:jakedatabase.database.windows.net,1433",
                InitialCatalog = "StockDB", //Y1H|U6Vt#DGW5ifa
                UserId = "Stock_Developer", //Stock_Developer
                Password = "Y1H|U6Vt#DGW5ifa" //Y1H|U6Vt#DGW5ifa
            };
            List<StockInfoModel> idNamePair = new List<StockInfoModel>();
            string conditionString = $" Where CompanyName!= 'null'";

            try
            {
                string tblName = "dbo.SeasonProfitDetailTbl";

                string sqlString =
                    "Select DISTINCT  [CompanyId]    ,[CompanyName] ,[CompanyType] From " + tblName + conditionString;
                DataTable tb = sql.SqlSelect(sqlString, para);
                if (tb.Rows.Count > 0)
                {
                 
                    foreach(DataRow tr in tb.Rows)
                    {
                        var CompanyId = tr["CompanyId"].ToString();
                        var CompanyName = tr["CompanyName"].ToString();
                        var CompanyType = tr["CompanyType"].ToString();
                        try
                        {
                            
                            StockInfoModel model = new StockInfoModel()
                            {
                                StockId = CompanyId,
                                StockName = CompanyName.Trim(),
                                StockType = CompanyType.Trim()

                            };

                            idNamePair.Add(model);
                        }
                        catch(Exception ex){

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return idNamePair;
        }

        public List<StockInfoModel> GetAutoPositiveEPSStock()
        {
            SqlControl sql = new SqlControl();
            SqlFinanceSeasonDetailModel financeDetail = new SqlFinanceSeasonDetailModel();
            SqlConnectionParameter para = new SqlConnectionParameter()
            {
                DataSource = "tcp:jakedatabase.database.windows.net,1433",
                InitialCatalog = "StockDB", //Y1H|U6Vt#DGW5ifa
                UserId = "Stock_Developer", //Stock_Developer
                Password = "Y1H|U6Vt#DGW5ifa" //Y1H|U6Vt#DGW5ifa
            };
            List<StockInfoModel> idNamePair = new List<StockInfoModel>();
            string conditionString = $" Where CompanyName!= 'null'";

            try
            {
                string tblName = "dbo.SeasonProfitDetailTbl";

                string sqlString = @$"Select  CompanyId,CompanyName,CompanyType, seasons From
                                    (SELECT [CompanyId],CompanyName,CompanyType, count(*) as seasons
                                    FROM [dbo].[SeasonProfitDetailTbl] 
                                    where EPS > 0 and Year = 109 and TotalProfitAfterTax > 2000000 
                                    Group by CompanyId ,CompanyName,CompanyType) as  A
                                    where seasons = 4";

                DataTable tb = sql.SqlSelect(sqlString, para);
                if (tb.Rows.Count > 0)
                {

                    foreach (DataRow tr in tb.Rows)
                    {
                        var CompanyId = tr["CompanyId"].ToString();
                        var CompanyName = tr["CompanyName"].ToString();
                        var CompanyType = tr["CompanyType"].ToString();
                        try
                        {
                          
                            StockInfoModel model = new StockInfoModel()
                            {
                                StockId = CompanyId,
                                StockName = CompanyName.Trim(),
                                StockType = CompanyType.Trim()

                            };

                            idNamePair.Add(model);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return idNamePair;
        }

        public List<PeRatioModel> GetAutoSelectLowPeRatioStock()
        {
            SqlControl sql = new SqlControl();
            SqlFinanceSeasonDetailModel financeDetail = new SqlFinanceSeasonDetailModel();
            SqlConnectionParameter para = new SqlConnectionParameter()
            {
                DataSource = "tcp:jakedatabase.database.windows.net,1433",
                InitialCatalog = "StockDB", //Y1H|U6Vt#DGW5ifa
                UserId = "Stock_Developer", //Stock_Developer
                Password = "Y1H|U6Vt#DGW5ifa" //Y1H|U6Vt#DGW5ifa
            };
            List<PeRatioModel> idNamePair = new List<PeRatioModel>();
            string today = DateTime.Today.ToString("yyyy-MM-dd");
            string conditionString = $" Where tDate = '{today}'";

            try
            {
                string tblName = "dbo.SeasonProfitDetailTbl";

                string sqlString = @"SELECT [CompanyId]
                                    ,[CompanyName]
                                    ,[CompanyType]
                                    ,[CurrentPeRatio]
                                    ,[HistoryPeRatio]
                                    ,[tDate]
                                    FROM[dbo].[PeRatioAutoSelectTbl] "/* + conditionString*/;

                 DataTable tb = sql.SqlSelect(sqlString, para);
                if (tb.Rows.Count > 0)
                {

                    foreach (DataRow tr in tb.Rows)
                    {
                        var CompanyId = tr["CompanyId"].ToString();
                        var CompanyName = tr["CompanyName"].ToString();
                        var CompanyType = tr["CompanyType"].ToString();
                        var CurrentPeRatio = Convert.ToSingle(tr["CurrentPeRatio"].ToString());
                        var HistoryPeRatio = Convert.ToSingle(tr["HistoryPeRatio"].ToString());
                        
                        try
                        {
                            
                            StockInfoModel info = new StockInfoModel()
                            {
                                StockId = CompanyId,
                                StockName = CompanyName.Trim(),
                                StockType = CompanyType.Trim()

                            };
                            PeRatioModel model = new PeRatioModel()
                            {
                                StockInfo = info,
                                CurrentPeRatio = CurrentPeRatio,
                                HistoryPeRatio = HistoryPeRatio,
                            };
                            idNamePair.Add(model);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return idNamePair;
        }

        public List<SqlDailyTop20TradingModel> GetTop20TradingStock_Sql()
        {
            SqlControl sql = new SqlControl();
            
            SqlConnectionParameter para = new SqlConnectionParameter()
            {
                DataSource = "tcp:jakedatabase.database.windows.net,1433",
                InitialCatalog = "StockDB", //Y1H|U6Vt#DGW5ifa
                UserId = "Stock_Developer", //Stock_Developer
                Password = "Y1H|U6Vt#DGW5ifa" //Y1H|U6Vt#DGW5ifa
            };
            List<SqlDailyTop20TradingModel> top20ModelList = new List<SqlDailyTop20TradingModel>();
            string today = DateTime.Today.ToString("yyyy-MM-dd");
            string conditionString = $" Where tDate = '{today}'";

            try
            {
                string tblName = "dbo.SeasonProfitDetailTbl";

                string sqlString = @"SELECT [CompanyId]
                                  ,[CompanyName]
                                  ,[TransactionShares]
                                  ,[TransactionAmount]
                                  ,[OpeningPrice]
                                  ,[HighestPrice]
                                  ,[LowestPrice]
                                  ,[ClosingPrice]
                                  ,[UpsDowns]
                                  ,[PriceDifference]
                                  ,[LastRevealBuyPrice]
                                  ,[LastRevealSealPrice]
                                  ,[Rank]
                                    FROM[dbo].[DailyTop20TradingAmountTbl] "/* + conditionString*/;

                DataTable tb = sql.SqlSelect(sqlString, para);
                if (tb.Rows.Count > 0)
                {

                    foreach (DataRow tr in tb.Rows)
                    {
                        try
                        {
                            var CompanyId = tr["CompanyId"].ToString().Trim();
                            var CompanyName = tr["CompanyName"].ToString().Trim();
                            var TransactionShares = Convert.ToSingle(tr["TransactionShares"].ToString());
                            var TransactionAmount = Convert.ToSingle(tr["TransactionAmount"].ToString());
                            var OpeningPrice = Convert.ToSingle(tr["OpeningPrice"].ToString());
                            var HighestPrice = Convert.ToSingle(tr["HighestPrice"].ToString());
                            var LowestPrice = Convert.ToSingle(tr["LowestPrice"].ToString());
                            var ClosingPrice = Convert.ToSingle(tr["ClosingPrice"].ToString());
                            var PriceDifference = Convert.ToSingle(tr["PriceDifference"].ToString());
                            var LastRevealBuyPrice = Convert.ToSingle(tr["LastRevealBuyPrice"].ToString());
                            var LastRevealSealPrice = Convert.ToSingle(tr["LastRevealSealPrice"].ToString());
                            var upsDowns = tr["UpsDowns"].ToString().Trim();
                            var rank = Convert.ToInt32(tr["Rank"].ToString());
                            StockInfoModel info = new StockInfoModel()
                            {
                                StockId = CompanyId,
                                StockName = CompanyName.Trim(),
                            };
                            SqlDailyTop20TradingModel top20Model = new SqlDailyTop20TradingModel()
                            {
                                CompanyName = CompanyName,
                                CompanyId = CompanyId,
                                TransactionShares = TransactionShares,
                                TransactionAmount = TransactionAmount,
                                OpeningPrice = OpeningPrice,
                                HighestPrice = HighestPrice,
                                LowestPrice = LowestPrice,
                                ClosingPrice = ClosingPrice,
                                UpsDowns = upsDowns,
                                PriceDifference = PriceDifference,
                                LastRevealBuyPrice= LastRevealBuyPrice,
                                LastRevealSealPrice = LastRevealSealPrice,
                                Rank = rank
                            };
                            top20ModelList.Add(top20Model);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return top20ModelList;
        }


    }
}
