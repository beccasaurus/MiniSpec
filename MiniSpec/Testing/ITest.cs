using System;
using System.Reflection;
using System.Collections.Generic;

namespace MiniSpec.Testing {
  public interface ITest {
    string Name { get; }
    string FullName { get; }
    TestStatus Status { get; }
    DateTime RunAt { get; }
    TimeSpan Duration { get; }
    string STDOUT { get; }
    string STDERR { get; }
    Exception? Exception { get; }
    object? ReturnObject { get; }
    string TypeName { get; }
    string MethodName { get; }
    string AssemblyLocation { get; }
    Assembly Assembly { get; }
    MethodInfo Method { get; }
    IEnumerable<MethodInfo> Setups { get; }
    IEnumerable<MethodInfo> Teardowns { get; }
    IDictionary<string, object> Meta { get; }
  }
}