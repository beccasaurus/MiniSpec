using System.IO;
using System.Collections.Generic;

public class Project {

    public enum OutputTypes { Library, Exe }
    public enum TargetFrameworks { Net50, Core21, Core31, Standard20, Standard21 }

    public Dictionary<TargetFrameworks,string> TargetFrameworkNames = new() {
        { TargetFrameworks.Net50, "net50" },
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

    public void CreateProjectFile() {
        WriteFile("Project.csproj", GetProjectFileText());
    }

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

    public CommandResult RunCommand(string commandName, params string[] arguments) => Command.Run(commandName, ProjectDirectory, arguments);

    public CommandResult BuildResult { get; set; }
    public CommandResult TestResult { get; set; }

    public CommandResult Build() {
        BuildResult = RunCommand("dotnet", "build");
        return BuildResult;
    }

    public CommandResult RunTests() {
        TestResult = RunCommand("dotnet", "test");
        return TestResult;
    }

    string GetProjectFileText() => $@"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>{TargetFrameworkName}</TargetFramework>
    <OutputType>{OutputType}</OutputType>
    <LangVersion>{CsharpVersion}</LangVersion>
  </PropertyGroup>
</Project>
";
}