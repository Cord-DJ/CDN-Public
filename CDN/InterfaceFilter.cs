using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cord.CDN;

public class InterfaceContractResolver : DefaultContractResolver {
    public InterfaceContractResolver() {
        
    }

    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization) {
        var face = type.GetInterfaces().FirstOrDefault();
        IList<JsonProperty> properties = base.CreateProperties(face ?? type, memberSerialization);

        foreach (var p in properties) {
            p.PropertyName = p.PropertyName?.ToLowerFirst();
        }

        return properties;
    }
}
