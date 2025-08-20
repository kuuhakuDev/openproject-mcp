using ModelContextProtocol.Server;
using openproject_mcp.Dtos;
using openproject_mcp.Infrastructure.entities.WorkPackage;
using openproject_mcp.services;
using System.ComponentModel;
using System.Text.Json.Nodes;

namespace openproject_mcp.tools
{
    [McpServerToolType]
    public class WorkPackageTool
    {
        [McpServerTool, Description("Lists the work packages associated with a given project ID.")]
        public static async Task<IEnumerable<WorkPackageReadEntity>> ListWorkPackages(
            IWorkPackageService service,
            [Description("Project Id")] int idProject,
            CancellationToken cancellationToken)
        {
            return await service.GetAllInformationWorkPackagesAsync(idProject, cancellationToken);
        }

        //get work package summary
        [McpServerTool, Description("Lists the work package summaries associated with a given project ID.")]
        public static async Task<IEnumerable<WorkPackageSummary>> ListWorkPackageSummaries(
            IWorkPackageService service,
            [Description("Project Id")] int idProject,
            CancellationToken cancellationToken)
        {
            return await service.GetAllWorkPackageSummaryAsync(idProject, cancellationToken);
        }

        [McpServerTool, Description("Creates a new work package in the specified project.")]
        public static async Task<WorkPackageReadEntity> CreateWorkPackage(
            IWorkPackageService service,
            [Description("Project Id")] int idProject,
            [Description("Work Package to create")] WorkPackageDto workPackage,
            CancellationToken cancellationToken)
        {
            return await service.CreateTaskWorkPackageAsync(idProject, workPackage, cancellationToken);
        }

        //Update WorkPackage
        [McpServerTool, Description("Updates an existing work package by its ID.")]
        public static async Task<WorkPackageReadEntity> UpdateWorkPackage(
            IWorkPackageService service,
            [Description("Work Package Id")] int idWorkPackage,
            [Description("Work Package to update")] WorkPackageDto workPackage,
            CancellationToken cancellationToken)
        {
            return await service.UpdateWorkPackageAsync(idWorkPackage, workPackage, cancellationToken);
        }

        //Create a Bug
        [McpServerTool, Description("Creates a new bug in the specified project.")]
        public static async Task<WorkPackageReadEntity> CreateBug(
            IWorkPackageService service,
            [Description("Project Id")] int idProject,
            [Description("Bug to create")] WorkPackageDto bug,
            CancellationToken cancellationToken)
        {
            return await service.CreateBugWorkPackageAsync(idProject, bug, cancellationToken);
        }
    }
}
