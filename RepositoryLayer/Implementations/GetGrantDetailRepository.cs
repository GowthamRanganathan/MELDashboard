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
    public class GetGrantDetailRepository: BaseRepository, IGetGrantDetailsRepository
    {

        private readonly IOptions<Models.DbConnection> options;
        public GetGrantDetailRepository(IConfiguration config, IOptions<Models.DbConnection> options) : base(config)
        {
            this.options = options;
            SQL_Helper.SetConnectionString(this.options.Value.ConnectionString);
        }

        public List<GrantDetails> GetGrantsDetails()
        {
            List<GrantDetails> grants = new();
            try
            {
                DataTable dt = new DataTable();
                List<DbParameter> dbparamsGrantInfo = new List<DbParameter>();
                dt = SQL_Helper.ExecuteSelect<SqlConnection>("get_grant_Details", dbparamsGrantInfo, SQL_Helper.ExecutionType.Procedure);
                if (dt != null && dt.Rows.Count > 0)
                {
                    grants = SQL_Helper.ConvertDataTableToList<GrantDetails>(dt);
                }
            }
            catch (Exception ex)
            {

            }
            return grants;
        }

    }
}
