using System.Reflection;

namespace MiniSpec {
    class TestExecutor {

        TestSuite _testSuite;

        internal TestExecutor(TestSuite testSuite) {
            _testSuite = testSuite;
        }

        // internal void BeforeSuite(TestSuite suite) { }
        // internal void AfterSuite(TestSuite suite) { }

        internal bool ExecuteTestMethod(MethodInfo testMethod) {
            return true;
        }
    }
}
