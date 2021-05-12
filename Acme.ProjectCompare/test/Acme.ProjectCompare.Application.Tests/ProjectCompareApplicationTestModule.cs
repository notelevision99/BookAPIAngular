using Volo.Abp.Modularity;

namespace Acme.ProjectCompare
{
    [DependsOn(
        typeof(ProjectCompareApplicationModule),
        typeof(ProjectCompareDomainTestModule)
        )]
    public class ProjectCompareApplicationTestModule : AbpModule
    {

    }
}
