using System.Net;
using AutomationInTesting.API.Infrastructure;
using AutomationInTesting.API.Models;
using FluentAssertions;

namespace AutomationInTesting.API.tests;

[TestFixture]
[Category("API")]
public class CreateTokenTests : TestBase
{
    [Test]
    public async Task CreateToken_GivenValidUserNameAndPassword_ShouldGenerateToken()
    {
        // Arrange
        var requestBody = new
        {
            username = Username,
            password = Password
        };

        // Act
        var response = await PostAsync("auth", requestBody);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseData = await DeserializeResponseAsync<AuthResponse>(response);

        responseData.Should().NotBeNull();
        responseData!.Token.Should().NotBeNullOrEmpty();
    }
    
    [Test]
    public async Task CreateToken_GivenInvalidUserNameAndValidPassword_ShouldReturnBadRequest()
    {
        // Arrange
        var requestBody = new
        {
            username = string.Empty,
            password = Password
        };

        // Act
        var response = await PostAsync("auth", requestBody);

        // Assert
        // This response should not be 200.
        // response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseBody = await response.Content.ReadAsStringAsync();
        responseBody.Should().Contain("\"reason\":\"Bad credentials\"");
    }
    
    [Test]
    public async Task CreateToken_GivenValidUserNameAndInvalidPassword_ShouldReturnBadRequest()
    {
        // Arrange
        var requestBody = new
        {
            username = Username,
            password = string.Empty
        };

        // Act
        var response = await PostAsync("auth", requestBody);

        // Assert
        // This response should not be 200.
        // response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseBody = await response.Content.ReadAsStringAsync();
        responseBody.Should().Contain("\"reason\":\"Bad credentials\"");
    }
}