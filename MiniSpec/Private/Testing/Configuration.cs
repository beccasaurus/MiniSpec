using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using MiniSpec.Testing;

namespace MiniSpec.Private.Testing {
  internal class Configuration : IConfiguration {

    List<MethodInfo> GlobalBeforeEachSetups = new List<MethodInfo>();
    List<MethodInfo> GlobalAfterEachSetups = new List<MethodInfo>();
    List<MethodInfo> GlobalBeforeAllSetups = new List<MethodInfo>();
    List<MethodInfo> GlobalAfterAllSetups = new List<MethodInfo>();

    internal Configuration(string dir, string[] args, TextWriter stdout, TextWriter stderr, ITestSuiteExecutor suiteExecutor, ITestExecutor executor, ITestReporter reporter, ITestDiscoverer discoverer) {
      _workingDirectory = dir;
      _arguments = args;
      _stdout = stdout;
      _stderr = stderr;
      _testSuiteExecutor = suiteExecutor;
      _testExecutor = executor;
      _testReporter = reporter;
      _testDiscoverer = discoverer;
    }

    internal void AddAssemblyPath(string path) {
      _assemblyPaths.Add(path);
    }

    TextWriter _stdout;
    TextWriter _stderr;
    bool _verbose = false;
    bool _quiet = false;
    bool _dryRun = false;
    ITestSuiteExecutor _testSuiteExecutor;
    ITestExecutor _testExecutor;
    ITestReporter _testReporter;
    ITestDiscoverer _testDiscoverer;
    string _workingDirectory;
    List<string> _assemblyPaths = new List<string>();
    IDictionary<string, object> _meta = new Dictionary<string, object>();
    string[] _arguments;
    List<Regex> _testNamePatterns = new List<Regex>() { new Regex("^[A-Z].*Test"), new Regex("^[A-Z].*Spec"), new Regex("^Test"), new Regex("^Spec") };
    List<Regex> _testGroupPatterns = new List<Regex>() { new Regex("^[A-Z].*Test"), new Regex("^[A-Z].*Spec"), new Regex("^Test"), new Regex("^Spec") };
    List<Regex> _testNameWithinGroupPatterns = new List<Regex>() { new Regex("^[A-Z].*Test"), new Regex("^[A-Z].*Spec"), new Regex("^Test"), new Regex("^Spec"), new Regex("^It"), new Regex("^Can"), new Regex("^Should") };
    List<Regex> _setupPatterns = new List<Regex>() { new Regex("^[A-Z].*Set[uU]p"), new Regex("^[A-Z].*Before"), new Regex("^Set[uU]p"), new Regex("^Before") };
    List<Regex> _teardownPatterns = new List<Regex>() { new Regex("^[A-Z].*Tear[dD]own"), new Regex("^[A-Z].*After"), new Regex("^Tear[dD]own"), new Regex("^After") };
    List<Regex> _globalSetupPatterns = new List<Regex>() { new Regex("Global.*Set[uU]p") };
    List<Regex> _globalTeardownPatterns = new List<Regex>() { new Regex("Global.*Tear[dD]own") };

    public TextWriter STDOUT { get => _stdout; }
    public TextWriter STDERR { get => _stderr; }
    public bool Verbose { get => _verbose; set => _verbose = value; }
    public bool Quiet { get => _quiet; set => _quiet = value; }
    public bool DryRun { get => _dryRun; set => _dryRun = value; }
    public ITestSuiteExecutor TestSuiteExecutor { get => _testSuiteExecutor; }
    public ITestExecutor TestExecutor { get => _testExecutor; }
    public ITestReporter TestReporter { get => _testReporter; set => _testReporter = value; }
    public ITestDiscoverer TestDiscoverer { get => _testDiscoverer; }
    public string WorkingDirectory { get => _workingDirectory; }
    public IEnumerable<string> AssemblyPaths { get => _assemblyPaths; }
    public IDictionary<string, object> Meta { get => _meta; }
    public string[] Arguments { get => _arguments; }
    public IEnumerable<Regex> TestNamePatterns { get => _testNamePatterns; }
    public IEnumerable<Regex> TestGroupPatterns { get => _testGroupPatterns; }
    public IEnumerable<Regex> TestNameWithinGroupPatterns { get => _testNameWithinGroupPatterns; }
    public IEnumerable<Regex> SetupPatterns { get => _setupPatterns; }
    public IEnumerable<Regex> TeardownPatterns { get => _teardownPatterns; }
    public IEnumerable<Regex> GlobalSetupPatterns { get => _globalSetupPatterns; }
    public IEnumerable<Regex> GlobalTeardownPatterns { get => _globalTeardownPatterns; }
  }
}