using System;
using System.Collections.Generic;

namespace MiniSpec.Testing {
  public interface ITestSuite {
    IConfiguration Config { get; }
    TestStatus Status { get; }
    int PassedCount { get; }
    int FailedCount { get; }
    int SkippedCount { get; }
    DateTime RunAt { get; }
    TimeSpan Duration { get; }
    IEnumerable<ITest> Tests { get; }
    IDictionary<string, object> Meta { get; }
  }
}