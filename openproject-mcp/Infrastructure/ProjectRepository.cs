using openproject_mcp.Infrastructure.entities.Project;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace openproject_mcp.Infrastructure
{
    public interface IProjectRepository
    {
        Task<IEnumerable<ProjectReadEntity>> GetAllProjectsAsync(CancellationToken cancellationToken);
    }
    public class ProjectRepository : IProjectRepository
    {
        private readonly HttpClient _httpClient;
        public ProjectRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<ProjectReadEntity>> GetAllProjectsAsync(CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"/api/v3/projects", cancellationToken);
            var jsonString = await EnsureSuccess(response, "Error fetching projects", cancellationToken);
            var workPackages = DeserializeProjects(jsonString);
            return workPackages;
        }



        private List<ProjectReadEntity> DeserializeProjects(string jsonString)
        {
            var root = JsonNode.Parse(jsonString);

            // Acceder a la lista de elementos
            var elements = root?["_embedded"]?["elements"]?.AsArray();
            if (elements == null)
                throw new Exception("No se encontraron elementos en el JSON");

            // Procesar cada elemento, eliminando la propiedad _links
            var workPackages = new List<ProjectReadEntity>();
            var options1 = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            foreach (var element in elements)
            {
                // Deserializar a Project
                var wp = JsonSerializer.Deserialize<ProjectReadEntity>(element.ToJsonString(), options1);
                if (wp != null)
                    workPackages.Add(wp);
            }
            return workPackages;
        }

        // Maneja la respuesta HTTP y lanza excepción si falla
        private async Task<string> EnsureSuccess(HttpResponseMessage response, string errorMessage, CancellationToken cancellationToken)
        {
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync(cancellationToken);
            }
            throw new Exception($"{errorMessage}: {response.ReasonPhrase}");
        }
    }
}
