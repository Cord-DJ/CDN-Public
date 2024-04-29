namespace Cord.CDN;

public static class ResourceManager {
    public static async Task<string> SaveResource(string code, string directory) {
        var root = Path.Combine(Directory.GetCurrentDirectory(), "../resources", directory);

        var hash = StringHelper.GenerateHash(32);
        var fullPath = Path.Combine(root, $"{hash}.webp");

        var parts = code.Split(',');
        if (parts.Length != 2 || parts[0] != "data:image/webp;base64") {
            throw new BadRequestException("format");
        }

        await File.WriteAllBytesAsync(fullPath, Convert.FromBase64String(parts[1]));
        return hash;
    }
}
