

using System.ComponentModel.DataAnnotations;

namespace Catalog.DTO
{
    public class LoginAccountDTO
    {
        [Required(ErrorMessage = "Login missing")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password missing")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}