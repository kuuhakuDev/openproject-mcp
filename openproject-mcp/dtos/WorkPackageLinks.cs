using System.ComponentModel;

namespace openproject_mcp.dtos
{
    public class WorkPackageLinks
    {
        [Description("Project to which the work package belongs")]
        public Link? Project { get; set; }
        [Description("Work package type")]
        public Link? Type { get; set; }
        [Description("Current status of the work package")]
        public Link? Status { get; set; }
        [Description("Work package priority")]
        public Link? Priority { get; set; }
        [Description("User assigned to the work package")]
        public Link? Assignee { get; set; }
        [Description("Parent work package")]
        public Link? Parent { get; set; }
        [Description("Work package category")]
        public Link? Category { get; set; }
        [Description("User responsible for the overall outcome")]
        public Link? Responsible { get; set; }
        [Description("Associated budget")]
        public Link? Budget { get; set; }
        [Description("Associated version")]
        public Link? Version { get; set; }
    }
}