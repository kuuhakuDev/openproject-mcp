using openproject_mcp.Infrastructure.entities.WorkPackage.Base;
using System.Text.Json.Serialization;

namespace openproject_mcp.Infrastructure.entities.Project
{
    public class ProjectReadEntity
    {
        [JsonPropertyName("_type")]
        public string? Type { get; set; }
        public int Id { get; set; }
        public string? Identifier { get; set; }
        public string? Name { get; set; }
        public bool Active { get; set; }
        public bool Public { get; set; }
        public Formattable? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public StatusExplanation? StatusExplanation { get; set; }
        [JsonPropertyName("_links")]
        public Links? Links { get; set; }
    }

    public class StatusExplanation
    {
        public string? Format { get; set; }
        public string? Raw { get; set; }
        public string? Html { get; set; }
    }

    public class Links
    {
        public Link? Self { get; set; }
        public Link? CreateWorkPackage { get; set; }
        public Link? CreateWorkPackageImmediately { get; set; }
        public Link? WorkPackages { get; set; }
        public List<object> Storages { get; set; }
        public Link? Categories { get; set; }
        public Link? Versions { get; set; }
        public Link? Memberships { get; set; }
        public Link? Types { get; set; }
        public Link? Update { get; set; }
        public Link? UpdateImmediately { get; set; }
        public Link? Delete { get; set; }
        public Link? Schema { get; set; }
        public Link? Status { get; set; }
        public List<object> Ancestors { get; set; }
        public Link? ProjectStorages { get; set; }
        public Link? Parent { get; set; }
    }
}
