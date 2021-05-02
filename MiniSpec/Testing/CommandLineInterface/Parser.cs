using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using MiniSpec.Testing.Configuration;
using MiniSpec.Private.Testing.Reporters;

namespace MiniSpec.Testing.CommandLineInterface {
    public class Parser {
        public int? ParseArguments(IConfig config, List<string> arguments) {
            while (arguments.Count > 0) {
                var argument = arguments[0];
                switch (argument) {
                    case "--version":
                        #if NO_GET_TYPE_INFO_AVAILABLE
                        var versionAssembly = Assembly.GetAssembly(typeof(Parser));
                        #else
                        var versionAssembly = typeof(Parser).GetTypeInfo().Assembly;
                        #endif
                        if (versionAssembly is null)
                            config.StandardOutput!.WriteLine($"MiniSpec");
                        else
                            config.StandardOutput!.WriteLine($"MiniSpec version {versionAssembly.GetName().Version}");
                        return 0;
                    case "--list":
                        config.DryRun = true;
                        config.TestReporter = new DryRunReporter();
                        arguments.RemoveAt(0);
                        break;
                    case "-l": goto case "--list";
                    case "--verbose":
                        config.Verbose = true;
                        arguments.RemoveAt(0);
                        break;
                    case "-v": goto case "--verbose";
                    default:
                        if ((argument.StartsWith("-") || argument.StartsWith("+")) && argument.Length > 2) {
                            var dashOrPlus = argument.Substring(0, 1);
                            var smallLetter = new Regex("[a-z]");
                            foreach (var character in argument.Substring(1).ToCharArray()) {
                                if (smallLetter.IsMatch(character.ToString())) {
                                    arguments.Add($"{dashOrPlus}{character}");
                                } else {
                                    config.StandardError!.WriteLine($"Unknown argument: '{argument}'");
                                    return 1;
                                }
                            }
                            arguments.RemoveAt(0);
                            break;
                        } else {
                            config.StandardError!.WriteLine($"Unknown argument: '{argument}'");
                            return 1;
                        }
                }
            }

            // This should be somewhere else
            var assembly = Assembly.GetEntryAssembly();
            if (assembly is not null)
                config.AssemblyPaths.Add(assembly.Location);
            return null;
        }
    }
}
