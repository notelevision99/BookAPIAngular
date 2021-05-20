using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Acme.ProjectCompare.MongoDB
{
    [ConnectionStringName(ProjectCompareDbProperties.ConnectionStringName)]
    public class ProjectCompareMongoDbContext : AbpMongoDbContext, IProjectCompareMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureProjectCompare();
        }
    }
}