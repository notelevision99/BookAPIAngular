using Acme.ProjectCompare.Model;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;

namespace Acme.ProjectCompare.EntityFrameworkCore
{
    [ConnectionStringName(ProjectCompareDbProperties.ConnectionStringName)]
    public interface IProjectCompareDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
        * DbSet<Question> Questions { get; }
        */
        DbSet<Book> Books { get; set; }
        DbSet<IdentityUser> Users { get; }
        DbSet<IdentityUserRole> UserRoles { get; }
        DbSet<IdentityUserClaim> UserClaims { get; }
        DbSet<IdentityUserLogin> UserLogins { get; }
        DbSet<IdentityUserToken> UserTokens { get; }
        DbSet<IdentityRole> Roles { get; }
        DbSet<IdentityRoleClaim> RoleClaims { get; }
        DbSet<IdentityClaimType> ClaimTypes { get; }
        DbSet<IdentityUserOrganizationUnit> OrganizationUnits { get; }
    }
}