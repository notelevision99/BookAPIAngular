using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Acme.ProjectCompare
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(ProjectCompareDomainSharedModule)
    )]
    public class ProjectCompareDomainModule : AbpModule
    {

    }
}
