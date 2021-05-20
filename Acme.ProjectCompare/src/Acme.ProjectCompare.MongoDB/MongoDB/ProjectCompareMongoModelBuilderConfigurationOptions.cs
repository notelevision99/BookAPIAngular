using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace Acme.ProjectCompare.MongoDB
{
    public class ProjectCompareMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public ProjectCompareMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}