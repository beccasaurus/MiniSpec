using System;
using System.Reflection;
using System.Collections.Generic;

namespace MiniSpec.Testing {
  public interface ITest {
    TestAction Invoke { get; }
    string Name { get; }
    string FullName { get; }
    TestStatus Status { get; set; } // Add 'set' to everything that an extension would want to possibly change (which is everything! be more open :)
    DateTime RunAt { get; }
    TimeSpan Duration { get; }
    string STDOUT { get; }
    string STDERR { get; }
    Exception? Exception { get; }
    object? ReturnObject { get; }
    string? TypeName { get; }
    string? MethodName { get; }
    string? AssemblyLocation { get; }
    Assembly? Assembly { get; }
    MethodInfo? Method { get; }
    IEnumerable<TestAction> Setups { get; }
    IEnumerable<TestAction> Teardowns { get; }
    IDictionary<string, object> Meta { get; }
  }
}