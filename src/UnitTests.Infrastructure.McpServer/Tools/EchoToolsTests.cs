using AutoFixture;
using Core.Infrastructure.McpServer.Tools;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.Infrastructure.McpServer.Tools
{
    public class EchoToolsTests
    {
        [Fact(DisplayName = "ET-001: Echo should return the formatted message.")]
        public void ET001()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<EchoTools>>(); // Mock the logger
            var fixture = new Fixture();    
            var message = fixture.Create<string>();
            var toolsSettings = new EchoToolsSettings
            {
                MessageFormat = "Test-Echo: {message}"
            };
            var tools = new EchoTools(loggerMock.Object, toolsSettings);

            // Act
            var result = tools.Echo(message);

            // Assert
            result.Should().Be($"Test-Echo: {message}");
        }
    }
}
