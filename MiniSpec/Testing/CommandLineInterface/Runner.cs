using System;
using System.Collections.Generic;

using MiniSpec.Testing.Configuration;
using MiniSpec.Testing.Extensibility;
namespace MiniSpec.Testing.CommandLineInterface {
    public static class Runner {
        public static int Run(IConfig config, params string[] arguments) {
            CheckRequiredConfigurationDefaults(config);

            var testSuite = TestSuite.InitializeOrGetGlobalInstance(config);            
            config.Arguments = arguments;

            var parseResult = new Parser().ParseArguments(config, new List<string>(arguments));
            if (parseResult is not null) return parseResult.GetValueOrDefault();

            new ExtensionDiscoverer().DiscoverExtensions(testSuite);

            config.TestDiscoverer!.DiscoverTests(testSuite);
            var testResult = config.TestSuiteExecutor!.RunTestSuite(testSuite);

            if (config.DryRun) return 0;

            return testResult switch {
                TestStatus.Passed => 0,
                TestStatus.Failed => 1,
                TestStatus.Skipped => 2,
                TestStatus.NotRun => 3,
                _ => 4
            };
        }

        static void CheckRequiredConfigurationDefaults(IConfig config) {
            if (config.StandardOutput    is null) throw new Exception("Provided configuration does not have configured StandardOutput, Aborting.");
            if (config.StandardError     is null) throw new Exception("Provided configuration does not have configured StandardError, Aborting.");
            if (config.TestDiscoverer    is null) throw new Exception("Provided configuration does not have configured ITestDiscoverer, Aborting.");
            if (config.TestSuiteExecutor is null) throw new Exception("Provided configuration does not have configured ITestDiscoverer, Aborting.");
            if (config.TestExecutor      is null) throw new Exception("Provided configuration does not have configured ITestDiscoverer, Aborting.");
            if (config.TestReporter      is null) throw new Exception("Provided configuration does not have configured ITestDiscoverer, Aborting.");
        }
    }
}
