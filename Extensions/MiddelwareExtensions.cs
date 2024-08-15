using Microsoft.Extensions.FileProviders;
using System.Runtime.CompilerServices;

namespace Ecommerce.Extensions
{
    public static class MiddelwareExtensions
    {
        public static void AddMediaMiddelwares(this IApplicationBuilder application , WebApplicationBuilder builder)
        {
            var fileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Media"));
            var requestPath = "/MyImages";

            application.UseCors("MyAllowSpecificOrigins");

            // Enable displaying browser links.
            application.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = fileProvider,
                RequestPath = requestPath
            });

            application.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = fileProvider,
                RequestPath = requestPath
            });
        }



    }
}
