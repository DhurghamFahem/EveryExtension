using System.Collections.Specialized;
using System.Web;

namespace EveryExtension;

public static class UriExtensions
{
    public static Uri CombineWith(this Uri baseUri, string relativeUri)
    {
        var relativeUriObject = new Uri(relativeUri, UriKind.RelativeOrAbsolute);
        return new Uri(baseUri, relativeUriObject);
    }

    public static bool IsHttpOrHttps(this Uri uri)
    {
        return uri.Scheme.Equals("http", StringComparison.OrdinalIgnoreCase) ||
               uri.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase);
    }

    public static Uri GetBaseUrl(this Uri uri)
        => new(uri.GetLeftPart(UriPartial.Authority));

    public static Uri AppendQueryString(this Uri uri, NameValueCollection parameters)
    {
        var uriBuilder = new UriBuilder(uri);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query.Add(parameters);
        uriBuilder.Query = query.ToString();
        return uriBuilder.Uri;
    }

    public static Uri AddOrUpdateQueryParameter(this Uri uri, string key, string value)
    {
        var uriBuilder = new UriBuilder(uri);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query[key] = value;
        uriBuilder.Query = query.ToString();
        return uriBuilder.Uri;
    }

    public static Uri RemoveQueryParameter(this Uri uri, string parameterName)
    {
        var uriBuilder = new UriBuilder(uri);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query.Remove(parameterName);
        uriBuilder.Query = query.ToString();
        return uriBuilder.Uri;
    }

    public static string? GetQueryParameter(this Uri uri, string parameterName)
    {
        var query = HttpUtility.ParseQueryString(uri.Query);
        return query.Get(parameterName);
    }

    public static Uri ToUriWithHttps(this Uri uri)
    {
        var uriBuilder = new UriBuilder(uri)
        {
            Scheme = Uri.UriSchemeHttps
        };
        return uriBuilder.Uri;
    }

    public static Uri ToUriWithFtp(this Uri uri)
    {
        var uriBuilder = new UriBuilder(uri)
        {
            Scheme = Uri.UriSchemeFtp
        };
        return uriBuilder.Uri;
    }

    public static Uri ToUriWithFtps(this Uri uri)
    {
        var uriBuilder = new UriBuilder(uri)
        {
            Scheme = Uri.UriSchemeFtps
        };
        return uriBuilder.Uri;
    }

    public static Uri EnsureTrailingSlash(this Uri uri)
        => !uri.OriginalString.EndsWith("/") ? new Uri($"{uri.OriginalString}/") : uri;

    public static Uri EnsureNoTrailingSlash(this Uri uri)
    {
        if (uri.OriginalString.EndsWith("/"))
            return new Uri(uri.OriginalString.TrimEnd('/'));
        return uri;
    }

    public static bool IsSubPathOf(this Uri uri, Uri baseUri)
        => uri.OriginalString.StartsWith(baseUri.OriginalString, StringComparison.OrdinalIgnoreCase);

    public static Uri CombinePaths(this Uri uri, params string[] paths)
    {
        var combinedPath = paths.Aggregate(uri.AbsolutePath, (current, path) => current.TrimEnd('/') + "/" + path.TrimStart('/'));
        return new Uri($"{uri.GetLeftPart(UriPartial.Authority)}{combinedPath}{uri.Query}{uri.Fragment}");
    }

    public static Uri GetRelativeUri(this Uri baseUri, Uri relativeUri)
    {
        var relative = baseUri.MakeRelativeUri(relativeUri);
        return new Uri(relative.ToString(), UriKind.Relative);
    }

    public static Uri EnsureScheme(this Uri uri, string scheme)
    {
        if (!uri.Scheme.Equals(scheme, StringComparison.OrdinalIgnoreCase))
            return new UriBuilder(uri) { Scheme = scheme }.Uri;
        return uri;
    }

    public static NameValueCollection GetUriParameters(this Uri uri)
        => HttpUtility.ParseQueryString(uri.Query);

    public static bool IsBaseOf(this Uri baseUri, Uri uri)
        => Uri.IsWellFormedUriString(uri.OriginalString, UriKind.Absolute) &&
               uri.OriginalString.StartsWith(baseUri.OriginalString, StringComparison.OrdinalIgnoreCase);

    public static string[] GetPathSegments(this Uri uri)
        => uri.AbsolutePath.Trim('/').Split('/');

    public static Uri DecodeUrl(this Uri uri)
    {
        var decodedUrl = Uri.UnescapeDataString(uri.OriginalString);
        return new Uri(decodedUrl);
    }

    public static bool HasSameOrigin(this Uri uri1, Uri uri2)
        => uri1.Scheme == uri2.Scheme && uri1.Host == uri2.Host && uri1.Port == uri2.Port;

    public static Uri RemoveFragment(this Uri uri)
        => new UriBuilder(uri) { Fragment = null }.Uri;

    public static string GetRootDomain(this Uri uri)
    {
        var segments = uri.Host.Split('.');
        return string.Join('.', segments.Skip(Math.Max(0, segments.Length - 2)));
    }

    public static int GetPortOrDefault(this Uri uri, int defaultPort)
        => uri.IsDefaultPort ? defaultPort : uri.Port;

    public static bool IsSecureConnection(this Uri uri)
        => uri.Scheme.Equals(Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase);

    public static bool IsWebSocketScheme(this Uri uri)
    {
        return uri.Scheme.Equals("ws", StringComparison.OrdinalIgnoreCase) ||
               uri.Scheme.Equals("wss", StringComparison.OrdinalIgnoreCase);
    }
}
