using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace MiniSpec {
    public class Tests {
        public static int Run(TextWriter stdout, TextWriter stderr, params string[] arguments) {
            var testRunner = new TestRunner(stdout, stderr);

            var args = new List<string>(arguments);
            while (args.Count > 0) {
                var arg = args[0];
                switch (arg) {
                    case "--version":
                        stdout.WriteLine($"MiniSpec version {Assembly.GetEntryAssembly().GetName().Version}");
                        return 0;
                    case "--list": testRunner.LIST_ONLY = true; args.RemoveAt(0); break;
                    case "-l": goto case "--list";
                    default:
                        // If starts with - or +, explode it into -a -b -c and throw them into the args to look at
                        stderr.WriteLine($"Unknown argument: '{arg}'");
                        return 1;
                }
            }

            testRunner.ASSEMBLIES_TO_SEARCH.Add(Assembly.GetEntryAssembly());

            return testRunner.Run();
        }

        public string Hello { get; set; }

        #if NETSTANDARD
        #else
        public static int Run(params string[] arguments) {
            return Run(System.Console.Out, System.Console.Error, arguments);
        }
        #endif
    }
}
