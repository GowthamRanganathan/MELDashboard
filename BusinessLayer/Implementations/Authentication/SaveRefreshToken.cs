using BusinessLayer.Interfaces.Authentication;
using RepositoryLayer.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementations.Authentication
{
    public class SaveRefreshToken : ISaveRefreshToken
    {
        public int SaveRefresh(string emailid, string RefreshToken, DateTime RefreshTokenExpiryTime)
        {
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            List<DbParameter> TokenInfo = new List<DbParameter>();
            TokenInfo.Add(new SqlParameter { ParameterName = "@email_Id", Value = "Sathiyendran.Ganesan@disys.com", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            TokenInfo.Add(new SqlParameter { ParameterName = "@refreshtoken", Value = RefreshToken, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            TokenInfo.Add(new SqlParameter { ParameterName = "@tokenexpiryts", Value = RefreshTokenExpiryTime, SqlDbType = SqlDbType.DateTime, Direction = ParameterDirection.Input });
            TokenInfo.Add(new SqlParameter { ParameterName = "@querytype", Value = "Update", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
            result = SQL_Helper.ExecuteNonQuery<SqlConnection>("get_Login_Details", TokenInfo, SQL_Helper.ExecutionType.Procedure);
            int insertRowsCount = result["RowsAffected"];
            return insertRowsCount;
        }
    }
}
