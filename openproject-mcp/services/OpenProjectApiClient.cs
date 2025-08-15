using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.Json;
using openproject_mcp.dtos;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace openproject_mcp.services
{
    public class OpenProjectApiClient
    {
        private readonly HttpClient _httpClient;

        public OpenProjectApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Obtiene y procesa una lista de elementos embebidos en la respuesta JSON
        private static List<JsonObject> ExtractEmbeddedElements(string jsonString)
        {
            var json = JsonNode.Parse(jsonString);
            var elements = json?["_embedded"]?["elements"]?.AsArray();
            var result = new List<JsonObject>();
            if (elements is not null)
            {
                foreach (var element in elements)
                {
                    var obj = element!.AsObject();
                    obj.Remove("_links"); // Elimina la propiedad "_links"
                    result.Add(obj);
                }
            }
            return result;
        }

        // Maneja la respuesta HTTP y lanza excepción si falla
        private static async Task<string> EnsureSuccess(HttpResponseMessage response, string errorMessage, CancellationToken cancellationToken)
        {
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync(cancellationToken);
            }
            throw new Exception($"{errorMessage}: {response.ReasonPhrase}");
        }

        public async Task<IEnumerable<JsonObject>> GetWorkPackagesAsync(int projectId, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"/api/v3/projects/{projectId}/work_packages", cancellationToken);
            var jsonString = await EnsureSuccess(response, "Error fetching work packages", cancellationToken);
            return ExtractEmbeddedElements(jsonString);
        }

        public async Task<JsonObject> CreateWorkPackageAsync(int projectId, WorkPackage workPackage, CancellationToken cancellationToken)
        {
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var json = JsonSerializer.Serialize(workPackage, options);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"/api/v3/projects/{projectId}/work_packages", content, cancellationToken);
            var jsonString = await EnsureSuccess(response, "Error creating work package", cancellationToken);
            return JsonNode.Parse(jsonString)!.AsObject();
        }

        public async Task<IEnumerable<JsonObject>> GetProjectsAsync(CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync("/api/v3/projects", cancellationToken);
            var jsonString = await EnsureSuccess(response, "Error fetching projects", cancellationToken);
            return ExtractEmbeddedElements(jsonString);
        }
    }
}
