using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RepositoryLayer.Helper;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Models.Manage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Implementations
{
    public class LoadDownloadDetailRespository: BaseRepository, ILoadDownloadDetailRespository
    {
        private readonly IOptions<Models.DbConnection> options;
        public LoadDownloadDetailRespository(IConfiguration config, IOptions<Models.DbConnection> options) : base(config)
        {
            this.options = options;
            SQL_Helper.SetConnectionString(this.options.Value.ConnectionString);
        }

        public List<DownloadDetails> LoadDownloadData(string grantName)
        {
            List<DownloadDetails> downloadData = new();
            try
            {
                DataTable dt = new DataTable();
                List<DbParameter> dbparamsDownloadInfo = new List<DbParameter>();
                dbparamsDownloadInfo.Add(new SqlParameter { ParameterName = "@grant_name", Value = grantName, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input });
                dt = SQL_Helper.ExecuteSelect<SqlConnection>("get_tab_col_Details", dbparamsDownloadInfo, SQL_Helper.ExecutionType.Procedure);
                if (dt != null && dt.Rows.Count > 0)
                {
                    downloadData = SQL_Helper.ConvertDataTableToList<DownloadDetails>(dt);
                }
            }
            catch (Exception ex)
            {

            }
            return downloadData;
        }
    }
}
