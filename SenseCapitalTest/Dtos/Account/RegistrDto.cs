using System.ComponentModel.DataAnnotations;

namespace SenseCapitalTest.Dtos.Account
{
    public class RegistrDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
