using System.IO;

using MiniSpec.Testing.Configuration;
using MiniSpec.Testing.CommandLineInterface;

namespace MiniSpec {
    public class Tests {
    public static int Run(TextWriter? standardOutput, TextWriter? standardError, params string[] arguments) {
      var config = Config.GetInstanceWithDefaults();
      if (standardOutput is not null) config.StandardOutput = standardOutput;
      if (standardError is not null) config.StandardError = standardError;
      return Run(config, arguments);
    }
    public static int Run(IConfig config, params string[] arguments) => Runner.Run(config, arguments);

    #if NETSTANDARD
    public static int Run(params string[] arguments) => Run(null, null, arguments);
    #else
    public static int Run(params string[] arguments) => Run(System.Console.Out, System.Console.Error, arguments);
    #endif
    }
}
