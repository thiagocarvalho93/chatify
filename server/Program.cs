using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDirectoryBrowser();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseStaticFiles();
// Access websocket file via url
// var fileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.WebRootPath, "ws-test"));
// var requestPath = "/ws-test";

// Enable displaying browser links.

// app.UseStaticFiles(new StaticFileOptions
// {
//     FileProvider = fileProvider,
//     RequestPath = requestPath
// });

// app.UseDirectoryBrowser(new DirectoryBrowserOptions
// {
//     FileProvider = fileProvider,
//     RequestPath = requestPath
// });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2)
};
webSocketOptions.AllowedOrigins.Add("*");
app.UseWebSockets(webSocketOptions);

app.MapControllers();

app.Run();
