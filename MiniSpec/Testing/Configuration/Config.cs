using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using MiniSpec.Testing.Extensibility;

namespace MiniSpec.Testing.Configuration {
  public class Config : IConfig
  {
    static Type _defaultClass = typeof(MiniSpecConfig);
    public static Type DefaultClass { get => _defaultClass; }
    public static void SetDefaultClass<T>(T defaultConfigClass) where T : IConfig => _defaultClass = typeof(T);
    public static IConfig GetInstance() {
      try {
        object? configObject = Activator.CreateInstance(DefaultClass);
        if (configObject is null) {
          #if NO_GET_TYPE_INFO_AVAILABLE
          var assembly = Assembly.GetAssembly(DefaultClass);
          #else
          var assembly = DefaultClass.GetTypeInfo().Assembly;
          #endif
          var assemblyLocation = (assembly is null) ? "" : $" from {assembly.Location}";
          throw new NullReferenceException($"Failed to initialize provided Config class {DefaultClass.FullName}{assemblyLocation}");
        }
        var config = configObject as IConfig;
        if (config is null) {
          #if NO_GET_TYPE_INFO_AVAILABLE
          var assembly = Assembly.GetAssembly(DefaultClass);
          #else
          var assembly = DefaultClass.GetTypeInfo().Assembly;
          #endif
          var assemblyLocation = (assembly is null) ? "" : $" from {assembly.Location}";
          throw new NullReferenceException($"Could not construct valid IConfig from provided Config class {DefaultClass.FullName}{assemblyLocation}");
        }
        return config;
      } catch (Exception e) {
        #if NO_GET_TYPE_INFO_AVAILABLE
        var assembly = Assembly.GetAssembly(DefaultClass);
        #else
        var assembly = DefaultClass.GetTypeInfo().Assembly;
        #endif
        var assemblyLocation = (assembly is null) ? "" : $" from {assembly.Location}";
        throw new Exception($"Error when attempting to initialize provided Config class {DefaultClass.FullName}{assemblyLocation}", e);
      }
    }
    public static IConfig GetInstanceWithDefaults() => SetDefaults(GetInstance());
    public static IConfig SetDefaults(IConfig config) => EnvironmentVariables.SetValues(Defaults.SetValues(config));

    TextWriter _standardOutput = new StringWriter();
    TextWriter _standardError = new StringWriter();
    IList<string> _assemblyPaths = new List<string>();
    ExtensionRegistry _extensibilityRegistry = new ExtensionRegistry();
    IDictionary<string, object> _meta = new Dictionary<string, object>();
    IList<Regex> _testNamePatterns = new List<Regex>();
    IList<Regex> _testGroupPatterns = new List<Regex>();
    IList<Regex> _specGroupPatterns = new List<Regex>();
    IList<Regex> _testNameWithinGroupPatterns = new List<Regex>();
    IList<Regex> _setupPatterns = new List<Regex>();
    IList<Regex> _teardownPatterns = new List<Regex>();
    IList<Regex> _suiteSetupPatterns = new List<Regex>();
    IList<Regex> _suiteTeardownPatterns = new List<Regex>();
    IList<Regex> _globalSetupPatterns = new List<Regex>();
    IList<Regex> _globalTeardownPatterns = new List<Regex>();

    public TextWriter StandardOutput { get => _standardOutput; set => _standardOutput = value; }
    public TextWriter StandardError { get => _standardError; set => _standardError = value; }
    public bool Verbose { get; set; }
    public bool Quiet { get; set; }
    public bool DryRun { get; set; }
    public bool ShowColors { get; set; }
    public string? OutputFile { get; set;  }
    public ExtensionRegistry ExtensionRegistry { get => _extensibilityRegistry; set => _extensibilityRegistry = value; }
    public ITestSuiteExecutor? TestSuiteExecutor { get; set; }
    public ITestExecutor? TestExecutor { get; set; }
    public ITestReporter? TestReporter { get; set; }
    public ITestDiscoverer? TestDiscoverer { get; set; }
    public string? WorkingDirectory { get; set; }
    public IList<string> AssemblyPaths { get => _assemblyPaths; set => _assemblyPaths = value; }
    public IDictionary<string, object> Meta { get => _meta; set => _meta = value; }
    public string[]? Arguments { get; set; }
    public IList<Regex> TestNamePatterns { get => _testNamePatterns; set => _testNamePatterns = value; }
    public IList<Regex> TestGroupPatterns { get => _testGroupPatterns; set => _testGroupPatterns = value;  }
    public IList<Regex> SpecGroupPatterns { get => _specGroupPatterns; set => _specGroupPatterns = value; }
    public IList<Regex> TestNameWithinGroupPatterns { get => _testNameWithinGroupPatterns; set => _testNameWithinGroupPatterns = value; }
    public IList<Regex> SetupPatterns { get => _setupPatterns; set => _setupPatterns = value; }
    public IList<Regex> TeardownPatterns { get => _teardownPatterns; set => _teardownPatterns = value; }
    public IList<Regex> SuiteSetupPatterns { get => _suiteSetupPatterns; set => _suiteSetupPatterns = value; }
    public IList<Regex> SuiteTeardownPatterns { get => _suiteTeardownPatterns; set => _suiteTeardownPatterns = value; }
    public IList<Regex> GlobalSetupPatterns { get => _globalSetupPatterns; set => _globalSetupPatterns = value; }
    public IList<Regex> GlobalTeardownPatterns { get => _globalTeardownPatterns; set => _globalTeardownPatterns = value; }
  }
}