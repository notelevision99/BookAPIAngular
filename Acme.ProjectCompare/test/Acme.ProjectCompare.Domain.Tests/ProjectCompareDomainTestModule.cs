using Acme.ProjectCompare.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Acme.ProjectCompare
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(ProjectCompareEntityFrameworkCoreTestModule)
        )]
    public class ProjectCompareDomainTestModule : AbpModule
    {
        
    }
}
