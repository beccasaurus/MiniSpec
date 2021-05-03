using System;
using System.Reflection;
using System.Collections.Generic;

using MiniSpec.Testing;

namespace MiniSpec.Private.Testing {
  internal class Test : ITest {
    internal Test(TestFunc<object?> invoke, string name, string fullName, string? typeName, string? methodName, string? assemblyLocation, MethodInfo? method, Assembly? assembly) {
      _invoke = invoke;
      _name = name;
      _fullName = fullName;
      _typeName = typeName;
      _methodName = methodName;
      _assemblyLocation = assemblyLocation;
      _method = method;
      _assembly = assembly;
    }

    TestFunc<object?> _invoke;
    string _name;
    string _fullName;
    TestStatus _status = TestStatus.NotRun;
    DateTime _runAt = DateTime.Now;
    TimeSpan _duration = TimeSpan.Zero;
    string _StandardOutput = string.Empty;
    string _StandardError = string.Empty;
    Exception? _exception;
    object? _returnObject;
    string? _typeName;
    string? _methodName;
    string? _assemblyLocation;
    Assembly? _assembly;
    MethodInfo? _method;
    IEnumerable<TestAction> _setups = new List<TestAction>();
    IEnumerable<TestAction> _teardowns = new List<TestAction>();
    IDictionary<string, object> _meta = new Dictionary<string, object>();

    public TestFunc<object?> Invoke { get => _invoke; }
    public string Name { get => _name; }
    public string FullName { get => _fullName; }
    public TestStatus Status { get => _status; set => _status = value; }
    public DateTime RunAt { get => _runAt; }
    public TimeSpan Duration { get => _duration; }
    public string StandardOutput { get => _StandardOutput; set => _StandardOutput = value; }
    public string StandardError { get => _StandardError; set => _StandardError = value; }
    public Exception? Exception { get => _exception; set => _exception = value; }
    public object? ReturnObject { get => _returnObject; set => _returnObject = value; }
    public string? TypeName { get => _typeName; }
    public string? MethodName { get => _methodName; }
    public string? AssemblyLocation { get => _assemblyLocation; }
    public Assembly? Assembly { get => _assembly; }
    public MethodInfo? Method { get => _method; }
    public IEnumerable<TestAction> Setups { get => _setups; }
    public IEnumerable<TestAction> Teardowns { get => _teardowns; }
    public IDictionary<string, object> Meta { get => _meta; }
  }
}