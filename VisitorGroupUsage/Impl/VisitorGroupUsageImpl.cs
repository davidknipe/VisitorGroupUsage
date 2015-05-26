using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using EPiServer.Core;
using EPiServer.Personalization;
using EPiServer.ServiceLocation;
using EPiServer.SpecializedProperties;
using VisitorGroupUsage.Interfaces;

namespace VisitorGroupUsage.Impl
{
    /// <summary>
    /// Implementation of the algorythm to 
    /// </summary>
    [ServiceConfiguration(typeof(IVisitorGroupUsage))]
    public class VisitorGroupUsageImpl : IVisitorGroupUsage
    {
        private readonly HashSet<ContentReference> _checkedItems = new HashSet<ContentReference>(); 

        /// <summary>
        /// Get a list of visitor groups that are used on the content item, including referenced content items
        /// </summary>
        /// <param name="currentContent">The IContent item to start looking for visitor groups with</param>
        /// <returns>A unique set of visitor groups that are used within the content, with the depth they were found at</returns>
        public IDictionary<string, int> GetVisitorGroupsForContent(IContent currentContent)
        {
            return GetVisitorGroupsRecursive(currentContent, 0);
        }

        /// <summary>
        /// Recursively get a unique list of vistor groups that are used in the content
        /// </summary>
        /// <param name="currentContent">The IContent item to look for visitor groups</param>
        /// <param name="depth">The depth we are in the recursive loop</param>
        /// <returns>A unique set of visitor groups that are used within the content, with the depth they were found at</returns>
        private IDictionary<string, int> GetVisitorGroupsRecursive(IContent currentContent, int depth)
        {
            var visitorGroups = new Dictionary<string, int>();

            //Prevent infinite recursive loops
            if (_checkedItems.Contains(currentContent.ContentLink))
                return visitorGroups;

            _checkedItems.Add(currentContent.ContentLink);

            foreach (var property in currentContent.Property)
            {
                //Properties that can be personalised such as the PropertyXhtmlString implement IPersonalizedRoles
                if (property is IPersonalizedRoles)
                {
                    (property as IPersonalizedRoles).GetRoles().ForEach(x => addVisitorGroup(visitorGroups, x, depth));
                }
                //Check content areas for other content
                if (property is PropertyContentArea)
                {
                    if (property.Value != null)
                    {
                        foreach (var item in (((ContentArea) (property.Value)).Items))
                        {
                            if (item.AllowedRoles != null)
                            {
                                item.AllowedRoles.ForEach(x => addVisitorGroup(visitorGroups, x, depth));
                            }

                            var blockData = item.GetContent();
                            var groups = GetVisitorGroupsRecursive(blockData, depth + 1);
                            groups.ForEach(x => addVisitorGroup(visitorGroups, x.Key, depth + 1));
                        }
                    }
                }
            }

            return visitorGroups;
        }

        /// <summary>
        /// Add a visitor group to the list, ensuring groups used on the current content item have depth 0 (i.e. "Used on this content")
        /// </summary>
        /// <param name="visitorGroups">The IDictionary&lt;string, int&gt; of the visitor groups used</param>
        /// <param name="group">The visitor group Guid to add</param>
        /// <param name="depth">The current depth of the operation</param>
        private void addVisitorGroup(IDictionary<string, int> visitorGroups, string group, int depth)
        {
            if (!visitorGroups.ContainsKey(group))
            {
                visitorGroups.Add(group, depth);
            }
            else if (depth == 0 && visitorGroups.ContainsKey(group))
            {
                visitorGroups[group] = depth;
            }
        }
    }
}