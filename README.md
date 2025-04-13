# Echo MCP Server

A simple echo server implementing the Model Context Protocol (MCP). This server provides a basic echo functionality through the MCP interface.

## Overview

The Echo MCP server is built with .NET Core using the Model Context Protocol C# SDK ([github.com/modelcontextprotocol/csharp-sdk](https://github.com/modelcontextprotocol/csharp-sdk)). It provides a tool for echoing messages back to the client. The server is designed to be lightweight and demonstrates how to create a custom MCP server with a simple functionality. It can be deployed either directly on a machine or as a Docker container.

## Features

- Echo any message back to the client
- Customizable message format through configuration
- Built on the Model Context Protocol standard

## Getting Started

### Prerequisites

- .NET 9.0 (for local development/deployment)
- Docker (for container deployment)

### Build Instructions (for development)

If you want to build the project from source:

1. Clone this repository:
   ```bash
   git clone https://github.com/yourusername/echo-mcp-server.git
   ```

2. Navigate to the project root directory:
   ```bash
   cd echo-mcp-server
   ```

3. Build the project using:
   ```bash
   dotnet build src/echo.sln
   ```

4. Run the tests:
   ```bash
   dotnet test src/echo.sln
   ```

## Docker Support

### Manual Docker Build

To build the Docker image yourself:

```bash
# Navigate to the repository root
cd echo-mcp-server

# Build the Docker image
docker build -f src/Core.Infrastructure.McpServer/Dockerfile -t echo-mcp-server:latest src/

# Run the locally built image
docker run -d --name echo-mcp echo-mcp-server:latest
```

## MCP Protocol Usage

### Client Integration

To connect to the Echo MCP Server from your applications:

1. Use the Model Context Protocol C# SDK or any MCP-compatible client
2. Configure your client to connect to the server's endpoint
3. Call the available tools described below

### Available Tools

#### echo

Echoes the message back to the client.

Parameters:
- `message` (required): The message to echo back.

Example request:
```json
{
  "name": "echo",
  "parameters": {
    "message": "Hello, world!"
  }
}
```

Example response:
```
Echo: Hello, world!
```

## Configuration

### MessageFormat

The `MessageFormat` setting determines how the echoed message is formatted. It uses a template string with the `{message}` placeholder that gets replaced with the input message.

You can set the MessageFormat in two ways:

1. **appsettings.json** file (for local deployment):
```json
{
  "MessageFormat": "Server says: {message}"
}
```

2. **Environment Variables** (for containerized deployments):

When using MCP server with Claude, environment variables are passed through the Claude Desktop configuration as shown in the "Configuring Claude Desktop" section below.

If not specified, the server defaults to "Echo: {message}".

## Configuring Claude Desktop

### Using Local Installation

To configure Claude Desktop to use a locally installed Echo server:

1. Add the server configuration to the `mcpServers` section in your Claude Desktop configuration:
```json
"echo": {
  "command": "dotnet",
  "args": [
    "YOUR_PATH_TO_DLL\\Core.Infrastructure.McpServer.dll"
  ],
  "env": {
    "MessageFormat": "Claude says: {message}"
  }
}
```

2. Save the file and restart Claude Desktop

### Using Docker Container

To use the Echo server from a Docker container with Claude Desktop:

1. Add the server configuration to the `mcpServers` section in your Claude Desktop configuration:
```json
"echo": {
  "command": "docker",
  "args": [
    "run",
    "--rm",
    "-i",
    "-e", "MessageFormat=Claude says: {message}",
    "echo-mcp-server:latest"
  ]
}
```

#### Using Docker with Custom Configuration File

To use a custom configuration file from your host system:

1. Create a custom configuration file on your host machine (e.g., `echo-appsettings.json`):
```json
{
  "MessageFormat": "My Custom Format: {message}",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning"
    }
  }
}
```

2. Update the Claude Desktop configuration to mount this file:
```json
"echo": {
  "command": "docker",
  "args": [
    "run",
    "--rm",
    "-i",
    "-v", "C:/path/to/echo-appsettings.json:/app/appsettings.json",
    "echo-mcp-server:latest"
  ]
}
```

**Important Notes:**
- The configuration file must exist on your host system before starting the container
- You can use any filename on the host (e.g., `echo-appsettings.json`), but it must be mounted to `/app/appsettings.json` inside the container
- Make sure the path is correct for your operating system (Windows uses backslashes or forward slashes, Linux/macOS use forward slashes)
- After changing the configuration file, restart Claude Desktop to apply the changes

3. Save the file and restart Claude Desktop

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.