using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace MiniSpec {
    public class Tests {
        public static int Run(TextWriter stdout, TextWriter stderr, params string[] arguments) {
            var args = new List<string>(arguments);
            while (args.Count > 0) {
                var arg = args[0];
                switch (arg) {
                    case "--version":
                        stdout.WriteLine($"MiniSpec version {Assembly.GetEntryAssembly().GetName().Version}");
                        return 0;
                    default:
                        stderr.WriteLine($"Unknown argument: '{arg}'");
                        return 1;
                }
            }
            stdout.WriteLine("Hello, world!");
            return 0;
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
