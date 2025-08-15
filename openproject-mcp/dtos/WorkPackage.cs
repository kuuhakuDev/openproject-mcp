using System.ComponentModel;

namespace openproject_mcp.dtos
{
    public class WorkPackage
    {
        [Description("Work package subject or title")]
        public string Subject { get; set; } = null!;
        [Description("Detailed description of the work package")]
        public Formattable? Description { get; set; }
        [Description("Indicates if manual scheduling is enabled")]
        public bool? ScheduleManually { get; set; }
        [Description("Indicates if the work package is read-only")]
        public bool? Readonly { get; set; }
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
        [Description("Links to related resources")]
        public WorkPackageLinks? _Links { get; set; }
    }
}