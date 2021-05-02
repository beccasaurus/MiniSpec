using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using MiniSpec.Testing.Configuration;

namespace MiniSpec.Testing.Utilities {
  public static class TestNameUtility {
    static readonly Regex EXTRACT_LOCAL_FUNCTION_NAME = new Regex(".*g__([^|]+)|");
    static readonly Regex EXTRACT_LOCAL_FUNCTION_PARENT_METHOD_NAME = new Regex("([<]+)([^>]+)([>]+)");
    static readonly Regex TOP_LEVEL_STATEMENT_TYPE_AND_METHOD_PREFIX_PATTERN = new Regex(@"^<Program>\$\.Main\.");
    static readonly Regex NON_FRIENDLY_TYPE_OR_METHOD_DISPLAY_NAME_CHARACTERS_PATTERN = new Regex("[^a-zA-Z0-9._]");

    public static bool MatchesAnyPattern(string text, IEnumerable<Regex> patterns) {
      foreach (var pattern in patterns)
        if (pattern.IsMatch(text)) return true;
      return false;
    }

    public static string MethodOrFunctionName(string methodName) {
      if (IsLocalFunction(methodName))
        return LocalFunctionName(methodName)!;
      else
        return methodName;
    }

    public static string FullMethodName(MethodInfo method) => FullMethodName(((method.DeclaringType is not null && method.DeclaringType.FullName is not null) ? method.DeclaringType!.FullName : ""), method.Name);
    public static string FullMethodName(Type? type, string methodName) => FullMethodName(((type is not null && type.FullName is not null) ? type!.FullName : ""), methodName);
    public static string FullMethodName(string typeFullName, string methodName) {
      string fullMethodName;
      typeFullName = string.IsNullOrEmpty(typeFullName) ? "" : $"{typeFullName}.";
      if (IsLocalFunction(methodName))
        fullMethodName = $"{typeFullName}{LocalFunctionParentMethodName(methodName)}.{LocalFunctionName(methodName)}";
      else
        fullMethodName = $"{typeFullName}{methodName}";
      fullMethodName = TOP_LEVEL_STATEMENT_TYPE_AND_METHOD_PREFIX_PATTERN.Replace(fullMethodName, "");
      fullMethodName = NON_FRIENDLY_TYPE_OR_METHOD_DISPLAY_NAME_CHARACTERS_PATTERN.Replace(fullMethodName, "");
      return fullMethodName;
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

    // TODO Rewrite to throw nice exception if Suite Config has not yet been set please :) For now, using forgiveness operator.

    public static bool MatchesTestNamePattern(string text, IConfig config) => MatchesAnyPattern(text, config.TestNamePatterns);
    public static bool MatchesTestNamePattern(string text, ITestSuite suite) => MatchesTestNamePattern(text, suite.Config!);

    public static bool MatchesTestGroupPattern(string text, IConfig config) => MatchesAnyPattern(text, config.TestGroupPatterns);
    public static bool MatchesTestGroupPattern(string text, ITestSuite suite) => MatchesTestGroupPattern(text, suite.Config!);

    public static bool MatchesSpecGroupPattern(string text, IConfig config) => MatchesAnyPattern(text, config.SpecGroupPatterns);
    public static bool MatchesSpecGroupPattern(string text, ITestSuite suite) => MatchesSpecGroupPattern(text, suite.Config!);

    public static bool MatchesTestNameWithinGroupPattern(string text, IConfig config) => MatchesAnyPattern(text, config.TestNameWithinGroupPatterns);
    public static bool MatchesTestNameWithinGroupPattern(string text, ITestSuite suite) => MatchesTestNameWithinGroupPattern(text, suite.Config!);

    public static bool MatchesSetupPattern(string text, IConfig config) => MatchesAnyPattern(text, config.SetupPatterns);
    public static bool MatchesSetupPattern(string text, ITestSuite suite) => MatchesSetupPattern(text, suite.Config!);

    public static bool MatchesTeardownPattern(string text, IConfig config) => MatchesAnyPattern(text, config.TeardownPatterns);
    public static bool MatchesTeardownPattern(string text, ITestSuite suite) => MatchesTeardownPattern(text, suite.Config!);

    public static bool MatchesGlobalSetupPattern(string text, IConfig config) => MatchesAnyPattern(text, config.GlobalSetupPatterns);
    public static bool MatchesGlobalSetupPattern(string text, ITestSuite suite) => MatchesGlobalSetupPattern(text, suite.Config!);

    public static bool MatchesGlobalTeardownPattern(string text, IConfig config) => MatchesAnyPattern(text, config.GlobalTeardownPatterns);
    public static bool MatchesGlobalTeardownPattern(string text, ITestSuite suite) => MatchesGlobalTeardownPattern(text, suite.Config!);
  }
}