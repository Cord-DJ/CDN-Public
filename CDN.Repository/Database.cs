using Cord.CDN.Domain.Resources;
using Cord.CDN.Repository.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Conventions;

namespace Cord.CDN.Repository;

public static class Database {
    public static void InitDatabase(this WebApplicationBuilder builder) {
        var conventionPack = new ConventionPack {
            new CamelCaseElementNameConvention()
        };
        ConventionRegistry.Register("camelCase", conventionPack, _ => true);

        InitMappings();
        
        var options = builder.Configuration.GetSection(MongoDbOptions.Section).Get<MongoDbOptions>();
        var ctx = new MongoContext(
            options.Hostname,
            options.Collection
        );

        builder.Services.AddSingleton(ctx);

        builder.Services.Chain<IResourceRepository>()
            // .Add<UserCacheRepository>()
            .Add<ResourceRepository>()
            .Configure();
    }

    static void InitMappings() {
        ResourceRepository.CreateMapping();
    }
}
