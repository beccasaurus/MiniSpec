using System;
using System.Reflection;
using System.Collections.Generic;

using MiniSpec.Testing;

namespace MiniSpec.Private.Testing {
  internal class Test : ITest {
    internal Test(string name, string fullName, string typeName, string methodName, string assemblyLocation, MethodInfo method, Assembly assembly) {
      _name = name;
      _fullName = fullName;
      _typeName = typeName;
      _methodName = methodName;
      _assemblyLocation = assemblyLocation;
      _method = method;
      _assembly = assembly;
    }

    string _name;
    string _fullName;
    TestStatus _status = TestStatus.NotRun;
    DateTime _runAt = DateTime.Now;
    TimeSpan _duration = TimeSpan.Zero;
    string _stdout = string.Empty;
    string _stderr = string.Empty;
    Exception? _exception;
    object? _returnObject;
    string _typeName;
    string _methodName;
    string _assemblyLocation;
    Assembly _assembly;
    MethodInfo _method;
    IEnumerable<MethodInfo> _setups = new List<MethodInfo>();
    IEnumerable<MethodInfo> _teardowns = new List<MethodInfo>();
    IDictionary<string, object> _meta = new Dictionary<string, object>();

    public string Name { get => _name; }
    public string FullName { get => _fullName; }
    public TestStatus Status { get => _status; }
    public DateTime RunAt { get => _runAt; }
    public TimeSpan Duration { get => _duration; }
    public string STDOUT { get => _stdout; }
    public string STDERR { get => _stderr; }
    public Exception? Exception { get => _exception; }
    public object? ReturnObject { get => _returnObject; }
    public string TypeName { get => _typeName; }
    public string MethodName { get => _methodName; }
    public string AssemblyLocation { get => _assemblyLocation; }
    public Assembly Assembly { get => _assembly; }
    public MethodInfo Method { get => _method; }
    public IEnumerable<MethodInfo> Setups { get => _setups; }
    public IEnumerable<MethodInfo> Teardowns { get => _teardowns; }
    public IDictionary<string, object> Meta { get => _meta; }
  }
}