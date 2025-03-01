namespace CrudAppDotNet8.Helpers;

public sealed class UrlBuilder(Flurl.Url baseUrl)
{
    public Uri ResetPassword(bool sendEmail)
    {
        var url = "reset-password";

        if (sendEmail)
            url += "?sendEmail=true";

        return baseUrl.AppendPathSegment(url).ToUri();
    }

    public Uri VerifyUser(string? webUrlSpecifier)
    {
        if (!string.IsNullOrWhiteSpace(webUrlSpecifier))
            baseUrl.AppendPathSegments("web", webUrlSpecifier);

        return baseUrl.AppendPathSegment("verify-user").ToUri();
    }
}
