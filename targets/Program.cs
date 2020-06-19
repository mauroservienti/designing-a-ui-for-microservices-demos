using System;
using System.IO;
using System.Runtime.InteropServices;
using static Bullseye.Targets;
using static SimpleExec.Command;

internal class Program
{
    public static void Main(string[] args)
    {
        var sdk = new DotnetSdkManager();

        Target("default", DependsOn("Test-Demo-01", "Test-Demo-02", "Demo-03", "Demo-04"));

        Target(
            "Build-Demo-01",
            Directory.EnumerateFiles("ASP.Net Core API Gateway - 01", "*.sln", SearchOption.AllDirectories),
            solution => Run(sdk.GetDotnetCliPath(), $"build \"{solution}\" --configuration Release"));

        Target(
            "Test-Demo-01", DependsOn("Build-Demo-01"),
            Directory.EnumerateFiles("ASP.Net Core API Gateway - 01", "*.Tests.csproj", SearchOption.AllDirectories),
            proj => Run(sdk.GetDotnetCliPath(), $"test \"{proj}\" --configuration Release --no-build"));

        Target(
           "Build-Demo-02",
            Directory.EnumerateFiles("ASP.Net Core API Gateway - 02", "*.sln", SearchOption.AllDirectories),
            solution => Run(sdk.GetDotnetCliPath(), $"build \"{solution}\" --configuration Release"));

	Target(
            "Test-Demo-02", DependsOn("Build-Demo-02"),
            Directory.EnumerateFiles("ASP.Net Core API Gateway - 02", "*.Tests.csproj", SearchOption.AllDirectories),
            proj => Run(sdk.GetDotnetCliPath(), $"test \"{proj}\" --configuration Release --no-build"));

        Target(
            "Demo-03",
            Directory.EnumerateFiles("ASP.Net Mvc Core", "*.sln", SearchOption.AllDirectories),
            solution => Run(sdk.GetDotnetCliPath(), $"build \"{solution}\" --configuration Release"));

        Target(
            "Demo-04",
            Directory.EnumerateFiles("ASP.Net Mvc Core UI Composition", "*.sln", SearchOption.AllDirectories),
            solution => Run(sdk.GetDotnetCliPath(), $"build \"{solution}\" --configuration Release"));

        RunTargetsAndExit(args);
    }
}
