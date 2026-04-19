using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApplication1.Tests;

public class UsersControllerEmailValidationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UsersControllerEmailValidationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    // ── POST /api/users ──────────────────────────────────────────────────────

    [Fact]
    public async Task Post_MissingEmailProperty_Returns400()
    {
        var response = await _client.PostAsJsonAsync("/api/users", new { Name = "Test User" });
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Post_NullEmail_Returns400()
    {
        var response = await _client.PostAsJsonAsync("/api/users", new { Email = (string?)null, Name = "Test User" });
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Post_EmptyEmail_Returns400()
    {
        var response = await _client.PostAsJsonAsync("/api/users", new { Email = "", Name = "Test User" });
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Post_WhitespaceEmail_Returns400()
    {
        var response = await _client.PostAsJsonAsync("/api/users", new { Email = "   ", Name = "Test User" });
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Post_ValidEmail_Returns201()
    {
        var response = await _client.PostAsJsonAsync("/api/users", new { Email = "valid@example.com", Name = "Test User" });
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    // ── PUT /api/users/{id} ──────────────────────────────────────────────────

    [Fact]
    public async Task Put_MissingEmailProperty_Returns400()
    {
        var response = await _client.PutAsJsonAsync("/api/users/1", new { Name = "Updated User" });
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Put_NullEmail_Returns400()
    {
        var response = await _client.PutAsJsonAsync("/api/users/1", new { Email = (string?)null, Name = "Updated User" });
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Put_EmptyEmail_Returns400()
    {
        var response = await _client.PutAsJsonAsync("/api/users/1", new { Email = "", Name = "Updated User" });
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Put_WhitespaceEmail_Returns400()
    {
        var response = await _client.PutAsJsonAsync("/api/users/1", new { Email = "   ", Name = "Updated User" });
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Put_ValidEmail_Returns204()
    {
        var response = await _client.PutAsJsonAsync("/api/users/1", new { Email = "valid@example.com", Name = "Updated User" });
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}
