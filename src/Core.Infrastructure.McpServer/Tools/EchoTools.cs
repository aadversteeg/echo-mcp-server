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

        [McpServerTool(Name = "echo"), Description("Echoes the message back to the client.")]
        public string Echo(string message)
        {
            _logger.LogInformation("Echoing message: {message}", message);
            return _messageFormat.Replace("{message}", message);
        }
    }
}
