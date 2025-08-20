using openproject_mcp.Infrastructure.entities.WorkPackage;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace openproject_mcp.Infrastructure
{
    public interface IWorkPackageRepository
    {
        Task<WorkPackageReadEntity?> GetWorkPackageByIdAsync(int workPackageId, CancellationToken cancellationToken);
        Task<IEnumerable<WorkPackageReadEntity>> GetAllWorkPackagesAsync(int projectId, CancellationToken cancellationToken);
        Task<WorkPackageReadEntity> CreateWorkPackageAsync(int projectId, WorkPackageWriteEntity workPackage, CancellationToken cancellationToken);
        Task<WorkPackageReadEntity> UpdateWorkPackageAsync(int workPackageId, WorkPackageWriteEntity workPackage, CancellationToken cancellationToken);

    }
    public class WorkPackageRepository : IWorkPackageRepository
    {
        private readonly HttpClient _httpClient;
        public WorkPackageRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WorkPackageReadEntity?> GetWorkPackageByIdAsync(int workPackageId, CancellationToken cancellationToken)
        {
            var filter = new[]
            {
                new Dictionary<string, object>
                {
                    ["id"] = new Dictionary<string, object>
                    {
                        ["operator"] = "=",
                        ["values"] = new[] { workPackageId }
                    }
                }
            };
            var filterJson = JsonSerializer.Serialize(filter);
            var filterEncoded = System.Net.WebUtility.UrlEncode(filterJson);
            var url = $"/api/v3/work_packages?filters={filterEncoded}";
            var response = await _httpClient.GetAsync(url, cancellationToken);

            var jsonString = await EnsureSuccess(response, "Error fetching work packages", cancellationToken);

            var workPackages = DeserializeWorkPackages(jsonString);

            return workPackages.FirstOrDefault();
        }

        public async Task<IEnumerable<WorkPackageReadEntity>> GetAllWorkPackagesAsync(int projectId, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"/api/v3/projects/{projectId}/work_packages", cancellationToken);
            var jsonString = await EnsureSuccess(response, "Error fetching work packages", cancellationToken);

            var workPackages = DeserializeWorkPackages(jsonString);

            return workPackages;
        }

        public async Task<WorkPackageReadEntity> CreateWorkPackageAsync(int projectId, WorkPackageWriteEntity workPackage, CancellationToken cancellationToken)
        {
            var jsonContent = JsonSerializer.Serialize(workPackage, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"/api/v3/projects/{projectId}/work_packages", content, cancellationToken);
            var jsonString = await EnsureSuccess(response, "Error creating work package", cancellationToken);

            return JsonSerializer.Deserialize<WorkPackageReadEntity>(jsonString, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            })!;
        }

        public async Task<WorkPackageReadEntity> UpdateWorkPackageAsync(int workPackageId, WorkPackageWriteEntity workPackage, CancellationToken cancellationToken)
        {
            var jsonContent = JsonSerializer.Serialize(workPackage, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync($"/api/v3/work_packages/{workPackageId}", content, cancellationToken);
            var jsonString = await EnsureSuccess(response, "Error updating work package", cancellationToken);
            return JsonSerializer.Deserialize<WorkPackageReadEntity>(jsonString, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            })!;
        }

        private List<WorkPackageReadEntity> DeserializeWorkPackages(string jsonString)
        {
            var root = JsonNode.Parse(jsonString);

            // Acceder a la lista de elementos
            var elements = root?["_embedded"]?["elements"]?.AsArray();
            if (elements == null)
                throw new Exception("No se encontraron elementos en el JSON");

            // Procesar cada elemento, eliminando la propiedad _links
            var workPackages = new List<WorkPackageReadEntity>();
            var options1 = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            foreach (var element in elements)
            {
                // Deserializar a WorkPackage
                var wp = JsonSerializer.Deserialize<WorkPackageReadEntity>(element.ToJsonString(), options1);
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
