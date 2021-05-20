using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Acme.ProjectCompare.MongoDB
{
    public static class ProjectCompareMongoDbContextExtensions
    {
        public static void ConfigureProjectCompare(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new ProjectCompareMongoModelBuilderConfigurationOptions(
                ProjectCompareDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}