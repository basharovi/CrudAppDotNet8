using CrudAppDotNet8.Helpers;

namespace CrudAPI.Tests.Unit.Services;

[TestFixture]
internal sealed class UrlBuilderTester
{
    private const string BaseUrl = "https://example.com";

    [TestCase(false, ExpectedResult = "https://example.com/reset-password")]
    [TestCase(true, ExpectedResult = "https://example.com/reset-password?sendEmail=true")]
    public string ResetPassword(bool sendEmail) => new UrlBuilder(BaseUrl).ResetPassword(sendEmail).ToString();


    [TestCase(null, ExpectedResult = "https://example.com/verify-user")]
    [TestCase("", ExpectedResult = "https://example.com/verify-user")]
    [TestCase("rmv", ExpectedResult = "https://example.com/web/rmv/verify-user")]
    [TestCase(" ", ExpectedResult = "https://example.com/verify-user")]
    [TestCase("user101", ExpectedResult = "https://example.com/web/user101/verify-user")]
    public string VerifyUser(string? webUrlSpecifier) => new UrlBuilder(BaseUrl).VerifyUser(webUrlSpecifier).ToString();
}
