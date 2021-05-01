using System;
using System.Collections.Generic;

using MiniSpec.Testing;

namespace MiniSpec.Private.Testing {
  internal class TestSuite : ITestSuite {

    static TestSuite? _globalInstance;
    internal static TestSuite? GlobalInstance { get => _globalInstance; }

    internal static TestSuite InitializeOrGetGlobalInstance() {
      if (_globalInstance is null)
        _globalInstance = new TestSuite(Configuration.InitializeOrGetGlobalInstance());
      return _globalInstance;
    }

    internal TestSuite(IConfiguration config) {
      _config = config;
    }

    internal TestStatus Run() => Config.TestSuiteExecutor.RunTestSuite(this);
    internal Test AddTest(Test test) {
      _tests.Add(test);
      return test;
    }

    IConfiguration _config;
    TestStatus _status = TestStatus.NotRun;
    int _passedCount = 0;
    int _failedCount = 0;
    int _skippedCount = 0;
    DateTime _runAt = DateTime.Now;
    TimeSpan _duration = TimeSpan.Zero;
    List<ITest> _tests = new List<ITest>();
    IDictionary<string, object> _meta = new Dictionary<string, object>();
    List<TestAction> _globalSetups = new List<TestAction>();
    List<TestAction> _globalTeardowns = new List<TestAction>();

    public IConfiguration Config { get => _config; }
    public TestStatus Status { get => _status; }
    public int PassedCount { get => _passedCount; }
    public int FailedCount { get => _failedCount; }
    public int SkippedCount { get => _skippedCount; }
    public DateTime RunAt { get => _runAt; }
    public TimeSpan Duration { get => _duration; }
    public IEnumerable<ITest> Tests { get => _tests; }
    public IDictionary<string, object> Meta { get => _meta; }
    public IEnumerable<TestAction> GlobalSetups { get => _globalSetups; }
    public IEnumerable<TestAction> GlobalTeardowns { get => _globalTeardowns; }
  }
}