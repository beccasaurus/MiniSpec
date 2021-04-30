using System;
using System.IO;
using System.Collections.Generic;

public class Project {

    public enum OutputTypes { Library, Exe }
    public enum TargetFrameworks { Net50, Net48, Net20, Core21, Core31, Standard20, Standard21 }

    public Dictionary<TargetFrameworks,string> TargetFrameworkNames = new() {
        { TargetFrameworks.Net50, "net50" },
        { TargetFrameworks.Net48, "net48" },
        { TargetFrameworks.Net20, "net20" },
        { TargetFrameworks.Core21, "netcoreapp2.1" },
        { TargetFrameworks.Core31, "netcoreapp3.1" },
        { TargetFrameworks.Standard20, "netstandard2.0" },
        { TargetFrameworks.Standard21, "netstandard2.1" },
    };

    int _csharpVersion = 9;
    TargetFrameworks _targetFramework;
    OutputTypes _outputType;

    public string ProjectDirectory { get; init; }
    public int CsharpVersion { get => _csharpVersion; }
    public TargetFrameworks TargetFramework { get => _targetFramework; }
    public string TargetFrameworkName { get => TargetFrameworkNames[TargetFramework]; }
    public OutputTypes OutputType { get => _outputType; }

    public Project(string projectDirectory, int csharpVersion = 9, TargetFrameworks targetFramework = TargetFrameworks.Net50, OutputTypes outputType = OutputTypes.Library) {
        ProjectDirectory = projectDirectory;
        _csharpVersion = csharpVersion;
        _targetFramework = targetFramework;
        _outputType = outputType;
    }

    public void CreateProjectFile() { WriteFile("Project.csproj", GetProjectFileText()); }
    public void CreateNuGetConfigFile() { WriteFile("nuget.config", GetNuGetConfigText()); }

    public void WriteFile(string relativePath, string content = "") {
        var fullPath = FilePath(relativePath);
        var parentFolder = Directory.GetParent(fullPath).ToString();
        if (! Directory.Exists(parentFolder)) Directory.CreateDirectory(parentFolder);
        File.WriteAllText(fullPath, content);
    }

    public string FilePath(string relativePath) {
        if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
            return Path.Combine(ProjectDirectory, relativePath.Replace("\\", "/"));
        else
            return Path.Combine(ProjectDirectory, relativePath.Replace("/", "\\"));
    }

    public CommandResult RunCommand(string commandName, params string[] arguments) {
        LastCommandResult = Command.Run(commandName, ProjectDirectory, arguments);
        return LastCommandResult;
    }

    public CommandResult LastCommandResult { get; set; }
    public CommandResult BuildResult { get; set; }
    public CommandResult TestResult { get; set; }
    public CommandResult RunResult { get; set; }

    public CommandResult Build() {
        BuildResult = RunCommand("dotnet", "build");
        return BuildResult;
    }

    public CommandResult Run(params string[] arguments) {
        List<string> args = new() { "run" };
        args.AddRange(arguments);
        RunResult = RunCommand("dotnet", args.ToArray());
        return RunResult;
    }

    public CommandResult Test() {
        TestResult = RunCommand("dotnet", "test");
        return TestResult;
    }

    string GetProjectFileText() => $@"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>{TargetFrameworkName}</TargetFramework>
    <OutputType>{OutputType}</OutputType>
    {GetLangVersionText()}
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include=""MiniSpec"" Version=""1.0.0"" />
  </ItemGroup>
</Project>
";

    string GetLangVersionText() => CsharpVersion > 0 ? $"<LangVersion>{CsharpVersion}</LangVersion>" : "";

    string GetNuGetConfigText() {
        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("NUGET_PACKAGES")))
            throw new Exception("Please set NUGET_PACKAGES to the path to a global package directory for MiniSpec tests");
        return $@"<?xml version=""1.0"" encoding=""utf-8""?>
<configuration>
    <packageSources>
        <add key=""GlobalPackages"" value=""{Environment.GetEnvironmentVariable("NUGET_PACKAGES")}"" />
    </packageSources>
</configuration>
";
    }
}