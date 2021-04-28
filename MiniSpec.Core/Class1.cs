using System.IO;

namespace MiniSpec {
    public class Tests {
        public static int Run(TextWriter stdout, TextWriter stderr, params string[] arguments) {
            stdout.WriteLine("Hello, world!");
            int Hello() => 5;
            return 0;
        }

        public string Hello { get; set; }
        
        #if NETSTANDARD
        #else
        public static int Run() {
            return Run(System.Console.Out, System.Console.Error);
        }
        #endif
    }
}
