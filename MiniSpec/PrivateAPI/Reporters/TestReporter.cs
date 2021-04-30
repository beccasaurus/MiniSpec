using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace MiniSpec {
    class TestReporter {

        TestSuite _testSuite;
        internal TextWriter STDOUT;
        internal TextWriter STDERR;

        internal TestReporter(TestSuite testSuite, TextWriter stdout, TextWriter stderr) {
            _testSuite = testSuite;
            STDOUT = stdout;
            STDERR = stderr;
        }

        internal virtual void BeforeTest(string testName) { }

        // internal void BeforeSuite(TestSuite suite) { }
        // internal void AfterSuite(TestSuite suite) { }
    }
}
