using Acme.ProjectCompare.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Acme.ProjectCompare.Permissions
{
    public class ProjectComparePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(ProjectComparePermissions.GroupName, L("Permission:ProjectCompare"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ProjectCompareResource>(name);
        }
    }
}