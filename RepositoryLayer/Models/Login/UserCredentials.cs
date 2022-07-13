using System;

namespace RepositoryLayer.Models.Login
{
    public class UserCredentials
    {
        public string refresh_token { get; set; }
        public DateTime token_expiry_ts { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string location { get; set; }
        public string Organization { get; set; }
        public int User_Id { get; set; }
        public string email_Id { get; set; }

    }

    public class LoginDetails
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
    }
}
