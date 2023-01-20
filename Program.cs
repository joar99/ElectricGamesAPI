using GameApiTwo.Services;
using GamesApi.Models;
using GamesApi.Services;
using Microsoft.Extensions.FileProviders;
using System.Linq.Expressions;
//using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("GameDatabase"));

builder.Services.AddSingleton<GamesService>();
builder.Services.AddSingleton<CharacterService>();
builder.Services.AddSingleton<DeveloperService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//allow cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

DefaultFilesOptions options = new DefaultFilesOptions();
//options.DefaultFileNames.Add("*index.html");
app.UseDefaultFiles(options);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    /*app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, ""))
    });*/
}

/*try
{
    app.UseFileServer(new FileServerOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "images")),
        RequestPath = "/images"
    });
} catch (Exception e)
{
    Console.WriteLine(e);
}*/

app.UseStaticFiles();

app.UseCors("corsapp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
