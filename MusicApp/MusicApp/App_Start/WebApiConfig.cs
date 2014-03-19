using MusicApp.Data;
using Newtonsoft.Json;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData.Builder;

namespace MusicApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<Song>("Songs");
            modelBuilder.EntitySet<Artist>("Artists");
            modelBuilder.EntitySet<Album>("Albums");

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

            var model = modelBuilder.GetEdmModel();
            config.Routes.MapODataRoute("odata", "odata", model);
            config.EnableQuerySupport();
        }
    }
}
