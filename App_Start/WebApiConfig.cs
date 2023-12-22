using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace NaijaQuickFix
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // var setting = config.Formatters.JsonFormatter.SerializerSettings;
            // setting.ContractResolver = new CamelCasePropertyNamesContractResolver();
            // GlobalConfiguration.Configuration.Formatters.Add
            //(new System.Net.Http.m FormMultipartEncodedMediaTypeFormatter());


            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
                .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters
                .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
