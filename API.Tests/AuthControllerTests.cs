using Moq;
using webapi.Controllers;
using webapi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Tests;

public class AuthControllerTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public async Task CreateUser_WithInvalidEmail_ShouldReturnBadRequest(string email)
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();
        var controller = new AuthController(mockRepo.Object);

        var dto = new UserDTO
        {
            Email = email,
            Password = "password123"
        };

        // Act
        var result = await controller.Create(dto); 

        // Assert
        var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.Equal("Email is required", badRequestResult.Value);
    }

    [Theory]
    [InlineData("12345")]
    [InlineData(null)]
    [InlineData("   ")]
    [InlineData("")]
    public async Task CreateUser_WithInvalidPassword_ShouldReturnBadRequest(string password)
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();
        var controller = new AuthController(mockRepo.Object);

        var dto = new UserDTO
        {
            Email = "test@example.com",
            Password = password
        };

        // Act
        var result = await controller.Create(dto); 

        // Assert
        var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.Equal("Password is required and must be at least 6 characters long", badRequestResult.Value);
    }
}
