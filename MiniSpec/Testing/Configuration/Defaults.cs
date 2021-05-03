using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using MiniSpec.Private.Testing.Discovery;
using MiniSpec.Private.Testing.Executors;
using MiniSpec.Private.Testing.Reporters;

namespace MiniSpec.Testing.Configuration {
  public static class Defaults {
    public static IConfig SetValues(IConfig Config) {
      Config.TestSuiteExecutor = Config.TestSuiteExecutor ?? TestSuiteExecutor;
      Config.TestExecutor = Config.TestExecutor ?? TestExecutor;
      Config.TestReporter = Config.TestReporter ?? TestReporter;
      Config.TestDiscoverer = Config.TestDiscoverer ?? TestDiscoverer;

      //
Config.TestNamePatterns = new List<Regex>() { new Regex("^[A-Z].*Test"), new Regex("^[A-Z].*Spec"), new Regex("^Test"), new Regex("^Spec") };
Config.TestGroupPatterns = new List<Regex>() { new Regex("^[A-Z].*Test"), new Regex("^[A-Z].*Spec"), new Regex("^Test"), new Regex("^Spec") };
Config.SpecGroupPatterns = new List<Regex>() { new Regex("^[A-Z].*Spec"), new Regex("^Spec") };
Config.TestNameWithinGroupPatterns = new List<Regex>() { new Regex("^[A-Z].*Test"), new Regex("^[A-Z].*Spec"), new Regex("^Test"), new Regex("^Spec"), new Regex("^It"), new Regex("^Can"), new Regex("^Should"), new Regex("^Example") };
Config.SetupPatterns = new List<Regex>() { new Regex("^[A-Z].*Set[uU]p"), new Regex("^[A-Z].*Before"), new Regex("^Set[uU]p"), new Regex("^Before") };
Config.TeardownPatterns = new List<Regex>() { new Regex("^[A-Z].*Tear[dD]own"), new Regex("^[A-Z].*After"), new Regex("^Tear[dD]own"), new Regex("^After") };
// IList<Regex> _globalSetupPatterns = new List<Regex>() { new Regex("Global.*Set[uU]p") };
// IList<Regex> _globalTeardownPatterns = new List<Regex>() { new Regex("Global.*Tear[dD]own") };
      return Config;
    }
    public static bool Verbose = false;
    public static bool Quiet = false;
    public static bool DryRun = false;
    public static bool ShowColors = true;

    #if NETSTANDARD
    public static TextWriter? StandardOutput = null;
    public static TextWriter? StandardError = null;
    #else
    public static TextWriter? StandardOutput = Console.Out;
    public static TextWriter? StandardError = Console.Error;
#endif

    public static ITestSuiteExecutor? TestSuiteExecutor = new TestSuiteExecutor();
    public static ITestExecutor? TestExecutor = new TestExecutor();
    public static ITestReporter? TestReporter = new DocumentationReporter();
    public static ITestDiscoverer? TestDiscoverer = new TestDiscoverer();
  }
}

// IList<Regex> _testNamePatterns = new List<Regex>() { new Regex("^[A-Z].*Test"), new Regex("^[A-Z].*Spec"), new Regex("^Test"), new Regex("^Spec") };
// IList<Regex> _testGroupPatterns = new List<Regex>() { new Regex("^[A-Z].*Test"), new Regex("^[A-Z].*Spec"), new Regex("^Test"), new Regex("^Spec") };
// IList<Regex> _specGroupPatterns = new List<Regex>() { new Regex("^[A-Z].*Spec"), new Regex("^Spec") };
// IList<Regex> _testNameWithinGroupPatterns = new List<Regex>() { new Regex("^[A-Z].*Test"), new Regex("^[A-Z].*Spec"), new Regex("^Test"), new Regex("^Spec"), new Regex("^It"), new Regex("^Can"), new Regex("^Should"), new Regex("^Example") };
// IList<Regex> _setupPatterns = new List<Regex>() { new Regex("^[A-Z].*Set[uU]p"), new Regex("^[A-Z].*Before"), new Regex("^Set[uU]p"), new Regex("^Before") };
// IList<Regex> _teardownPatterns = new List<Regex>() { new Regex("^[A-Z].*Tear[dD]own"), new Regex("^[A-Z].*After"), new Regex("^Tear[dD]own"), new Regex("^After") };
// IList<Regex> _globalSetupPatterns = new List<Regex>() { new Regex("Global.*Set[uU]p") };
// IList<Regex> _globalTeardownPatterns = new List<Regex>() { new Regex("Global.*Tear[dD]own") };

    // TextWriter? StandardOutput { get; set; }
    // TextWriter? StandardError { get; set; }
    // bool Verbose { get; set; }
    // bool Quiet { get; set; }
    // bool DryRun { get; set; }
    // bool ShowColors { get; set; }
    // string? OutputFile { get; set; }
    // string? WorkingDirectory { get; set; }
    // IList<string> AssemblyPaths { get; set; }
    // IDictionary<string, object> Meta { get; set; }
    // IList<Regex> TestNamePatterns { get; set; }
    // IList<Regex> TestGroupPatterns { get; set; }
    // IList<Regex> SpecGroupPatterns { get; set; }
    // IList<Regex> TestNameWithinGroupPatterns { get; set; }
    // IList<Regex> SetupPatterns { get; set; }
    // IList<Regex> TeardownPatterns { get; set; }
    // IList<Regex> SuiteSetupPatterns { get; set; }
    // IList<Regex> SuiteTeardownPatterns { get; set; }
    // IList<Regex> GlobalSetupPatterns { get; set; }
    // IList<Regex> GlobalTeardownPatterns { get; set; }
    // string[]? Arguments { get; set; }