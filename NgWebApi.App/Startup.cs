
using Microsoft.Owin;

[assembly: OwinStartup(typeof(NgWebApi.App.Startup))]

namespace NgWebApi.App
{
    using System.Web.Http;

    using Microsoft.Owin;
    using Microsoft.Owin.Extensions;
    using Microsoft.Owin.FileSystems;
    using Microsoft.Owin.StaticFiles;

    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //http://www.strathweb.com/2014/04/ignoring-routes-asp-net-web-api/
            //var config = new HttpConfiguration();
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute("Html", "{whatever}.html/{*pathInfo}", null,
            //        null, new StopRoutingHandler());
            //config.Routes.MapHttpRoute("FilesRoute", "files/{*pathInfo}", null,
            //        null, new StopRoutingHandler());
            //config.Routes.MapHttpRoute("DefaultApi", "{controller}/{id}", new { id = RouteParameter.Optional });

            //appBuilder.UseWebApi(config);
            //appBuilder.UseFileServer(new FileServerOptions()
            //{
            //    RequestPath = new PathString("/files"),
            //    EnableDirectoryBrowsing = true
            //});

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //app.UseWebApi(config);

            //// Make ./app the default root of the static files in our Web Application.
            //app.UseFileServer(new FileServerOptions
            //{
            //    RequestPath = new PathString(string.Empty),
            //    FileSystem = new PhysicalFileSystem("./app"),
            //    EnableDirectoryBrowsing = true
            //});

            //app.UseStageMarker(PipelineStage.MapHandler);
        }
    }
}
