using Mapster;
using openproject_mcp.Dtos;
using openproject_mcp.Infrastructure;
using openproject_mcp.Infrastructure.entities.WorkPackage;
using openproject_mcp.Infrastructure.entities.WorkPackage.Base;

namespace openproject_mcp.services
{
    public interface IWorkPackageService
    {
        Task<IEnumerable<WorkPackageReadEntity>> GetAllInformationWorkPackagesAsync(int projectId, CancellationToken cancellationToken);
        Task<IEnumerable<WorkPackageSummary>> GetAllWorkPackageSummaryAsync(int projectId, CancellationToken cancellationToken);
        Task<WorkPackageReadEntity> CreateBugWorkPackageAsync(int projectId, WorkPackageDto workPackageDto, CancellationToken cancellationToken);
        Task<WorkPackageReadEntity> CreateTaskWorkPackageAsync(int projectId, WorkPackageDto workPackageDto, CancellationToken cancellationToken);
        Task<WorkPackageReadEntity> UpdateWorkPackageAsync(int workPackageId, WorkPackageDto workPackage, CancellationToken cancellationToken);
    }
    public class WorkPackageService : IWorkPackageService
    {
        private readonly IWorkPackageRepository _workPackageRepository;
        public WorkPackageService(IWorkPackageRepository workPackageRepository)
        {
            _workPackageRepository = workPackageRepository;
        }

        public async Task<IEnumerable<WorkPackageSummary>> GetAllWorkPackageSummaryAsync(int projectId, CancellationToken cancellationToken)
        {
            var workPackages = await _workPackageRepository.GetAllWorkPackagesAsync(projectId, cancellationToken);
            var summaries = workPackages.Adapt<IEnumerable<WorkPackageSummary>>();
            return summaries;
        }

        public async Task<IEnumerable<WorkPackageReadEntity>> GetAllInformationWorkPackagesAsync(int projectId, CancellationToken cancellationToken)
        {
            return await _workPackageRepository.GetAllWorkPackagesAsync(projectId, cancellationToken);
        }

        public async Task<WorkPackageReadEntity> CreateBugWorkPackageAsync(int projectId, WorkPackageDto workPackageDto, CancellationToken cancellationToken)
        {

            var model = new WorkPackageWriteEntity
            {
                Subject = workPackageDto.Subject,
                Description = new Formattable { Format = "markdown", Raw = workPackageDto.Description },
                Date = workPackageDto.Date,
                StartDate = workPackageDto.StartDate,
                DueDate = workPackageDto.DueDate,
                EstimatedTime = workPackageDto.EstimatedTime,
                Links = new LinksWriteEntity
                {
                    Type = new Link { Title = "Bug", Href = "/api/v3/types/7" } // Assuming "Bug" is the type name for bugs
                }
            };

            return await _workPackageRepository.CreateWorkPackageAsync(projectId, model, cancellationToken);
        }

        public async Task<WorkPackageReadEntity> CreateTaskWorkPackageAsync(int projectId, WorkPackageDto workPackageDto, CancellationToken cancellationToken)
        {

            var model = new WorkPackageWriteEntity
            {
                Subject = workPackageDto.Subject,
                Description = new Formattable { Format = "markdown", Raw = workPackageDto.Description },
                Date = workPackageDto.Date,
                StartDate = workPackageDto.StartDate,
                DueDate = workPackageDto.DueDate,
                EstimatedTime = workPackageDto.EstimatedTime
            };

            return await _workPackageRepository.CreateWorkPackageAsync(projectId, model, cancellationToken);
        }

        public async Task<WorkPackageReadEntity> UpdateWorkPackageAsync(int workPackageId, WorkPackageDto workPackage, CancellationToken cancellationToken)
        {
            var oldWorkPackageData = await _workPackageRepository.GetWorkPackageByIdAsync(workPackageId, cancellationToken);
            if (oldWorkPackageData == null)
            {
                throw new Exception("Failed to deserialize old work package data.");
            }

            var newWorkPackageData = oldWorkPackageData.Adapt<WorkPackageWriteEntity>();

            newWorkPackageData.StartDate = workPackage.StartDate ?? newWorkPackageData.StartDate;
            newWorkPackageData.DueDate = workPackage.DueDate ?? newWorkPackageData.DueDate;
            newWorkPackageData.EstimatedTime = workPackage.EstimatedTime ?? newWorkPackageData.EstimatedTime;
            newWorkPackageData.Subject = workPackage.Subject ?? newWorkPackageData.Subject;
            newWorkPackageData.Description = new Formattable { Format = "markdown", Raw = workPackage.Description ?? newWorkPackageData.Description?.Raw };

            var workPackageRead = await _workPackageRepository.UpdateWorkPackageAsync(workPackageId, newWorkPackageData, cancellationToken);

            return workPackageRead;
        }
    }

    public class WorkPackageSummary
    {
        public int Id { get; set; }
        public string Subject { get; set; } = null!;
        public Formattable? Description { get; set; }
        public string? StartDate { get; set; }
        public string? DueDate { get; set; }
        public string? Duration { get; set; }
        public int? PercentageDone { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
