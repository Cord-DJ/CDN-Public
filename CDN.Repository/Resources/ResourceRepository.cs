using Cord.CDN.Domain.Resources;

namespace Cord.CDN.Repository.Resources; 

public sealed class ResourceRepository : Repository<Resource>, IResourceRepository {
    public ResourceRepository(MongoContext mongoContext) : base(null, mongoContext) {
        
    }
    
    public static void CreateMapping() {
        BsonClassMap.RegisterClassMap<Resource>(map => {
            map.MapIdProperty(x => x.Id);

            map.MapMember(x => x.OwnerId).SetIsRequired(true);
        });
    }
}
