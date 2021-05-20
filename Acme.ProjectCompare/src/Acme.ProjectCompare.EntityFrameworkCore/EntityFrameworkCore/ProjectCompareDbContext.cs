using Acme.ProjectCompare.Model;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;

    namespace Acme.ProjectCompare.EntityFrameworkCore
    
    {
        [ConnectionStringName(ProjectCompareDbProperties.ConnectionStringName)]
        public class ProjectCompareDbContext : AbpDbContext<ProjectCompareDbContext> , IProjectCompareDbContext
        {
            /* Add DbSet for each Aggregate Root here. Example:
            * public DbSet<Question> Questions { get; set; }
            */

            public ProjectCompareDbContext(DbContextOptions<ProjectCompareDbContext> options)
                : base(options)
            {

            }
            public virtual DbSet<Book> Books { get; set; }
            public virtual DbSet<IdentityUser> Users { get;  }
            public virtual DbSet<IdentityUserRole> UserRoles { get; }
            public virtual DbSet<IdentityUserClaim> UserClaims { get; }
            public virtual DbSet<IdentityUserLogin> UserLogins { get; }
            public virtual DbSet<IdentityUserToken> UserTokens { get; }
            public virtual DbSet<IdentityRole> Roles { get; }
            public virtual DbSet<IdentityRoleClaim> RoleClaims { get; }
            public virtual DbSet<IdentityClaimType> ClaimTypes { get; }
            public virtual DbSet<IdentityUserOrganizationUnit> OrganizationUnits { get; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureProjectCompare();

            builder.Entity<IdentityUserRole>()
            .HasKey(r => new { r.UserId, r.RoleId });
            builder.Entity<IdentityUserLogin>()
            .HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId });
            builder.Entity<IdentityUserOrganizationUnit>().HasKey(l => new { l.UserId, l.OrganizationUnitId });
            builder.Entity<IdentityUserToken>()
            .HasKey(l => new { l.LoginProvider, l.Name, l.UserId });
            builder.Entity<IdentityUserOrganizationUnit>().HasKey(l => new { l.UserId, l.OrganizationUnitId });
        }
    }
}