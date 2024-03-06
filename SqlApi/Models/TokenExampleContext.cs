using Microsoft.EntityFrameworkCore;

namespace SqlApi.Models
{
    public class TokenExampleContext : DbContext
    {
        public TokenExampleContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserApi> Users { get; set; }
    }
}
