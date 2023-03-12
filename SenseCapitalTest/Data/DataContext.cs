
using jwtauth.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SenseCapitalTest.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Account> accounts { get; set; }
    }
}
