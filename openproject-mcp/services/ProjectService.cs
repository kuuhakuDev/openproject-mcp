using openproject_mcp.Infrastructure;
using openproject_mcp.Infrastructure.entities.Project;

namespace openproject_mcp.services
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectReadEntity>> GetAllProjectsAsync(CancellationToken cancellationToken);
    }
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _workPackageRepository;
        public ProjectService(IProjectRepository workPackageRepository)
        {
            _workPackageRepository = workPackageRepository;
        }

        public async Task<IEnumerable<ProjectReadEntity>> GetAllProjectsAsync(CancellationToken cancellationToken)
        {
            var projects = await _workPackageRepository.GetAllProjectsAsync(cancellationToken);
            return projects;
        }
    }
}
