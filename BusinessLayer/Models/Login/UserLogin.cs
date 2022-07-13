using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.Login
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Username is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
    }
}
