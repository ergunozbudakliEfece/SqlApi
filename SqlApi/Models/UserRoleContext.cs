using Microsoft.EntityFrameworkCore;

namespace SqlApi.Models
{
    public class UserRoleContext: DbContext
    {
        public UserRoleContext(DbContextOptions<UserRoleContext> options)
          : base(options)
        {

        }
        public DbSet<UserRole> TBL_USER_AUTH { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasNoKey();
        }

        public DbSet<UserWithRoles> VW_USER_WITH_ROLES { get; set; }

    }
}
