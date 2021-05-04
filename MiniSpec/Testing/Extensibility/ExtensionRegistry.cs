using System;
using System.Collections.Generic;

namespace MiniSpec.Testing.Extensibility {
  public class ExtensionRegistry {
    IList<Type> _testReporterTypes = new List<Type>();
    IList<Type> _testDiscovererTypes = new List<Type>();
    IList<Type> _testExecutorTypes = new List<Type>();
    IList<Type> _testSuiteExecutorTypes = new List<Type>();
    IList<Type> _extensionDiscovererTypes = new List<Type>();
    IList<Type> _extensionLoaderTypes = new List<Type>();

    public IList<Type> TestReporterTypes { get => _testReporterTypes; set => _testReporterTypes = value; }
    public IList<Type> TestDiscovererTypes { get => _testDiscovererTypes; set => _testDiscovererTypes = value; }
    public IList<Type> TestExecutorTypes { get => _testExecutorTypes; set => _testExecutorTypes = value; }
    public IList<Type> TestSuiteExecutorTypes { get => _testSuiteExecutorTypes; set => _testSuiteExecutorTypes = value; }
    public IList<Type> ExtensionDiscovererTypes { get => _extensionDiscovererTypes; set => _extensionDiscovererTypes = value; }
    public IList<Type> ExtensionLoaderTypes { get => _extensionLoaderTypes; set => _extensionLoaderTypes = value; }
  }
}