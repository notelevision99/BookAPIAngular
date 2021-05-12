using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace Acme.ProjectCompare.Blazor.WebAssembly
{
    [DependsOn(
        typeof(ProjectCompareBlazorModule),
        typeof(ProjectCompareHttpApiClientModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
        )]
    public class ProjectCompareBlazorWebAssemblyModule : AbpModule
    {
        
    }
}