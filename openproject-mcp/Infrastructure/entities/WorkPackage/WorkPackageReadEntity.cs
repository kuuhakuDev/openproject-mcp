using openproject_mcp.Infrastructure.entities.WorkPackage.Base;
using System.Text.Json.Serialization;

namespace openproject_mcp.Infrastructure.entities.WorkPackage
{
    public class WorkPackageReadEntity
    {
        public string? DerivedStartDate { get; set; }
        public string? DerivedDueDate { get; set; }
        public string? SpentTime { get; set; }
        public string? LaborCosts { get; set; }
        public string? MaterialCosts { get; set; }
        public string? OverallCosts { get; set; }
        [JsonPropertyName("_type")]
        public string? Type { get; set; }
        public int Id { get; set; }
        public int LockVersion { get; set; }
        private string _subject;

        public string Subject
        {
            get => _subject;
            set
            {
                if (value is not null && value.Length > 255 && string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Subject no puede tener más de 255 caracteres.");

                _subject = value;
            }
        }
        public Formattable? Description { get; set; }
        public bool? ScheduleManually { get; set; }
        public string? StartDate { get; set; }
        public string? DueDate { get; set; }
        //Exists only on work packages of a milestone type
        public string? Date { get; set; }
        public string? EstimatedTime { get; set; }
        public string? DerivedEstimatedTime { get; set; }
        public string? RemainingTime { get; set; }
        public string? DerivedRemainingTime { get; set; }
        public string? Duration { get; set; }
        public bool? IgnoreNonWorkingDays { get; set; }
        public int? PercentageDone { get; set; }
        public int? DerivedPercentageDone { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [JsonPropertyName("readonly")]
        public bool IsReadOnly { get; set; }
        [JsonPropertyName("_links")]
        public Links? Links { get; set; }
    }

    public class Links
    {
        public Link? Attachments { get; set; }
        public Link? AddAttachment { get; set; }
        public Link? FileLinks { get; set; }
        public Link? AddFileLink { get; set; }
        public Link? Update { get; set; }
        public Link? Schema { get; set; }
        public Link? UpdateImmediately { get; set; }
        public Link? Delete { get; set; }
        public Link? LogTime { get; set; }
        public Link? Move { get; set; }
        public Link? Copy { get; set; }
        public Link? Pdf { get; set; }
        public Link? GeneratePdf { get; set; }
        public Link? Atom { get; set; }
        public Link? AvailableRelationCandidates { get; set; }
        public Link? CustomFields { get; set; }
        public Link? ConfigureForm { get; set; }
        public Link? Activities { get; set; }
        public Link? AvailableWatchers { get; set; }
        public Link? Relations { get; set; }
        public Link? Revisions { get; set; }
        public Link? Watchers { get; set; }
        public Link? AddWatcher { get; set; }
        public Link? RemoveWatcher { get; set; }
        public Link? AddRelation { get; set; }
        public Link? AddChild { get; set; }
        public Link? ChangeParent { get; set; }
        public Link? AddComment { get; set; }
        public Link? PreviewMarkup { get; set; }
        public Link? TimeEntries { get; set; }
        public Link? Category { get; set; }
        public Link? Type { get; set; }
        public Link? Priority { get; set; }
        public Link? Project { get; set; }
        public Link? ProjectPhase { get; set; }
        public Link? ProjectPhaseDefinition { get; set; }
        public Link? Status { get; set; }
        public Link? Author { get; set; }
        public Link? Responsible { get; set; }
        public Link? Assignee { get; set; }
        public Link? Version { get; set; }
        public Link? Budget { get; set; }
        public Link? LogCosts { get; set; }
        public Link? ShowCosts { get; set; }
        public Link? CostsByType { get; set; }
        public Link? Github { get; set; }
        public Link? GithubPullRequests { get; set; }
        public Link? GitlabMergeRequests { get; set; }
        public Link? GitlabIssues { get; set; }
        public Link? Meetings { get; set; }
        public Link? Self { get; set; }
        public Link? Unwatch { get; set; }
        public List<object>? Ancestors { get; set; }
        public Link? Parent { get; set; }
        public List<object>? CustomActions { get; set; }
    }
}
