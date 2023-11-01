using HealthCheck.Console;

namespace HealthCheck.Tests;

public class HealthCheckConsoleTests
{
    [Theory]
    [InlineData("google.com")]
    [InlineData("http://google.com")]
    [InlineData("https://google.com")]
    public async Task GivenValidHealthyHost_ReturnSuccessExitCode(string host)
    {
        // Arrange
        var args = new string[] { host };

        // Act
        var exitCode = await Program.Main(args);

        // Assert
        Assert.Equal(0, exitCode);
    }

    [Theory]
    [InlineData("https://google.com/error")]
    public async Task GivenValidUnhealthyHost_ReturnErrorExitCode(string host)
    {
        // Arrange
        var args = new string[] { host };

        // Act
        var exitCode = await Program.Main(args);

        // Assert
        Assert.Equal(1, exitCode);
    }

    [Theory]
    [InlineData("https://google.comm")]
    public async Task GivenInvalidHost_ReturnErrorExitCode(string host)
    {
        // Arrange
        var args = new string[] { host };

        // Act
        var exitCode = await Program.Main(args);

        // Assert
        Assert.Equal(1, exitCode);
    }
}

