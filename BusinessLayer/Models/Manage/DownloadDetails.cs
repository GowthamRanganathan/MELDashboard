using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Manage
{
    public class DownloadDetails
    {
        public string template_name { get; set; }

        public string tab_name { get; set; }

        public string column_name { get; set; }

        public string grant_name { get; set; }

        public string op_ind_name { get; set; }
    }
}
