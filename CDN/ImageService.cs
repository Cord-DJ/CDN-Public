using ImageMagick;

namespace Cord.CDN;

internal static class ImageService {
    public static readonly string[] Resources = new[] { "avatars", "banners", "icons", "wallpapers", "emoji", "stickers" };

    public static byte[] GetImageAsBytes(int? size, byte[] bytes) {
        // TODO: GIFs

        using var img = new MagickImage(bytes);

        if (size != null && size.Value != img.BaseWidth) {
            var geometry = new MagickGeometry(size.Value, 0);
            img.Resize(geometry);
        }

        return img.ToByteArray(MagickFormat.WebP);
    }
}
