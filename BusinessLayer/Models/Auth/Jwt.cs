using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Auth
{
    public class Jwt
    {
        public string Key { get; set; }
        public int accessTimeOut { get; set; }
        public int refershTimeOut { get; set; }
    }
}
