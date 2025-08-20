using ModelContextProtocol.Server;
using openproject_mcp.Infrastructure.entities.Project;
using openproject_mcp.services;
using System.ComponentModel;

namespace openproject_mcp.tools
{
    [McpServerToolType]
    public class ProjectTool
    {

        //get projects
        [McpServerTool, Description("Lists all projects.")]
        public static async Task<IEnumerable<ProjectReadEntity>> ListProjects(
            IProjectService service,
            CancellationToken cancellationToken)
        {
            return await service.GetAllProjectsAsync(cancellationToken);
        }
    }
}
