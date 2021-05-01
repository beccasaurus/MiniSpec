using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MiniSpec.Testing {
  public interface IConfiguration {
    TextWriter STDOUT { get; }
    TextWriter STDERR { get; }
    bool Verbose { get; }
    bool Quiet { get; }
    bool DryRun { get; }
    ITestSuiteExecutor TestSuiteExecutor { get; }
    ITestExecutor TestExecutor { get; }
    ITestReporter TestReporter { get; }
    ITestDiscoverer TestDiscoverer { get; }
    string WorkingDirectory { get; }
    IEnumerable<string> AssemblyPaths { get; }
    IDictionary<string, object> Meta { get; }
    string[] Arguments { get; }
    IEnumerable<Regex> TestNamePatterns { get; }
    IEnumerable<Regex> TestGroupPatterns { get; }
    IEnumerable<Regex> SpecGroupPatterns { get; }
    IEnumerable<Regex> TestNameWithinGroupPatterns { get; }
    IEnumerable<Regex> SetupPatterns { get; }
    IEnumerable<Regex> TeardownPatterns { get; }
    IEnumerable<Regex> GlobalSetupPatterns { get; }
    IEnumerable<Regex> GlobalTeardownPatterns { get; }
  }
}