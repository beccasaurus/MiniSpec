using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using MiniSpec.Testing.Extensibility;

namespace MiniSpec.Testing.Configuration {

  public interface IConfig {

    TextWriter StandardOutput { get; set; }
    TextWriter StandardError { get; set; }
    bool Verbose { get; set; }
    bool Quiet { get; set; }
    bool DryRun { get; set; }
    bool ShowColors { get; set; }
    string? OutputFile { get; set; }
    ExtensionRegistry ExtensionRegistry { get; set; }
    ITestSuiteExecutor? TestSuiteExecutor { get; set; }
    ITestExecutor? TestExecutor { get; set; }
    ITestReporter? TestReporter { get; set; }
    ITestDiscoverer? TestDiscoverer { get; set; }
    string? WorkingDirectory { get; set; }
    IList<string> AssemblyPaths { get; set; }
    IDictionary<string, object> Meta { get; set; }
    IList<Regex> TestNamePatterns { get; set; }
    IList<Regex> TestGroupPatterns { get; set; }
    IList<Regex> SpecGroupPatterns { get; set; }
    IList<Regex> TestNameWithinGroupPatterns { get; set; }
    IList<Regex> SetupPatterns { get; set; }
    IList<Regex> TeardownPatterns { get; set; }
    IList<Regex> SuiteSetupPatterns { get; set; }
    IList<Regex> SuiteTeardownPatterns { get; set; }
    IList<Regex> GlobalSetupPatterns { get; set; }
    IList<Regex> GlobalTeardownPatterns { get; set; }
    string[]? Arguments { get; set; }
  }
}