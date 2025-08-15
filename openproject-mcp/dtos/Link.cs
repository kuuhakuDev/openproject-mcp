using System.ComponentModel;

namespace openproject_mcp.dtos
{
    public class Link
    {
        [Description("Linked resource URL")]
        public string? Href { get; set; }
        [Description("Descriptive title of the link")]
        public string? Title { get; set; }
    }
}