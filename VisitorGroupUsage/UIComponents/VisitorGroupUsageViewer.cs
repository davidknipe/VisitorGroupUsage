using EPiServer.Shell;
using EPiServer.Shell.ViewComposition;

namespace VisitorGroupUsage.UIComponents
{
    [Component(PlugInAreas = PlugInArea.Assets,
           Categories = "cms",
           WidgetType = "visitorgroupusage/viewer",
           LanguagePath = "/widgets/visitorgroupusageviewer", 
           SortOrder = int.MaxValue
           )]
    public class VisitorGroupUsageViewer { }
}
