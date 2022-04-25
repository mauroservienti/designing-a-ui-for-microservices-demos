using System.IO;
using System.Threading.Tasks;
using static Bullseye.Targets;
using static SimpleExec.Command;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var sdk = new DotnetSdkManager();
        var dotnet = await sdk.GetDotnetCliPath();

        Target("default", DependsOn("Test-Demo-01", "Test-Demo-02", "Test-Demo-03", "Test-Demo-04"));

        Target(
            "Build-Demo-01",
            Directory.EnumerateFiles("ASP.Net Core API Gateway - 01", "*.sln", SearchOption.AllDirectories),
            solution => Run(dotnet, $"build \"{solution}\" --configuration Release"));

        Target(
            "Test-Demo-01", DependsOn("Build-Demo-01"),
            Directory.EnumerateFiles("ASP.Net Core API Gateway - 01", "*.Tests.csproj", SearchOption.AllDirectories),
            proj => Run(dotnet, $"test \"{proj}\" --configuration Release --no-build"));

        Target(
           "Build-Demo-02",
            Directory.EnumerateFiles("ASP.Net Core API Gateway - 02", "*.sln", SearchOption.AllDirectories),
            solution => Run(dotnet, $"build \"{solution}\" --configuration Release"));

	    Target(
            "Test-Demo-02", DependsOn("Build-Demo-02"),
            Directory.EnumerateFiles("ASP.Net Core API Gateway - 02", "*.Tests.csproj", SearchOption.AllDirectories),
            proj => Run(dotnet, $"test \"{proj}\" --configuration Release --no-build"));

        Target(
            "Build-Demo-03",
            Directory.EnumerateFiles("ASP.Net Mvc Core", "*.sln", SearchOption.AllDirectories),
            solution => Run(dotnet, $"build \"{solution}\" --configuration Release"));

        Target(
            "Test-Demo-03", DependsOn("Build-Demo-03"),
            Directory.EnumerateFiles("ASP.Net Mvc Core", "*.Tests.csproj", SearchOption.AllDirectories),
            proj => Run(dotnet, $"test \"{proj}\" --configuration Release --no-build"));

        Target(
            "Build-Demo-04",
            Directory.EnumerateFiles("ASP.Net Mvc Core UI Composition", "*.sln", SearchOption.AllDirectories),
            solution => Run(dotnet, $"build \"{solution}\" --configuration Release"));
        
        Target(
            "Test-Demo-04", DependsOn("Build-Demo-04"),
            Directory.EnumerateFiles("ASP.Net Mvc Core UI Composition", "*.Tests.csproj", SearchOption.AllDirectories),
            proj => Run(dotnet, $"test \"{proj}\" --configuration Release --no-build"));

        await RunTargetsAndExitAsync(args);
    }
}
