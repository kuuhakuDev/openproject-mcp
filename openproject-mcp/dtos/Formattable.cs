using System.ComponentModel;

namespace openproject_mcp.dtos
{
    public class Formattable
    {
        [Description("Content format (e.g. 'markdown')")]
        public string? Format { get; set; }
        [Description("Raw text content")]
        public string? Raw { get; set; }
        [Description("HTML-rendered content")]
        public string? Html { get; set; }
    }
}