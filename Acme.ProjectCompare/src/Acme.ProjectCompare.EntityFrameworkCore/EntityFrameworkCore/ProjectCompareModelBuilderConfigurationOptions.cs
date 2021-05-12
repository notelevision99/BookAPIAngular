using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Acme.ProjectCompare.EntityFrameworkCore
{
    public class ProjectCompareModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public ProjectCompareModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}