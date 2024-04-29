namespace Cord.CDN.Domain.Resources; 

public sealed class Resource : ISnowflakeEntity {
    public ID Id { get; }
    public ID OwnerId { get; }
    public object[] PrimaryKeys => new object[] { Id };
}
