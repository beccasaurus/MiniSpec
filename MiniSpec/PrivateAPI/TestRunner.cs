using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace MiniSpec {
    class TestRunner {
        TextWriter STDOUT;
        TextWriter STDERR;

        internal TestRunner(TextWriter stdout, TextWriter stderr) {
            STDOUT = stdout;
            STDERR = stderr;
        }

        internal List<Assembly> ASSEMBLIES_TO_SEARCH = new List<Assembly>();

        internal bool LIST_ONLY = false;

        internal int Run() {
            var testSuite = new TestSuite();
            var testExecutor = new TestExecutor(testSuite);
            var testReporter = LIST_ONLY ? new PrintTestNamesReporter(testSuite, STDOUT, STDERR) : new TestReporter(testSuite, STDOUT, STDERR);

            foreach (var assembly in ASSEMBLIES_TO_SEARCH) {
                var testDiscoverer = new TestDiscoverer(assembly, testSuite, testExecutor, testReporter);
                testDiscoverer.FindAndExecutorTests();
            }

            return testSuite.ExitCode;
        }
    }
}
