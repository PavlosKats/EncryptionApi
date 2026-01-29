using Microsoft.Extensions.FileProviders;

public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        var app = builder.Build();

        // Log current directory and files for debugging
        var currentDir = Directory.GetCurrentDirectory();
        app.Logger.LogInformation($"Current directory: {currentDir}");
        
        if (File.Exists(Path.Combine(currentDir, "Index.html")))
        {
            app.Logger.LogInformation("Index.html found in current directory");
        }
        else
        {
            app.Logger.LogWarning("Index.html NOT found in current directory");
        }

        app.UseCors();

        app.UseDefaultFiles();
        
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(currentDir),
            RequestPath = ""
        });

        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}


