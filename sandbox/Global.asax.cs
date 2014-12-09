using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.WebPages;

namespace sandbox
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            DisplayModeProvider.Instance.Modes.Insert(0, new MobileDisplayMode());
        }
    }
    public class MobileDisplayMode : DefaultDisplayMode
    {
        private readonly StringCollection _useragenStringPartialIdentifiers = new StringCollection
    {
        "Android",
        "Mobile",
        "Opera Mobi",
        "Samsung",
        "HTC",
        "Nokia",
        "Ericsson",
        "SonyEricsson",
        "iPhone"
    };

        public MobileDisplayMode()
            : base("Mobile")
        {
            ContextCondition = (context => IsMobile(context.GetOverriddenUserAgent()));
        }

        private bool IsMobile(string useragentString)
        {
            return _useragenStringPartialIdentifiers.Cast<string>()
                        .Any(val => useragentString.IndexOf(val, StringComparison.InvariantCultureIgnoreCase) >= 0);
        }
    }
}