using Microsoft.AspNetCore.DataProtection.KeyManagement;
using openproject_mcp.services;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Register MCP server and discover tools from the current assembly
builder.Services.AddMcpServer().WithHttpTransport().WithToolsFromAssembly();

var apiKey = Environment.GetEnvironmentVariable("API_KEY");
var url = Environment.GetEnvironmentVariable("API_URL");
builder.Services.AddSingleton(_ =>
{
    var client = new HttpClient() { BaseAddress = new Uri(url) };
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    var authToken = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"apikey:{apiKey}"));
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
    return client;
});

// Solución: use una función para crear la instancia de OpenProjectApiClient con la URL
builder.Services.AddScoped<OpenProjectApiClient>();

var app = builder.Build();

// Add MCP middleware
app.MapMcp();
app.Run();