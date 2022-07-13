using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Models.Manage
{
    public class GrantDetails
    {
        public string grant_id { get; set; }
        public string grant_name { get; set; }
        public DateTime update_date { get; set; }
        public int grant_count { get; set; }
        public int opindcount { get; set; }
    }
}
