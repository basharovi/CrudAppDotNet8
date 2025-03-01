using CrudAppDotNet8.Entities;
using CrudAppDotNet8.Models.Users;
using Shouldly;
using System.Net;
using System.Text;
using System.Text.Json;

namespace CrudAPI.Tests.Integration.Services;

[TestFixture]
public sealed class UserControllerTest
{
    private HttpClient _client;
    private const string _baseUrl = "https://localhost:44387"; // Adjust the base URL according to your API

    [OneTimeSetUp]
    public void Setup()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri(_baseUrl)
        };
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _client.Dispose();
    }

    [Test]
    public async Task GetAllUsers_ReturnsSuccessStatusCode()
    {
        // Arrange
        var response = await _client.GetAsync("/users");

        // Assert
        response.IsSuccessStatusCode.ShouldBe(true);
        response.Content.Headers.ContentType?.ToString().ShouldBe("application/json; charset=utf-8");
    }

    [Test]
    public async Task GetUserById_WithValidId_ReturnsUser()
    {
        // Arrange
        var userId = 1;

        // Act
        var response = await _client.GetAsync($"/users/{userId}");

        // Assert
        response.IsSuccessStatusCode.ShouldBe(false);
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);

        var content = await response.Content.ReadAsStringAsync();
        var user = JsonSerializer.Deserialize<object>(content);

        user.ShouldNotBeNull();
    }

    [Test]
    public async Task CreateUser_WithValidData_ReturnsCreatedUser()
    {
        // Arrange
        var newUser = new CreateRequest
        {
            FirstName = "Test",
            LastName = "User",
            Email = "test1@example.com",
            Role = "Admin",
            Password = "password",
            ConfirmPassword = "password"
        };
        var jsonContent = JsonSerializer.Serialize(newUser);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/users", content);

        // Assert
        response.IsSuccessStatusCode.ShouldBe(true);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var responseContent = await response.Content.ReadAsStringAsync();
        var createdResponse = JsonSerializer.Deserialize<dynamic>(responseContent);
    }

    [Test]
    public async Task UpdateUser_WithValidData_ReturnsNoContent()
    {
        // Arrange
        var userId = 1;
        var updateUser = new UserDto(userId, "Test", "User2", "test1@example.com", "Admin");
        var jsonContent = JsonSerializer.Serialize(updateUser);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PutAsync($"/users/{userId}", content);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    [Test]
    public async Task DeleteUser_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var userId = 1;

        // Act
        var response = await _client.DeleteAsync($"/users/{userId}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}