using ModelContextProtocol.Server;
using openproject_mcp.dtos;
using openproject_mcp.services;
using System.ComponentModel;
using System.Text.Json.Nodes;

namespace openproject_mcp.tools
{
    [McpServerToolType]
    public class WorkPackagesTools
    {
        [McpServerTool, Description("Lists the work packages associated with a given project ID.")]
        public static async Task<IEnumerable<JsonObject>> ListWorkPackages(
            OpenProjectApiClient client,
            [Description("Project Id")] int idProject,
            CancellationToken cancellationToken)
        {
            return await client.GetWorkPackagesAsync(idProject, cancellationToken);
        }

        [McpServerTool, Description("Creates a new work package in the specified project.")]
        public static async Task<JsonObject> CreateWorkPackage(
            OpenProjectApiClient client,
            [Description("Project Id")] int idProject,
            [Description("Work Package to create")] WorkPackage workPackage,
            CancellationToken cancellationToken)
        {
            return await client.CreateWorkPackageAsync(idProject, workPackage, cancellationToken);
        }

        //get projects
        [McpServerTool, Description("Lists all projects.")]
        public static async Task<IEnumerable<JsonObject>> ListProjects(
            OpenProjectApiClient client,
            CancellationToken cancellationToken)
        {
            return await client.GetProjectsAsync(cancellationToken);
        }
    }
}
