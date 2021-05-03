using System;
using System.Reflection;
using System.Collections.Generic;

using MiniSpec.Testing.Configuration;

namespace MiniSpec.Testing {
  public class TestSuite : ITestSuite {
    static Type _defaultClass = typeof(MiniSpecTestSuite);
    public static Type DefaultClass { get => _defaultClass; }
    public static void SetDefaultClass<T>(T defaultConfigClass) where T : ITestSuite => _defaultClass = typeof(T);
    public static ITestSuite GetInstance(IConfig? config = null) {
      try {
        object? testSuiteObject = Activator.CreateInstance(DefaultClass, config);
        #if NO_GET_TYPE_INFO_AVAILABLE
        var assembly = Assembly.GetAssembly(DefaultClass);
        #else
        var assembly = DefaultClass.GetTypeInfo().Assembly;
        #endif
        var assemblyLocation = (assembly is null) ? "" : $" from {assembly.Location}";
        if (testSuiteObject is null) throw new NullReferenceException($"Failed to initialize provided TestSuite class {DefaultClass.FullName}{assemblyLocation}");
        var testSuite = testSuiteObject as ITestSuite;
        if (testSuite is null) throw new NullReferenceException($"Could not construct valid ITestSuite from provided TestSuite class {DefaultClass.FullName}{assemblyLocation}");
        if (config is not null) testSuite.Config = config;
        return testSuite;
      } catch (Exception e) {
        #if NO_GET_TYPE_INFO_AVAILABLE
        var assembly = Assembly.GetAssembly(DefaultClass);
        #else
        var assembly = DefaultClass.GetTypeInfo().Assembly;
        #endif
        var assemblyLocation = (assembly is null) ? "" : $" from {assembly.Location}";
        throw new Exception($"Error when attempting to initialize provided TestSuite class {DefaultClass.FullName}{assemblyLocation}", e);
      }
    }
    static ITestSuite? _globalInstance;
    public static ITestSuite? GlobalInstance { get => _globalInstance; }
    public static ITestSuite InitializeOrGetGlobalInstance(IConfig? config = null) {
      if (_globalInstance is not null && _globalInstance.Config is not null && config is not null && _globalInstance.Config != config)
        throw new Exception("Unexpected Occurrance: Global ITestSuite IConfig instance does not match the IConfig provided to (InitializeOrGetGlobalInstance), Aborting.");
      if (_globalInstance is null) _globalInstance = GetInstance(config);
      if (_globalInstance.Config is null && config is not null) _globalInstance.Config = config;
      return _globalInstance!;
    }

    public TestSuite(IConfig? config = null) {
      if (config is not null) Config = config;
    }

    public TestStatus Run() {
      if (Config is null)
        throw new NullReferenceException("Please set ITestSuite.Config before calling TestSuite.Run()");
      else if (Config.TestSuiteExecutor is null)
        throw new NullReferenceException("Please set ITestSuite.Config.TestSuiteExecutor before calling TestSuite.Run()");
      else
        return Config.TestSuiteExecutor.RunTestSuite(this);
    }

    IList<ITest> _tests = new List<ITest>();
    IDictionary<string, object> _meta = new Dictionary<string, object>();
    IList<TestAction> _testSetups = new List<TestAction>();
    IList<TestAction> _testTeardowns = new List<TestAction>();
    IList<TestAction> _suiteSetups = new List<TestAction>();
    IList<TestAction> _suiteTeardowns = new List<TestAction>();

    public IConfig? Config { get; set; }
    public TestStatus Status { get; set; }
    public int PassedCount { get; set; }
    public int FailedCount { get; set; }
    public int SkippedCount { get; set; }
    public DateTime? RunAt { get; set; }
    public TimeSpan? Duration { get; set; }
    public IList<ITest> Tests { get => _tests; set => _tests = value; }
    public IDictionary<string, object> Meta { get => _meta; set => _meta = value; }
    public IList<TestAction> TestSetups { get => _testSetups; set => _testSetups = value; }
    public IList<TestAction> TestTeardowns { get => _testTeardowns; set => _testTeardowns = value; }
    public IList<TestAction> SuiteSetups { get => _suiteSetups; set => _suiteSetups = value; }
    public IList<TestAction> SuiteTeardowns { get => _suiteTeardowns; set => _suiteTeardowns = value; }
  }
}