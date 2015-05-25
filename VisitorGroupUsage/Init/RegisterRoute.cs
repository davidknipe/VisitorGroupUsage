using System.Web.Mvc;
using System.Web.Routing;
using EPiServer.Configuration;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using InitializationModule = EPiServer.Web.InitializationModule;

namespace VisitorGroupUsage.Init
{
    [ModuleDependency(typeof(InitializationModule))]
    public class RegisterRoute : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var uiUrl = Settings.Instance.UIUrl.OriginalString.TrimStart("~/".ToCharArray()).TrimEnd("/".ToCharArray());

            //Resister the route to hang off the EPiServer UI Url to inherit any current security configs
            RouteTable.Routes.MapRoute(
                name: "VisitorGroupUsageRoute",
                url: uiUrl + "/VisitorGroupUsage/{action}",
                defaults: new { controller = "VisitorGroupUsage", action = "Index" }
            );
        }

        public void Uninitialize(InitializationEngine context) { }
    }
}
