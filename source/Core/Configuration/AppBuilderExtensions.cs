﻿
using Autofac;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.StaticFiles;
using Thinktecture.IdentityServer.Core;

namespace Owin
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder UseIdentityServerCore(this IAppBuilder app, IdentityServerCoreOptions options)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions { AuthenticationType = "idsrv" });

            app.UseFileServer(new FileServerOptions
            {
                RequestPath = new PathString("/assets"),
                FileSystem = new EmbeddedResourceFileSystem(typeof(Constants).Assembly, "Thinktecture.IdentityServer.Core.Authentication.Assets")
            });
            app.UseStageMarker(PipelineStage.MapHandler);

            app.UseFileServer(new FileServerOptions
            {
                RequestPath = new PathString("/assets/libs/fonts"),
                FileSystem = new EmbeddedResourceFileSystem(typeof(Constants).Assembly, "Thinktecture.IdentityServer.Core.Authentication.Assets.libs.bootstrap.fonts")
            });
            app.UseStageMarker(PipelineStage.MapHandler);

            var container = AutoFacConfig.Configure(options.Factory);

            app.Use(async (ctx, next) =>
            {
                // this creates a per-request, disposable scope
                using (var scope = container.BeginLifetimeScope(b =>
                {
                    // this makes owin context resolvable in the scope
                    b.RegisterInstance(ctx).As<IOwinContext>();
                }))
                {
                    // this makes scope available for downstream frameworks
                    ctx.Set<ILifetimeScope>("idsrv:AutofacScope", scope);
                    await next();
                }
            }); 
            
            app.UseWebApi(WebApiConfig.Configure(options));

            return app;
        }


    }
}