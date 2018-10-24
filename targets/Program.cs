using System;
using System.Diagnostics;
using System.IO;
using static Bullseye.Targets;

internal class Program
{
    public static void Main(string[] args)
    {
        Target("default", DependsOn("Demo-01", "Demo-02", "Demo-03", "Demo-04"));

        Target(
            "Demo-01",
            Directory.EnumerateFiles("ASP.Net Core API Gateway - 01", "*.sln", SearchOption.AllDirectories),
            solution => Cmd("dotnet", $"build \"{solution}\" --configuration Debug"));

        Target(
            "Demo-02",
            Directory.EnumerateFiles("ASP.Net Core API Gateway - 02", "*.sln", SearchOption.AllDirectories),
            solution => Cmd("dotnet", $"build \"{solution}\" --configuration Debug"));

        Target(
            "Demo-03",
            Directory.EnumerateFiles("ASP.Net Mvc Core", "*.sln", SearchOption.AllDirectories),
            solution => Cmd("dotnet", $"build \"{solution}\" --configuration Debug"));

        Target(
            "Demo-04",
            Directory.EnumerateFiles("ASP.Net Mvc Core UI Composition", "*.sln", SearchOption.AllDirectories),
            solution => Cmd("dotnet", $"build \"{solution}\" --configuration Debug"));

        RunTargets(args);
    }

    private static void Cmd(string fileName, string args)
    {
        using (var process = new Process())
        {
            process.StartInfo = new ProcessStartInfo
            {
                FileName = $"\"{fileName}\"",
                Arguments = args,
                UseShellExecute = false,
            };

            Console.WriteLine($"Running '{process.StartInfo.FileName} {process.StartInfo.Arguments}'...");
            process.Start();

            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException($"The command exited with code {process.ExitCode}.");
            }
        }
    }
}
