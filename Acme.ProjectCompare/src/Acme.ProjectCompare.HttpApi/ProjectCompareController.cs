using Acme.ProjectCompare.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Acme.ProjectCompare
{
    public abstract class ProjectCompareController : AbpController
    {
        protected ProjectCompareController()
        {
            LocalizationResource = typeof(ProjectCompareResource);
        }
    }
}
