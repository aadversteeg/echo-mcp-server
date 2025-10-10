using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace Core.Infrastructure.McpServer.Tools
{
    [McpServerToolType]
    public class EchoTools
    {
        private readonly ILogger _logger;
        private readonly string _messageFormat;


        public EchoTools(ILogger <EchoTools> logger, EchoToolsSettings settings)
        {
            _logger = logger;
            _messageFormat = settings.MessageFormat;
        }

        [McpServerTool(Name = "echo", ReadOnly = true, OpenWorld = false), Description("Echoes the message back to the client.")]
        public async Task<string> Echo(string message, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Echoing message: {message}", message);

            // CancellationToken is automatically wired by the SDK
            cancellationToken.ThrowIfCancellationRequested();

            await Task.CompletedTask;

            return _messageFormat.Replace("{message}", message);
        }
    }
}
