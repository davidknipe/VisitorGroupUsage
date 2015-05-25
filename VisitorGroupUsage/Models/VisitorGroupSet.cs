using System.Collections.Generic;

namespace VisitorGroupUsage.Models
{
    public class VisitorGroupSet
    {
        public VisitorGroupSet()
        {
            UsedVisitorGroups = new Dictionary<string, string>();
        }

        public string LanguageKey { get; set; }
        public IDictionary<string, string> UsedVisitorGroups { get; set; }
        public string SelectedVisitorGroup { get; set; }
    }
}
