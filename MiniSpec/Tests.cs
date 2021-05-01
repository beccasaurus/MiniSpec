using System.IO;

namespace MiniSpec {
    public class Tests {
        public static int Run(TextWriter stdout, TextWriter stderr, params string[] arguments) => MiniSpec.Private.CLI.Run(stdout, stderr, arguments);

        #if NETSTANDARD
        #else
        public static int Run(params string[] arguments) => Run(System.Console.Out, System.Console.Error, arguments);
        #endif
    }
}
