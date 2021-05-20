using Localization.Resources.AbpUi;
using Acme.ProjectCompare.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Acme.ProjectCompare
{
    [DependsOn(
        typeof(ProjectCompareApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class ProjectCompareHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(ProjectCompareHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<ProjectCompareResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
