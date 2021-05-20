using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Acme.ProjectCompare.MongoDB
{
    [ConnectionStringName(ProjectCompareDbProperties.ConnectionStringName)]
    public interface IProjectCompareMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
