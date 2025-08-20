namespace openproject_mcp.Infrastructure.entities.WorkPackage.Base
{
    public class Link
    {
        public string? Href { get; set; }
        public string? Method { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
        public object? Payload { get; set; }
        public bool? Templated { get; set; }
    }
}
