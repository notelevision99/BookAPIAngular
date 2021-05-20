using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Acme.ProjectCompare
{
    [DependsOn(
        typeof(ProjectCompareDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class ProjectCompareApplicationContractsModule : AbpModule
    {

    }
}
