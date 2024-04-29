using System.ComponentModel;
using System.Dynamic;
using System.Security.Cryptography;
using System.Text;
using Pluralize.NET.Core;

namespace Cord.CDN;

public static class StringExtensions {
    public static string ToLowerFirst(this string str) {
        return Char.ToLowerInvariant(str[0]) + str.Substring(1);
    }

    public static string ToUpperFirst(this string str) {
        return Char.ToUpperInvariant(str[0]) + str.Substring(1);
    }

    public static string ToPlural(this string str) {
        return new Pluralizer().Pluralize(str);
    }

    public static string ToSingular(this string str) {
        return new Pluralizer().Singularize(str);
    }
}


public static class ObjectExtensions {
    public static dynamic ToDynamic(this object value) {
        IDictionary<string, object> expando = new ExpandoObject()!;

        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
            expando.Add(property.Name, property.GetValue(value)!);

        return expando as ExpandoObject;
    }
}

public static class StringHelper {
    public static string GenerateHash(int length) {
        const string chrs = "abcdefghijkmnopqrstuvwxyz0123456789";
        var ret  = new char[length];
        var rand = new Random();

        for (var i = 0; i < length; i++) {
            ret[i] = chrs[rand.Next(0, chrs.Length)];
        }

        return new string(ret);
    }

    public static string HashPassword(string password) {
        using var md5 = MD5.Create();

        var data = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
        return Convert.ToBase64String(data);
    }

    public static int? TryInt(this string str) {
        return int.TryParse(str, out int result) ? result : null;
    }

    public static string MaxLength(this string str, int len) {
        return (str.Length > len ? str.Substring(0, len) : str).Trim();
    }
}
