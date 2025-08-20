using System.ComponentModel;

namespace openproject_mcp.Dtos
{
    public class WorkPackageDto
    {
        [Description("Work package subject or title")]
        public string Subject { get; set; } = null!;
        [Description("Detailed description of the work package")]
        public string? Description { get; set; }
        [Description("Scheduled start date (YYYY-MM-DD)")]
        public string? StartDate { get; set; }
        [Description("Scheduled due date (YYYY-MM-DD)")]
        public string? DueDate { get; set; }
        [Description("Date when the milestone is achieved (YYYY-MM-DD)")]
        public string? Date { get; set; }
        [Description("Estimated time to complete the work package (ISO 8601 duration)")]
        public string? EstimatedTime { get; set; }
        [Description("Completion percentage of the work package (0-100)")]
        public int? PercentageDone { get; set; }
    }
}
