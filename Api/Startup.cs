using Microsoft.Owin;
using Api;
using Techsson.Gaming.ControlPanel.Core;

[assembly: OwinStartup(typeof(Startup))]
namespace Api
{
    using System.Web.Http;
    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();

            // Configure Web API Routes:
            // - Enable Attribute Mapping
            // - Enable Default routes at /api.
            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { constroller = "Root", action = "Get", id = RouteParameter.Optional }
            );

            httpConfiguration.Formatters.JsonFormatter.SerializerSettings = JsonFormatterSettingsProvider.CreateJsonSettings();

            app.UseWebApi(httpConfiguration);
        }
    }
}
