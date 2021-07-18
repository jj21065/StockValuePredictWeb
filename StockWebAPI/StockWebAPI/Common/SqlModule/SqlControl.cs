using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebAPI.Common.SqlModule
{
    public class SqlControl
    {
        public string SqlCommand(string sqlString, SqlConnectionParameter para)
        {
            string errorMsg = string.Empty;
            SqlConnection sqlConnection = null;
            try
            {
                string tblName = "dbo.MonthRevenueTbl";
                string connectString = $"Data Source = {para.DataSource}; Initial Catalog = {para.InitialCatalog}; uid={para.UserId};pwd={para.Password};";
                sqlConnection = new SqlConnection(connectString);

                //開啟連線
                sqlConnection.Open();

                //建立要insert至資料庫的資料
                string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //將sql語法組成字串
                //sqlString = $@"insert into " + tblName + $@"(CompanyName, StockId, IndustryType,SourceDate, Revenue)
                //          values('台泥','100','','{dateTime}','100.0')";
                Console.WriteLine(sqlString);

                //執行sql語法
                SqlCommand command = new SqlCommand(sqlString, sqlConnection);

                //取回結果並顯示
                int result = command.ExecuteNonQuery();
                Console.WriteLine($"成功新增{result}筆資料!");

                //關閉連線
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                sqlConnection?.Close();
                errorMsg = ex.ToString();
                Console.WriteLine(errorMsg);
            }
            return errorMsg;
        }
      
        public DataTable SqlSelect(string sqlString, SqlConnectionParameter para)
        {
            string errorMsg = string.Empty;
            SqlConnection sqlConnection = null;
            
            string connectString = $"Data Source = {para.DataSource}; Initial Catalog = {para.InitialCatalog}; uid={para.UserId};pwd={para.Password};";
            sqlConnection = new SqlConnection(connectString);

            //開啟連線
            sqlConnection.Open();


            SqlCommand cmd = new SqlCommand(sqlString, sqlConnection);
     
            SqlDataReader mydr = cmd.ExecuteReader();
            DataTable tt = new DataTable();
            tt.Load(mydr);
            mydr.Close();
            cmd.Clone();
            sqlConnection.Close();
            return tt;
        }

   
    }

    public class SqlConnectionParameter
    {
        public string DataSource { get; set; }
        public string InitialCatalog { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }

    }
}
