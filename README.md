# ASP.NET WebAPI OWIN for Angular-Cli

This is an ASP.NET WebAPI appliction template designed to host Angular2 SPA in **/App** directory as complete single application read for deployment.

# NgWebApi.App project

This is an ASP.NET WebAPI appliction template designed to host Angular2 SPA in **/App** directory. 
It can be deployed alone. There is no need to deploy a separate Angular project.

## IIS ASP.NET WebApi into Owin.WebApi transition steps

```
Uninstall-Package Microsoft.AspNet.WebApi
Uninstall-Package Microsoft.AspNet.WebApi.WebHost
Install-Package Microsoft.Owin
Install-Package Microsoft.Owin.StaticFiles
Install-Package Microsoft.Owin.Host.SystemWeb
Install-Package Microsoft.AspNet.WebApi.Owin
```    
Delete following files:
```
App_Start\WebApiConfig.cs
Global.asax*
```
Add *OWIN Startup file* `Startup.cs` 
```
public void Configuration(IAppBuilder app)
{
    var httpConfiguration = new HttpConfiguration();

    // Configure Web API Routes:
    // - Enable Attribute Mapping
    // - Enable Default routes at /api.
    httpConfiguration.MapHttpAttributeRoutes();
    httpConfiguration.Routes.MapHttpRoute(
        name: "DefaultApi",
        routeTemplate: "api/{controller}/{id}",
        defaults: new { id = RouteParameter.Optional }
    );

    app.UseWebApi(httpConfiguration);

    // Make ./app the default root of the static files in our Web Application.
    app.UseFileServer(new FileServerOptions
    {
        RequestPath = new PathString(string.Empty),
        FileSystem = new PhysicalFileSystem("./app"),
        EnableDirectoryBrowsing = true,
    });

    app.UseStageMarker(PipelineStage.MapHandler);
}
```

Modify `Web.config` and replace `<system.webServer>` section:
```
<system.webServer>
    <!-- runAllManagedModulesForAllRequests: Make sure that we have OWIN handle static files, too. -->
    <modules runAllManagedModulesForAllRequests="true" />

    <!-- Disable all static content handling in the IIS -->
    <staticContent>
        <clear />
    </staticContent>

    <!-- Remove all handlers -->
    <handlers>
        <clear />
    </handlers>
</system.webServer>
```
Required `packages.config`
```
<packages>
  <package id="Microsoft.AspNet.WebApi.Client" version="5.2.3" targetFramework="net452" />
  <package id="Microsoft.AspNet.WebApi.Core" version="5.2.3" targetFramework="net452" />
  <package id="Microsoft.AspNet.WebApi.Owin" version="5.2.3" targetFramework="net452" />
  <package id="Microsoft.Owin" version="3.0.1" targetFramework="net452" />
  <package id="Microsoft.Owin.FileSystems" version="3.0.1" targetFramework="net452" />
  <package id="Microsoft.Owin.Host.SystemWeb" version="3.0.1" targetFramework="net452" />
  <package id="Microsoft.Owin.StaticFiles" version="3.0.1" targetFramework="net452" />
  <package id="Newtonsoft.Json" version="9.0.1" targetFramework="net452" />
  <package id="Owin" version="1.0" targetFramework="net452" />
</packages>
```
## Owin Self-Host version
In order to make the application OWIN Self-hosted run the following commands:
```
Uninstall-Package Microsoft.Owin.Host.SystemWeb
Install-Package OwinHost
```

OWIN example based on [OWIN ASP.NET WebAPI SPA Template Visual Studio Extension](https://marketplace.visualstudio.com/items?itemName=OliverLohmann-MSFT.OWINASPNETWebAPISPATemplate])

---

# IIS ASP.NET WebApi with IIS rewrite rules
This another version using IIS. It is in a separate branch **iisRewriteRules**. It doesn't use OWIN, it's simple 'Empty ASP.NET Web Application' template with 'WebApi' checkbox ticked.

```
git checkout iisRewriteRules
```

```
<packages>
  <package id="Microsoft.AspNet.WebApi" version="5.2.3" targetFramework="net452" />
  <package id="Microsoft.AspNet.WebApi.Client" version="5.2.3" targetFramework="net452" />
  <package id="Microsoft.AspNet.WebApi.Core" version="5.2.3" targetFramework="net452" />
  <package id="Microsoft.AspNet.WebApi.WebHost" version="5.2.3" targetFramework="net452" />
  <package id="Microsoft.CodeDom.Providers.DotNetCompilerPlatform" version="1.0.0" targetFramework="net452" />
  <package id="Microsoft.Net.Compilers" version="1.0.0" targetFramework="net452" developmentDependency="true" />
  <package id="Newtonsoft.Json" version="6.0.4" targetFramework="net452" />
</packages>
```

### IIS rewrite rules in web.config
```
<system.webServer>
    <defaultDocument enabled="true">
        <files>
            <clear />
            <add value="app/index.html" />
        </files>
    </defaultDocument>
    <rewrite>
        <rules>
            <rule name="static dist files" stopProcessing="true">
                <match url="^(.+)$" />
                <conditions>
                    <add input="{APPL_PHYSICAL_PATH}app\{R:1}" matchType="IsFile" />
                </conditions>
                <action type="Rewrite" url="/app/{R:1}" />
            </rule>
            <rule name="index.html as document root" stopProcessing="true">
                <match url="^$" />
                <action type="Rewrite" url="/app/index.html" />
            </rule>
        </rules>
    </rewrite>
<system.webServer>
```
Keep `Global.asax` and `App_Start\WebApiConfig.cs`


# NgWebApi.Angular project
This project was generated with [angular-cli](https://github.com/angular/angular-cli) tool.

## Steps during frontend (angular) development
* Open solution `NgWebApi.sln` in Visual Studio and run (Ctrl+F5) `NgWebApi.App` project
* `> cd .\NgWebApi.Angular` __`> npm start`__ which starts __`ng serve`__
* Navigate to [http://localhost:4200/](http://localhost:4200/) - this is the _NgWebApi.Angular_ URL created by __`ng serve`__ 
which is configured in `proxy.conf.json` to pass all the API requests to the running ASP.NET WebApi application on port __:58519__.
The `ng serve` does not create any files on disk and everything is served from memory. The app will automatically reload if you change any of the source files.
* __`ng build`__ command will transpile and bundle all needed files and copy everything, including static files, to `App` folder of the _NgWebApi.App_ application which can be then deployed to server. Use the `-prod` flag for a production build.
