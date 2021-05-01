using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using MiniSpec.Testing;
using MiniSpec.Private.Testing;
using MiniSpec.Private.Testing.Reporters;
using MiniSpec.Private.Testing.Executors;
using MiniSpec.Private.Testing.Discovery;

namespace MiniSpec.Private {
    static class CLI {
        internal static int Run(TextWriter stdout, TextWriter stderr, params string[] arguments) {
            var config = Configuration.GlobalInstance ?? new Configuration();
            var testSuite = TestSuite.GlobalInstance ?? new TestSuite(config);

            config.Arguments = arguments;
            config.STDOUT = stdout;
            config.STDERR = stderr;

            var parseResult = ParseArguments(config, new List<string>(arguments));
            if (parseResult is not null)
                return parseResult.GetValueOrDefault();

            config.TestDiscoverer.DiscoverTests(testSuite);
            var testResult = config.TestSuiteExecutor.RunTestSuite(testSuite);

            if (config.DryRun) return 0;
            switch (testResult) {
                case TestStatus.Passed: return 0;
                case TestStatus.Failed: return 1;
                case TestStatus.Skipped: return 2;
                default: return 3;
            }
        }

        static int? ParseArguments(Configuration config, List<string> arguments) {
            while (arguments.Count > 0) {
                var argument = arguments[0];
                switch (argument) {
                    case "--version":
                        config.STDOUT.WriteLine($"MiniSpec version {Assembly.GetEntryAssembly().GetName().Version}");
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
                                    config.STDERR.WriteLine($"Unknown argument: '{argument}'");
                                    return 1;
                                }
                            }
                            arguments.RemoveAt(0);
                            break;
                        } else {
                            config.STDERR.WriteLine($"Unknown argument: '{argument}'");
                            return 1;
                        }
                }
            }
            config.AddAssemblyPath(Assembly.GetEntryAssembly().Location);
            return null;
        }
    }
}
