using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebAPI.Common
{
    public class MSSQLModule
    {
        public string SqlCommand(string sqlString)
        {
            string errorMsg = string.Empty;
            try
            {
                string tblName = "dbo.MothRevenueTbl";
                string connectString = "Data Source = localhost; Initial Catalog = StockDB;";
                SqlConnection sqlConnection = new SqlConnection(connectString);

                //開啟連線
                sqlConnection.Open();

                //建立要insert至資料庫的資料
                string dateTime = DateTime.Now.ToString("YYYY-MM-dd HH:mm:ss");
                //將sql語法組成字串
                sqlString = $@"insert into " + tblName + $@"(CompanyName, StockId, IndustryType,SourceDate, Revenue)
                          values('台泥','100','','{dateTime}','100.0')";
                Console.WriteLine(sqlString);

                //執行sql語法
                SqlCommand command = new SqlCommand(sqlString, sqlConnection);

                //取回結果並顯示
                int result = command.ExecuteNonQuery();
                Console.WriteLine($"成功新增{result}筆資料!");
                Console.ReadLine();

                //關閉連線
                sqlConnection.Close();
            }catch(Exception ex)
            {
                errorMsg = ex.ToString();
                Console.WriteLine(errorMsg);
            }
            return errorMsg;
        }
    }
    
}
