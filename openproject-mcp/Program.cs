using openproject_mcp;
using openproject_mcp.Infrastructure;
using openproject_mcp.services;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Register MCP server and discover tools from the current assembly
builder.Services.AddMcpServer().WithHttpTransport().WithToolsFromAssembly();

var apiSettings = builder.Configuration.GetSection("ApiSettings").Get<ApiSettings>();

if (apiSettings == null)
{
    // Maneja el caso en que la configuración no se encuentre, si es necesario.
    // Esto es opcional, pero buena práctica.
    throw new InvalidOperationException("ApiSettings section not found in configuration.");
}
builder.Services.AddSingleton(_ =>
{
    var client = new HttpClient() { BaseAddress = new Uri(apiSettings.ApiUrl) };
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    var authToken = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"apikey:{apiSettings.ApiKey}"));
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
    return client;
});

// Register the WorkPackageRepository with HttpClient
builder.Services.AddScoped<IWorkPackageRepository, WorkPackageRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

// Register the WorkPackageService
builder.Services.AddScoped<IWorkPackageService, WorkPackageService>();
builder.Services.AddScoped<IProjectService, ProjectService>();

var app = builder.Build();

// Add MCP middleware
app.MapMcp();
app.Run();