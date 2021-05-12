using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Acme.ProjectCompare
{
    [DependsOn(
        typeof(ProjectCompareApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class ProjectCompareHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "ProjectCompare";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(ProjectCompareApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
