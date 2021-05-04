using System;
using System.IO;
using System.Reflection;

using MiniSpec.Private.Testing.Reporters;

namespace MiniSpec.Testing.Extensibility {
  public class ExtensionDiscoverer : IExtensionDiscoverer {
    public void DiscoverExtensions(ITestSuite suite) {
      // TODO add config for specifying extension folders etc - right now just look for ones next to us!
      Assembly miniSpecAssembly;
      #if NO_GET_TYPE_INFO_AVAILABLE
      miniSpecAssembly = Assembly.GetAssembly(typeof(ExtensionDiscoverer));
      #else
      miniSpecAssembly = typeof(ExtensionDiscoverer).GetTypeInfo().Assembly;
      #endif
      var searchDirectory = Path.GetDirectoryName(miniSpecAssembly.Location);
      
      var dlls = Directory.GetFiles(searchDirectory, "*MiniSpec*.dll");
      foreach (var dllPath in dlls) {
        if (Path.GetFileName(dllPath) == "MiniSpec.dll") continue;
        try {
          var assembly = Assembly.LoadFile(dllPath);
          DiscoverAndLoadExtensionsInAssembly(assembly, suite);
        } catch (Exception e) {
          throw new Exception($"Failed to load DLL {dllPath} (presumably an extension?)", e);
        }
      }
    }

    void DiscoverAndLoadExtensionsInAssembly(Assembly assembly, ITestSuite suite) {
      foreach (var type in assembly.GetTypes()) {
        if (type.Name.EndsWith("Reporter")) {
          // suite.Config!.ExtensionRegistry.TestReporterTypes.Add(type);
          var reporter = Activator.CreateInstance(type);
          if (reporter is not null)
            suite.Config.TestReporter = new ExtensionReporter(reporter);
        }
      }
    }
  }
}