# OpenProject MCP Server

A Model Context Protocol (MCP) server that provides seamless integration with OpenProject instances, enabling AI assistants and applications to interact with OpenProject's project management features through a standardized interface.

## What is this?

This is an MCP server implementation that bridges the gap between AI assistants (like Claude, ChatGPT, etc.) and OpenProject - a leading open-source project management software. It exposes OpenProject's functionality through the Model Context Protocol, allowing AI tools to manage projects, work packages, and other project-related tasks on your behalf.

## What does it do?

The OpenProject MCP Server provides the following capabilities:

### 🏗️ Project Management
- **List Projects**: Retrieve all projects from your OpenProject instance
- **Project Information**: Access detailed project information including status, description, and metadata

### 📋 Work Package Management
- **List Work Packages**: Get all work packages for specific projects
- **Create Work Packages**: Generate new tasks, features, or other work items
- **Create Bugs**: Specialized tool for creating bug reports
- **Work Package Summaries**: Get condensed views of work packages for quick overview

### 🔧 Supported Work Package Types
- Tasks
- Bugs
- Features
- Milestones
- User Stories
- And other custom types configured in your OpenProject instance

## Why was this created?

Currently, there is **no existing MCP server for OpenProject**. While there are integrations available for various project management tools, OpenProject - despite being a powerful and widely-used open-source project management platform - lacked native support for modern AI assistant integrations.

This MCP server fills that gap by:
- Providing a standardized way for AI tools to interact with OpenProject
- Enabling automation of project management tasks through conversational AI
- Allowing seamless integration between OpenProject and AI-powered workflows
- Supporting the growing ecosystem of MCP-compatible tools and applications

## How to use it

### Prerequisites

- .NET 8.0 or later
- Access to an OpenProject instance
- OpenProject API key

### Installation & Setup

#### Option 1: Using Docker (Recommended)

1. Clone the repository:
```bash
git clone https://github.com/kuuhakuDev/openproject-mcp.git
cd openproject-mcp
```

2. Configure your OpenProject settings in `docker-compose.yml`:
```yaml
environment:
  - ApiSettings__ApiUrl=http://your-openproject-instance:port
  - ApiSettings__ApiKey=your-api-key-here
```

3. Run the container:
```bash
docker-compose up -d
```

#### Option 2: Running from Source

1. Clone and build:
```bash
git clone https://github.com/kuuhakuDev/openproject-mcp.git
cd openproject-mcp/openproject-mcp
dotnet build
```

2. Configure `appsettings.json`:
```json
{
  "ApiSettings": {
    "ApiUrl": "http://your-openproject-instance:port",
    "ApiKey": "your-openproject-api-key"
  }
}
```

3. Run the application:
```bash
dotnet run
```

### Getting Your OpenProject API Key

1. Log into your OpenProject instance
2. Go to **My Account** → **Access Tokens**
3. Create a new API token
4. Copy the generated token for use in configuration

### Configuration

The server requires two main configuration parameters:

- **ApiUrl**: The base URL of your OpenProject instance (e.g., `http://localhost:8080` or `https://your-company.openproject.com`)
- **ApiKey**: Your OpenProject API access token

These can be configured via:
- Environment variables: `ApiSettings__ApiUrl` and `ApiSettings__ApiKey`
- Configuration file: `appsettings.json`
- Docker environment variables (as shown above)

### Using with MCP-Compatible Clients

Once running, the server can be connected to any MCP-compatible client. The server will expose tools for:

1. **ListProjects**: Get all available projects
2. **ListWorkPackages**: Get work packages for a specific project
3. **ListWorkPackageSummaries**: Get summarized work package information
4. **CreateWorkPackage**: Create new work packages
5. **CreateBug**: Create bug reports

Example usage with an AI assistant:
```
"List all projects in my OpenProject instance"
"Create a new bug for project ID 5 with subject 'Login page not loading'"
"Show me all work packages for the Mobile App project"
```

## Technical Details

### Architecture

- **Framework**: ASP.NET Core 8.0
- **Protocol**: Model Context Protocol (MCP)
- **Communication**: HTTP API calls to OpenProject REST API
- **Authentication**: API Key-based authentication
- **Containerization**: Docker support included

### Dependencies

- `ModelContextProtocol.AspNetCore` (v0.3.0-preview.3) - Core MCP implementation
- `Mapster.DependencyInjection` (v1.0.1) - Object mapping
- `Microsoft.AspNetCore.*` - Web framework

### Project Structure

```
openproject-mcp/
├── tools/              # MCP tool implementations
│   ├── ProjectTool.cs     # Project management tools
│   └── WorkPackageTool.cs # Work package management tools
├── services/           # Business logic layer
├── Infrastructure/     # Data access and API integration
├── Dtos/              # Data transfer objects
└── Program.cs         # Application entry point
```

## Contributing

Contributions are welcome! This project follows standard .NET development practices.

### Development Setup

1. Clone the repository
2. Ensure you have .NET 8.0 SDK installed
3. Configure a test OpenProject instance
4. Run `dotnet build` to verify setup
5. Make your changes and submit a pull request

### Areas for Contribution

- Additional OpenProject API integrations
- Enhanced error handling and logging
- Performance optimizations
- Documentation improvements
- Test coverage
- Support for additional work package types

## Roadmap

Future enhancements planned:

- [ ] User and team management tools
- [ ] Time tracking integration
- [ ] Custom field support
- [ ] Bulk operations
- [ ] Webhook support for real-time updates
- [ ] Integration with OpenProject's notification system
- [ ] Support for file attachments
- [ ] Advanced filtering and search capabilities

## License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.

## Support

For issues, questions, or contributions:

1. Check existing issues on GitHub
2. Create a new issue if needed
3. Provide detailed information about your OpenProject setup and configuration

## Acknowledgments

- OpenProject team for their excellent open-source project management platform
- Anthropic and the broader community for developing the Model Context Protocol
- All contributors who help improve this integration

---

**Note**: This is an independent project and is not officially affiliated with or endorsed by OpenProject GmbH.