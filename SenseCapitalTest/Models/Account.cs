using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace jwtauth.Models
{
    [Index(nameof(Account.UserName), IsUnique = true)]
    public class Account
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Role Roles { get; set; } = Role.User;
    }
}
