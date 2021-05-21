using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Acme.BookStore
{
    [DependsOn(
        typeof(BookStoreDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(BookStoreApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(AbpSettingManagementApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpAutofacModule)
        )]
    public class BookStoreApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<BookStoreApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<BookStoreApplicationModule>();
            });
        }
    }
}
