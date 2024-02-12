using System.Collections.Specialized;
using System.Web;

namespace EveryExtension;

/// <summary>
/// Provides extension methods for working with Uri.
/// </summary>
public static class UriExtensions
{
    /// <summary>
    /// Combines a base Uri with a relative Uri.
    /// </summary>
    /// <param name="baseUri">The base Uri.</param>
    /// <param name="relativeUri">The relative Uri to combine.</param>
    /// <returns>The combined Uri.</returns>
    public static Uri CombineWith(this Uri baseUri, string relativeUri)
    {
        var relativeUriObject = new Uri(relativeUri, UriKind.RelativeOrAbsolute);
        return new Uri(baseUri, relativeUriObject);
    }

    /// <summary>
    /// Checks if the Uri has an HTTP or HTTPS scheme.
    /// </summary>
    /// <param name="uri">The Uri to check.</param>
    /// <returns>True if the scheme is HTTP or HTTPS; otherwise, false.</returns>
    public static bool IsHttpOrHttps(this Uri uri)
    {
        return uri.Scheme.Equals("http", StringComparison.OrdinalIgnoreCase) ||
               uri.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Gets the base URL of the Uri.
    /// </summary>
    /// <param name="uri">The Uri.</param>
    /// <returns>The base URL of the Uri.</returns>
    public static Uri GetBaseUrl(this Uri uri)
        => new(uri.GetLeftPart(UriPartial.Authority));

    /// <summary>
    /// Appends query string parameters to the Uri.
    /// </summary>
    /// <param name="uri">The Uri to append to.</param>
    /// <param name="parameters">The query string parameters.</param>
    /// <returns>The Uri with appended query string parameters.</returns>
    public static Uri AppendQueryString(this Uri uri, NameValueCollection parameters)
    {
        var uriBuilder = new UriBuilder(uri);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query.Add(parameters);
        uriBuilder.Query = query.ToString();
        return uriBuilder.Uri;
    }

    /// <summary>
    /// Adds or updates a query parameter in the Uri.
    /// </summary>
    /// <param name="uri">The Uri to modify.</param>
    /// <param name="key">The key of the query parameter.</param>
    /// <param name="value">The value of the query parameter.</param>
    /// <returns>The modified Uri.</returns>
    public static Uri AddOrUpdateQueryParameter(this Uri uri, string key, string value)
    {
        var uriBuilder = new UriBuilder(uri);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query[key] = value;
        uriBuilder.Query = query.ToString();
        return uriBuilder.Uri;
    }

    /// <summary>
    /// Removes a query parameter from the Uri.
    /// </summary>
    /// <param name="uri">The Uri to modify.</param>
    /// <param name="parameterName">The name of the query parameter to remove.</param>
    /// <returns>The modified Uri.</returns>
    public static Uri RemoveQueryParameter(this Uri uri, string parameterName)
    {
        var uriBuilder = new UriBuilder(uri);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query.Remove(parameterName);
        uriBuilder.Query = query.ToString();
        return uriBuilder.Uri;
    }

    /// <summary>
    /// Gets the value of a query parameter from the Uri.
    /// </summary>
    /// <param name="uri">The Uri.</param>
    /// <param name="parameterName">The name of the query parameter.</param>
    /// <returns>The value of the query parameter, or null if not found.</returns>
    public static string? GetQueryParameter(this Uri uri, string parameterName)
    {
        var query = HttpUtility.ParseQueryString(uri.Query);
        return query.Get(parameterName);
    }

    /// <summary>
    /// Converts the Uri to use the HTTPS scheme.
    /// </summary>
    /// <param name="uri">The Uri.</param>
    /// <returns>The Uri with the HTTPS scheme.</returns>
    public static Uri ToUriWithHttps(this Uri uri)
    {
        var uriBuilder = new UriBuilder(uri)
        {
            Scheme = Uri.UriSchemeHttps
        };
        return uriBuilder.Uri;
    }

    /// <summary>
    /// Converts the Uri to use the FTP scheme.
    /// </summary>
    /// <param name="uri">The Uri.</param>
    /// <returns>The Uri with the FTP scheme.</returns>
    public static Uri ToUriWithFtp(this Uri uri)
    {
        var uriBuilder = new UriBuilder(uri)
        {
            Scheme = Uri.UriSchemeFtp
        };
        return uriBuilder.Uri;
    }

    /// <summary>
    /// Converts the Uri to use the FTPS scheme.
    /// </summary>
    /// <param name="uri">The Uri.</param>
    /// <returns>The Uri with the FTPS scheme.</returns>
    public static Uri ToUriWithFtps(this Uri uri)
    {
        var uriBuilder = new UriBuilder(uri)
        {
            Scheme = Uri.UriSchemeFtps
        };
        return uriBuilder.Uri;
    }

    /// <summary>
    /// Ensures that the Uri has a trailing slash.
    /// </summary>
    /// <param name="uri">The Uri to modify.</param>
    /// <returns>The modified Uri with a trailing slash.</returns>
    public static Uri EnsureTrailingSlash(this Uri uri)
        => !uri.OriginalString.EndsWith("/") ? new Uri($"{uri.OriginalString}/") : uri;

    /// <summary>
    /// Ensures that the Uri does not have a trailing slash.
    /// </summary>
    /// <param name="uri">The Uri to modify.</param>
    /// <returns>The modified Uri without a trailing slash.</returns>
    public static Uri EnsureNoTrailingSlash(this Uri uri)
    {
        if (uri.OriginalString.EndsWith("/"))
            return new Uri(uri.OriginalString.TrimEnd('/'));
        return uri;
    }

    /// <summary>
    /// Checks if the Uri is a subpath of another Uri.
    /// </summary>
    /// <param name="uri">The Uri to check.</param>
    /// <param name="baseUri">The base Uri.</param>
    /// <returns>True if the Uri is a subpath of the base Uri; otherwise, false.</returns>
    public static bool IsSubPathOf(this Uri uri, Uri baseUri)
        => uri.OriginalString.StartsWith(baseUri.OriginalString, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Combines paths with the Uri.
    /// </summary>
    /// <param name="uri">The Uri to combine with paths.</param>
    /// <param name="paths">The paths to combine.</param>
    /// <returns>The combined Uri with paths.</returns>
    public static Uri CombinePaths(this Uri uri, params string[] paths)
    {
        var combinedPath = paths.Aggregate(uri.AbsolutePath, (current, path) => current.TrimEnd('/') + "/" + path.TrimStart('/'));
        return new Uri($"{uri.GetLeftPart(UriPartial.Authority)}{combinedPath}{uri.Query}{uri.Fragment}");
    }

    /// <summary>
    /// Gets the relative Uri between two Uris.
    /// </summary>
    /// <param name="baseUri">The base Uri.</param>
    /// <param name="relativeUri">The relative Uri to get.</param>
    /// <returns>The relative Uri.</returns>
    public static Uri GetRelativeUri(this Uri baseUri, Uri relativeUri)
    {
        var relative = baseUri.MakeRelativeUri(relativeUri);
        return new Uri(relative.ToString(), UriKind.Relative);
    }

    /// <summary>
    /// Ensures that the Uri has a specific scheme.
    /// </summary>
    /// <param name="uri">The Uri to modify.</param>
    /// <param name="scheme">The desired scheme.</param>
    /// <returns>The modified Uri with the specified scheme.</returns>
    public static Uri EnsureScheme(this Uri uri, string scheme)
    {
        if (!uri.Scheme.Equals(scheme, StringComparison.OrdinalIgnoreCase))
            return new UriBuilder(uri) { Scheme = scheme }.Uri;
        return uri;
    }

    /// <summary>
    /// Gets the parameters from the Uri's query string.
    /// </summary>
    /// <param name="uri">The Uri with a query string.</param>
    /// <returns>The parameters from the query string.</returns>
    public static NameValueCollection GetUriParameters(this Uri uri)
        => HttpUtility.ParseQueryString(uri.Query);

    /// <summary>
    /// Checks if the Uri is the base of another Uri.
    /// </summary>
    /// <param name="baseUri">The base Uri.</param>
    /// <param name="uri">The Uri to check.</param>
    /// <returns>True if the Uri is the base of the other Uri; otherwise, false.</returns>
    public static bool IsBaseOf(this Uri baseUri, Uri uri)
        => Uri.IsWellFormedUriString(uri.OriginalString, UriKind.Absolute) &&
               uri.OriginalString.StartsWith(baseUri.OriginalString, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Gets the segments of the Uri's path.
    /// </summary>
    /// <param name="uri">The Uri with a path.</param>
    /// <returns>The segments of the path.</returns>
    public static string[] GetPathSegments(this Uri uri)
        => uri.AbsolutePath.Trim('/').Split('/');

    /// <summary>
    /// Decodes the URL-encoded characters in the Uri.
    /// </summary>
    /// <param name="uri">The Uri to decode.</param>
    /// <returns>The decoded Uri.</returns>
    public static Uri DecodeUrl(this Uri uri)
    {
        var decodedUrl = Uri.UnescapeDataString(uri.OriginalString);
        return new Uri(decodedUrl);
    }

    /// <summary>
    /// Checks if two Uris have the same origin.
    /// </summary>
    /// <param name="uri1">The first Uri.</param>
    /// <param name="uri2">The second Uri.</param>
    /// <returns>True if the Uris have the same origin; otherwise, false.</returns>
    public static bool HasSameOrigin(this Uri uri1, Uri uri2)
        => uri1.Scheme == uri2.Scheme && uri1.Host == uri2.Host && uri1.Port == uri2.Port;

    /// <summary>
    /// Removes the fragment part from the Uri.
    /// </summary>
    /// <param name="uri">The Uri to modify.</param>
    /// <returns>The modified Uri without the fragment part.</returns>
    public static Uri RemoveFragment(this Uri uri)
        => new UriBuilder(uri) { Fragment = null }.Uri;

    /// <summary>
    /// Gets the root domain from the Uri's host.
    /// </summary>
    /// <param name="uri">The Uri with a host.</param>
    /// <returns>The root domain.</returns>
    public static string GetRootDomain(this Uri uri)
    {
        var segments = uri.Host.Split('.');
        return string.Join('.', segments.Skip(Math.Max(0, segments.Length - 2)));
    }

    /// <summary>
    /// Gets the port from the Uri or a default port if the Uri is using the default port.
    /// </summary>
    /// <param name="uri">The Uri with or without a port.</param>
    /// <param name="defaultPort">The default port to use if the Uri is using the default port.</param>
    /// <returns>The port from the Uri or the default port.</returns>
    public static int GetPortOrDefault(this Uri uri, int defaultPort)
        => uri.IsDefaultPort ? defaultPort : uri.Port;

    /// <summary>
    /// Checks if the Uri is using a secure connection (HTTPS).
    /// </summary>
    /// <param name="uri">The Uri to check.</param>
    /// <returns>True if the Uri is using a secure connection; otherwise, false.</returns>
    public static bool IsSecureConnection(this Uri uri)
        => uri.Scheme.Equals(Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Checks if the Uri is using the WebSocket scheme (ws or wss).
    /// </summary>
    /// <param name="uri">The Uri to check.</param>
    /// <returns>True if the Uri is using the WebSocket scheme; otherwise, false.</returns>
    public static bool IsWebSocketScheme(this Uri uri)
    {
        return uri.Scheme.Equals("ws", StringComparison.OrdinalIgnoreCase) ||
               uri.Scheme.Equals("wss", StringComparison.OrdinalIgnoreCase);
    }
}
