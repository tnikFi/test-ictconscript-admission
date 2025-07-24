using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.Design;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // Get the configuration base path by the Web project's location
        // Makes managing migrations with the EF Core CLI a little bit simpler
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Web");
        var secretId = GetUserSecretsId(basePath, "Web");

        var builder = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json");

        if (secretId != null)
            builder.AddUserSecrets(secretId);

        var configuration = builder.Build();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlite(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }

    /// <summary>
    ///     Get the UserSecretsId from the given project path and project name.
    ///     Needed to get the UserSecrets from the project without including a reference to it to avoid circular
    ///     dependencies.
    /// </summary>
    /// <param name="projectPath"></param>
    /// <param name="projectName"></param>
    /// <returns></returns>
    private static string? GetUserSecretsId(string projectPath, string projectName)
    {
        var projectFilePath = Path.Combine(projectPath, $"{projectName}.csproj");
        var projectFile = File.ReadAllText(projectFilePath);
        var csproj = XDocument.Parse(projectFile);

        // Get the UserSecretsId from the project file
        var userSecretsId = csproj.Descendants()
            .FirstOrDefault(x => x.Name.LocalName == "UserSecretsId")?.Value;

        return userSecretsId;
    }
}