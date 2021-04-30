using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace MiniSpec {
    class PrintTestNamesReporter : TestReporter {

        internal PrintTestNamesReporter(TestSuite testSuite, TextWriter stdout, TextWriter stderr) : base(testSuite, stdout, stderr) {}

        internal override void BeforeTest(string testName) {
            STDOUT.WriteLine(testName);
        }
    }
}
