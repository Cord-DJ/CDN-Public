namespace Cord.CDN.Domain.Resources;

public sealed record ResourceAddedEvent(ID OwnerId, string Data);
