using Microsoft.EntityFrameworkCore;

namespace SqlApi.Models
{
    public class UserContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public UserContext(DbContextOptions<UserContext> options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserWithRoles>().HasKey(ba => new { ba.USER_ID, ba.MODULE_INCKEY });
        }
        public Microsoft.EntityFrameworkCore.DbSet<Log> TBL_USER_LOG { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<User> TBL_USER_DATA { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Roles> TBL_ROLES { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<UserWithRoles> VW_USER_WITH_ROLES { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Module> TBL_MODULES { get; set; }
    }
}
