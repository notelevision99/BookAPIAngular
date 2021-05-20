using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace Acme.ProjectCompare.Blazor.Server
{
    [DependsOn(
        typeof(AbpAspNetCoreComponentsServerThemingModule),
        typeof(ProjectCompareBlazorModule)
        )]
    public class ProjectCompareBlazorServerModule : AbpModule
    {
        
    }
}