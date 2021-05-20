using Acme.ProjectCompare.Localization;
using Volo.Abp.Application.Services;

namespace Acme.ProjectCompare
{
    public abstract class ProjectCompareAppService : ApplicationService
    {
        protected ProjectCompareAppService()
        {
            LocalizationResource = typeof(ProjectCompareResource);
            ObjectMapperContext = typeof(ProjectCompareApplicationModule);
        }
    }
}
