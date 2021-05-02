using System;
using System.Collections.Generic;

namespace MiniSpec.Testing {
  public interface ITestSuite {
    TestStatus Run();
    Configuration.IConfig? Config { get; set; }
    TestStatus Status { get; set; }
    int PassedCount { get; set; }
    int FailedCount { get; set; }
    int SkippedCount { get; set; }
    DateTime? RunAt { get; set; }
    TimeSpan? Duration { get; set; }
    IList<ITest> Tests { get; set; }
    IDictionary<string, object> Meta { get; set; }
    IList<TestAction> TestSetups { get; set; }
    IList<TestAction> TestTeardowns { get; set; }
    IList<TestAction> SuiteSetups { get; set; }
    IList<TestAction> SuiteTeardowns { get; set; }
  }
}