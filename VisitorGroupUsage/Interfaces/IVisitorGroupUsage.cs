using System.Collections.Generic;
using EPiServer.Core;

namespace VisitorGroupUsage.Interfaces
{
    public interface IVisitorGroupUsage
    {
        IDictionary<string, int> GetVisitorGroupsForContent(IContent currentContent);
    }
}
