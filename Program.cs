using Ecommerce.Extensions;
using Microsoft.Extensions.FileProviders;

using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDB(builder);
builder.Services.AddCustomServices();
builder.Services.AddValidationServices();
builder.Services.AddCustomAuth(builder);
builder.Services.AddCustomIdentity();

builder.Services.AddDirectoryBrowser();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var fileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Media"));
var requestPath = "/MyImages";

app.UseCors("MyAllowSpecificOrigins");

// Enable displaying browser links.
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = fileProvider,
    RequestPath = requestPath
});

app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = fileProvider,
    RequestPath = requestPath
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
