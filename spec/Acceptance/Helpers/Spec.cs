using System;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;

public class Spec {

    public string CurrentDirectory { get; set; }

    string _temporaryDirectory;
    public string TemporaryDirectory {
        get {
            if (_temporaryDirectory is null) {
                _temporaryDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                CurrentDirectory = _temporaryDirectory;
            }
            return _temporaryDirectory;
        }
    }
    public bool TemporaryDirectoryExists { get => _temporaryDirectory is not null && Directory.Exists(_temporaryDirectory); }

    [SetUp]
    public void SetUp() {
        //
    }

    [TearDown]
    public void TearDown() {
        if (TemporaryDirectoryExists) Directory.Delete(TemporaryDirectory, recursive: true);
    }
    
    public Project CreateProject(string name = null, int csharp = 0, Project.TargetFrameworks framework = Project.TargetFrameworks.Net50, Project.OutputTypes type = Project.OutputTypes.Library, bool includeMiniSpec = true, string packageName = null, string packageOutputPath = null, string packagesFolder = null, string assemblyName = null) {
        if (name is null) name = Guid.NewGuid().ToString();
        var projectPath = Path.Combine(TemporaryDirectory, name);
        var project = new Project(projectDirectory: projectPath, csharpVersion: csharp, targetFramework: framework, outputType: type);
        project.CheckPlatformCompatibilityOrSkip(framework);
        project.CreateProjectFile(includeMiniSpec: includeMiniSpec, packageName: packageName, packageOutputPath: packageOutputPath, assemblyName: assemblyName);
        project.CreateNuGetConfigFile(packagesFolder: packagesFolder);
        return project;
    }

    public string ChangeDirectory(string relativePath) {
        if (CurrentDirectory is null) return TemporaryDirectory;
        else return FilePath(relativePath);
    }

    public string CreateDirectory(string relativePath) {
    var x = TemporaryDirectory; // Kick this property. TODO update to not have to kick it like this.
    var directoryPath = FilePath(relativePath);
        Directory.CreateDirectory(directoryPath);
        return directoryPath;
    }

    public string FilePath(string relativePath) {
        if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
            return Path.Combine(CurrentDirectory, relativePath.Replace("\\", "/"));
        else
            return Path.Combine(CurrentDirectory, relativePath.Replace("/", "\\"));
    }

    public CommandResult RunCommand(string commandName, params string[] arguments) => Command.Run(commandName, CurrentDirectory, arguments);
}