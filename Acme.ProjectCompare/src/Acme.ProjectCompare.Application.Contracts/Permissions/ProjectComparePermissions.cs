using Volo.Abp.Reflection;

namespace Acme.ProjectCompare.Permissions
{
    public class ProjectComparePermissions
    {
        public const string GroupName = "ProjectCompare";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(ProjectComparePermissions));
        }
    }
}