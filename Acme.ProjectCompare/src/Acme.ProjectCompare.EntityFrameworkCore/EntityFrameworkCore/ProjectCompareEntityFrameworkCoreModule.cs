using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Acme.ProjectCompare.EntityFrameworkCore
{
    [DependsOn(
        typeof(ProjectCompareDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class ProjectCompareEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<ProjectCompareDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });
        }
    }
}