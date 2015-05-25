using System;
using System.Linq;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Personalization.VisitorGroups;
using EPiServer.ServiceLocation;
using VisitorGroupUsage.Interfaces;
using VisitorGroupUsage.Models;
using EPiServer.Logging;

namespace VisitorGroupUsage.Controllers
{
    //[Authorize(Roles = "WebEditors, WebAdmins, Administrator")]
    public class VisitorGroupUsageController : Controller
    {
        private static readonly ILogger _logger = LogManager.GetLogger();

        public ActionResult Index()
        {
            if (this.Request.QueryString["id"] == null)
            {
                return PartialView(null);
            }

            try
            {
                return PartialView(GetModel());
            }
            catch (Exception ex)
            {
                _logger.Error("Exception in VisitorGroupUsageController", ex);
                return PartialView(null);
            }
        }

        private VisitorGroupUsageModel GetModel()
        {
            var visitorGroupRepo = ServiceLocator.Current.GetInstance<IVisitorGroupRepository>();
            var visitorGroupUsage = ServiceLocator.Current.GetInstance<IVisitorGroupUsage>();
            var contentRepo = ServiceLocator.Current.GetInstance<IContentRepository>();

            string id = this.Request.QueryString["id"];
            var content = contentRepo.Get<IContent>(ContentReference.Parse(id));

            var model = new VisitorGroupUsageModel();

            model.UsedOnThisContent.LanguageKey = "/widgets/visitorgroupusageviewer/usedonthiscontent";
            model.UsedOnReferencedContent.LanguageKey = "/widgets/visitorgroupusageviewer/usedonreferencedcontent";

            if (this.Request.QueryString["vg"] != null)
            {
                model.UsedOnThisContent.SelectedVisitorGroup = this.Request.QueryString["vg"].ToString();
                model.UsedOnReferencedContent.SelectedVisitorGroup = this.Request.QueryString["vg"].ToString();
            }

            foreach (var group in visitorGroupUsage.GetVisitorGroupsForContent(content).OrderBy(x => x.Value))
            {
                var vg = new Guid(group.Key);
                if (group.Value == 0)
                {
                    var loadedVg = visitorGroupRepo.Load(vg);
                    if (loadedVg != null)
                    {
                        model.UsedOnThisContent.UsedVisitorGroups.Add(group.Key, visitorGroupRepo.Load(vg).Name);
                    }
                }
                else
                {
                    var loadedVg = visitorGroupRepo.Load(vg);
                    if (loadedVg != null)
                    {
                        model.UsedOnReferencedContent.UsedVisitorGroups.Add(group.Key, visitorGroupRepo.Load(vg).Name);
                    }
                }
            }

            return model;
        }
    }
}
