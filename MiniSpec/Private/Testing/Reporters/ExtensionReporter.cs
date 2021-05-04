using System.Reflection;

using MiniSpec.Testing;

namespace MiniSpec.Private.Testing.Reporters {
  public class ExtensionReporter : ITestReporter {
    public object ReporterObject { get; set; }

    MethodInfo? _beforeDiscoveryMethod;
    MethodInfo? _afterDiscoveryMethod;
    MethodInfo? _beforeSuiteMethod;
    MethodInfo? _afterSuiteMethod;
    MethodInfo? _beforeTestMethod;
    MethodInfo? _afterTestMethod;

    public ExtensionReporter(object reporterObject) {
      ReporterObject = reporterObject;
      #if NO_GET_TYPE_INFO_AVAILABLE
      var methods = reporterObject.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
      #else
      var methods = reporterObject.GetType().GetTypeInfo().DeclaredMethods;
      #endif
      foreach (var method in methods) {
        switch (method.Name) {
          case "BeforeDiscovery": _beforeDiscoveryMethod = method; break;
          case "AfterDiscovery": _afterDiscoveryMethod = method; break;
          case "BeforeSuite": _beforeSuiteMethod = method; break;
          case "AfterSuite": _afterSuiteMethod = method; break;
          case "BeforeTest": _beforeTestMethod = method; break;
          case "AfterTest": _afterTestMethod = method; break;
        }
      }
    }

    public void BeforeDiscovery(ITestSuite suite) {
      if (_beforeDiscoveryMethod is not null) _beforeDiscoveryMethod.Invoke(ReporterObject, new object[] { suite });
    }
    public void AfterDiscovery(ITestSuite suite) {
      if (_afterDiscoveryMethod is not null) _afterDiscoveryMethod.Invoke(ReporterObject, new object[] { suite });
    }
    public void BeforeSuite(ITestSuite suite) {
      if (_beforeSuiteMethod is not null) _beforeSuiteMethod.Invoke(ReporterObject, new object[] { suite });
    }
    public void AfterSuite(ITestSuite suite) {
      if (_afterSuiteMethod is not null) _afterSuiteMethod.Invoke(ReporterObject, new object[] { suite });
    }
    public void BeforeTest(ITestSuite suite, ITest test) {
      if (_beforeTestMethod is not null) _beforeTestMethod.Invoke(ReporterObject, new object[] { suite, test });
    }
    public void AfterTest(ITestSuite suite, ITest test) {
      suite.Config.StandardOutput.WriteLine($"So, we're invoking the function and HERE, the test status is: {test.Status}");
      if (_afterTestMethod is not null) _afterTestMethod.Invoke(ReporterObject, new object[] { suite, test });
    }
  }
}