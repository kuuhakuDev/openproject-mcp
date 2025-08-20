using openproject_mcp.Infrastructure.entities.WorkPackage.Base;
using System.Text.Json.Serialization;

namespace openproject_mcp.Infrastructure.entities.WorkPackage
{
    public class WorkPackageWriteEntity
    {
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
        public int LockVersion { get; set; }
        public string? DueDate { get; set; }
        //Exists only on work packages of a milestone type
        public string? Date { get; set; }
        public string? EstimatedTime { get; set; }
        public string? Duration { get; set; }
        [JsonPropertyName("_links")]
        public LinksWriteEntity? Links { get; set; }
    }

    public class LinksWriteEntity
    {
        public Link? ProjectPhase { get; set; }
        public Link? Attachments { get; set; }
        public Link? Budget { get; set; }
        public Link? Category { get; set; }
        public Link? Type { get; set; }
        public Link? Priority { get; set; }
        public Link? Project { get; set; }
        public Link? Status { get; set; }
        public Link? Responsible { get; set; }
        public Link? Assignee { get; set; }
        public Link? Version { get; set; }
        public Link? Parent { get; set; }
    }
}
