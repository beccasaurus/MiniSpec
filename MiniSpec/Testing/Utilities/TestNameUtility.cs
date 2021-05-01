using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MiniSpec.Testing.Utilities {
  public static class TestNameUtility {
    static readonly Regex EXTRACT_LOCAL_FUNCTION_NAME = new Regex(".*g__([^|]+)|");
    static readonly Regex EXTRACT_LOCAL_FUNCTION_PARENT_METHOD_NAME = new Regex("([<]+)([^>]+)([>]+)");

    public static bool MatchesAnyPattern(string text, IEnumerable<Regex> patterns) {
      foreach (var pattern in patterns)
        if (pattern.IsMatch(text))
          return true;
      return false;
    }

    public static string MethodOrFunctionName(string methodName) {
      if (IsLocalFunction(methodName))
        return LocalFunctionName(methodName)!;
      else
        return methodName;
    }

    public static string FullMethodName(MethodInfo method) => FullMethodName(method.DeclaringType.FullName, method.Name);
    public static string FullMethodName(Type type, string methodName) => FullMethodName(type.FullName, methodName);
    public static string FullMethodName(string typeFullName, string methodName) {
      if (IsLocalFunction(methodName))
        return $"{typeFullName}.{LocalFunctionParentMethodName(methodName)}.{LocalFunctionName(methodName)}";
      else
        return $"{typeFullName}.{methodName}";
    }

    public static bool IsLocalFunction(MethodInfo method) => IsLocalFunction(method.Name);
    public static bool IsLocalFunction(string methodName) => methodName.Contains(">g__");

    public static string? LocalFunctionName(MethodInfo method) => LocalFunctionName(method.Name);
    public static string? LocalFunctionName(string methodName) {
      if (IsLocalFunction(methodName)) {
        var match = EXTRACT_LOCAL_FUNCTION_NAME.Match(methodName);
        if (match is not null && match.Groups.Count > 0)
          return match.Groups[1].Value;
      }
      return null;
    }

    public static string? LocalFunctionParentMethodName(MethodInfo method) => LocalFunctionParentMethodName(method.Name);
    public static string? LocalFunctionParentMethodName(string methodName) {
      if (IsLocalFunction(methodName)) {
        var match = EXTRACT_LOCAL_FUNCTION_PARENT_METHOD_NAME.Match(methodName);
        if (match is not null && match.Groups.Count > 1)
          return match.Groups[2].Value;
      }
      return null;
    }

    public static bool MatchesTestNamePattern(string text, IConfiguration config) => MatchesAnyPattern(text, config.TestNamePatterns);
    public static bool MatchesTestNamePattern(string text, ITestSuite suite) => MatchesTestNamePattern(text, suite.Config);

    public static bool MatchesTestGroupPattern(string text, IConfiguration config) => MatchesAnyPattern(text, config.TestGroupPatterns);
    public static bool MatchesTestGroupPattern(string text, ITestSuite suite) => MatchesTestGroupPattern(text, suite.Config);

    public static bool MatchesTestNameWithinGroupPattern(string text, IConfiguration config) => MatchesAnyPattern(text, config.TestNameWithinGroupPatterns);
    public static bool MatchesTestNameWithinGroupPattern(string text, ITestSuite suite) => MatchesTestNameWithinGroupPattern(text, suite.Config);

    public static bool MatchesSetupPattern(string text, IConfiguration config) => MatchesAnyPattern(text, config.SetupPatterns);
    public static bool MatchesSetupPattern(string text, ITestSuite suite) => MatchesSetupPattern(text, suite.Config);

    public static bool MatchesTeardownPattern(string text, IConfiguration config) => MatchesAnyPattern(text, config.TeardownPatterns);
    public static bool MatchesTeardownPattern(string text, ITestSuite suite) => MatchesTeardownPattern(text, suite.Config);

    public static bool MatchesGlobalSetupPattern(string text, IConfiguration config) => MatchesAnyPattern(text, config.GlobalSetupPatterns);
    public static bool MatchesGlobalSetupPattern(string text, ITestSuite suite) => MatchesGlobalSetupPattern(text, suite.Config);

    public static bool MatchesGlobalTeardownPattern(string text, IConfiguration config) => MatchesAnyPattern(text, config.GlobalTeardownPatterns);
    public static bool MatchesGlobalTeardownPattern(string text, ITestSuite suite) => MatchesGlobalTeardownPattern(text, suite.Config);
  }
}