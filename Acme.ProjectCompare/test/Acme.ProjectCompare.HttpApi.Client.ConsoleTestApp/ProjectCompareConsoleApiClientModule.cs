using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Acme.ProjectCompare
{
    [DependsOn(
        typeof(ProjectCompareHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class ProjectCompareConsoleApiClientModule : AbpModule
    {
        
    }
}
