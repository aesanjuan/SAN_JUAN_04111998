using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using short_clips_web_api;
using short_clips_web_api.Interfaces;
using short_clips_web_api.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        // configure the max file size of a file globally to accept up to 100MB
        var maxFileSizeMB = 100 * 1024 * 1024;        
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.Limits.MaxRequestBodySize = maxFileSizeMB;
        });
        builder.Services.Configure<IISServerOptions>(options =>
        {
            options.MaxRequestBodySize = maxFileSizeMB;
        });
        builder.Services.Configure<KestrelServerOptions>(options =>
        {
            options.Limits.MaxRequestBodySize = maxFileSizeMB;
        });
        builder.Services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = maxFileSizeMB;
        });
        builder.Services.Configure<HttpSysOptions>(options =>
        {
            options.MaxRequestBodySize = maxFileSizeMB;
        });

        ConfigurationManager configuration = builder.Configuration;
        builder.Services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

        // add services
        builder.Services.AddScoped<ICategoriesService, CategoriesService>();
        builder.Services.AddScoped<IShortClipsService, ShortClipsService>();

        // add entity framework
        var connectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}