using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RepositoryLayer.Helper;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Models.Login;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryLayer.Implementations
{
    public class GetUserLoginDetailsRepository : BaseRepository, IGetUserLoginDetailsRepository
    {
        private readonly IOptions<Models.DbConnection> options;
        private ILogger<GetUserLoginDetailsRepository> _logger;
        public GetUserLoginDetailsRepository (IConfiguration config, IOptions<Models.DbConnection> options, ILogger<GetUserLoginDetailsRepository> logger) : base(config)
        {
            this.options = options;
            SQL_Helper.SetConnectionString(this.options.Value.ConnectionString);
            _logger = logger;
        }
        public async Task<UserCredentials> GetLoginDetails (LoginDetails userDetails)
        {

            UserCredentials logins = new UserCredentials();
            try
            {
                DataTable dt = new DataTable();
                List<DbParameter> dbparamsUserInfo = new List<DbParameter>();
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@email_Id", Value = userDetails.UserName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@user_Password", Value = userDetails.UserPassword, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dbparamsUserInfo.Add(new SqlParameter { ParameterName = "@querytype", Value = "Select", SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dt = SQL_Helper.ExecuteSelect<SqlConnection>("get_Login_Details", dbparamsUserInfo, SQL_Helper.ExecutionType.Procedure);
                if (dt != null && dt.Rows.Count > 0)
                {
                    logins =  SQL_Helper.ConvertDataTableToList<UserCredentials>(dt).FirstOrDefault(); ;
                }
                _logger.LogInformation("GetUserLoginDetailsRepository","GetLoginDetails");
            }
            catch(Exception ex)
            {
                _logger.LogError(0,ex, "GetLoginDetails");
            }
            return logins;
        }
    }
}
