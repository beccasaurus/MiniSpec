using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class Project {

  public static List<TargetFrameworks> WindowsOnlyFrameworks {
      get {
      if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("MINISPEC_FRAMEWORKS"))) {
        return new() {
          TargetFrameworks.Core10, // This is basically just for my personal development  // I simply haven't been able to
          TargetFrameworks.Core11, // and we'll clean this up and get it working          // personally get these versions
          TargetFrameworks.Net20,  // for everyone, e.g. make this configurable           // working on my Ubuntu setup.
          TargetFrameworks.Net35   // (tho it already is via the environment variable)    //
        //   TargetFrameworks.Net452
        };
      } else {
        return new() { }; // If frameworks have been explicitly set, don't ignore anything, run them all
      }
    }
  }


  public void CheckPlatformCompatibilityOrSkip(TargetFrameworks framework) {
    if (! RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        if (WindowsOnlyFrameworks.Contains(framework))
            NUnit.Framework.Assert.Ignore($"{framework} is only supported on Windows");

    if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("MINISPEC_FRAMEWORKS"))) {
      var desiredFrameworkNames = Environment.GetEnvironmentVariable("MINISPEC_FRAMEWORKS").Split(";");
      var desiredFrameworks = new List<TargetFrameworks>();
      foreach (var desiredFramework in desiredFrameworkNames) {
        var found = false;
        foreach (var targetFramework in TargetFrameworkNames) {
            if (targetFramework.Value == desiredFramework) {
            found = true;
            desiredFrameworks.Add(targetFramework.Key);
          }
        }
        if (! found) throw new Exception($"User wants to run framework {desiredFramework} but that is not a defined target framework {TargetFrameworkNames}");
      }
      if (! desiredFrameworks.Contains(framework))
        NUnit.Framework.Assert.Ignore($"Skip framework {framework} - only running: {Environment.GetEnvironmentVariable("MINISPEC_FRAMEWORKS")}");
    }
  }

  public enum OutputTypes { Library, Exe }
    public enum TargetFrameworks { Net50, Net48, Net462, Net452, Net40, Net35, Net20, Core10, Core11, Core21, Core22, Core31, Standard10, Standard20, Standard21 }

    public Dictionary<TargetFrameworks,string> TargetFrameworkNames = new() {
        { TargetFrameworks.Net50, "net50" },
        { TargetFrameworks.Net48, "net48" },
        { TargetFrameworks.Net462, "net462" },
        { TargetFrameworks.Net452, "net452" },
        { TargetFrameworks.Net40, "net40" },
        { TargetFrameworks.Net35, "net35" },
        { TargetFrameworks.Net20, "net20" },
        { TargetFrameworks.Core10, "netcoreapp1.0" },
        { TargetFrameworks.Core11, "netcoreapp1.1" },
        { TargetFrameworks.Core21, "netcoreapp2.1" },
        { TargetFrameworks.Core22, "netcoreapp2.2" },
        { TargetFrameworks.Core31, "netcoreapp3.1" },
        { TargetFrameworks.Standard10, "netstandard1.0" },
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