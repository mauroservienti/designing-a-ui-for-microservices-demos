using System;
using System.Diagnostics;
using System.IO;
using static Bullseye.Targets;
using static SimpleExec.Command;

internal class Program
{
    public static void Main(string[] args)
    {
        Target("default", DependsOn("Demo-01", "Demo-02", "Demo-03", "Demo-04"));

        Target(
            "Demo-01",
            Directory.EnumerateFiles("ASP.Net Core API Gateway - 01", "*.sln", SearchOption.AllDirectories),
            solution => Run("dotnet", $"build \"{solution}\" --configuration Debug"));

        Target(
            "Demo-02",
            Directory.EnumerateFiles("ASP.Net Core API Gateway - 02", "*.sln", SearchOption.AllDirectories),
            solution => Run("dotnet", $"build \"{solution}\" --configuration Debug"));

        Target(
            "Demo-03",
            Directory.EnumerateFiles("ASP.Net Mvc Core", "*.sln", SearchOption.AllDirectories),
            solution => Run("dotnet", $"build \"{solution}\" --configuration Debug"));

        Target(
            "Demo-04",
            Directory.EnumerateFiles("ASP.Net Mvc Core UI Composition", "*.sln", SearchOption.AllDirectories),
            solution => Run("dotnet", $"build \"{solution}\" --configuration Debug"));

        RunTargets(args);
    }
}
