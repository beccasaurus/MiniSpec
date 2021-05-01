// using System.IO;
// using System.Reflection;
// using System.Collections.Generic;
// using System.Text.RegularExpressions;

// namespace MiniSpec {
//   class TestDiscovererOLD {

//     Assembly _assembly;
//     TestSuite _suite;
//     TestExecutor _executor;
//     TestReporter _reporter;

//     internal TestDiscoverer(Assembly assembly, TestSuite testSuite, TestExecutor testExecutor, TestReporter testReporter) {
//       _assembly = assembly;
//       _suite = testSuite;
//       _executor = testExecutor;
//       _reporter = testReporter;
//     }

//     internal List<string> TEST_OR_TEST_GROUP_NAME_PATTERNS = new List<string> { "^[A-Z].*Test", "^[A-Z].*Spec", "^Test", "^Spec" };
//     internal List<string> TEST_GROUP_CHILD_TEST_NAME_PATTERNS = new List<string> { "^[A-Z].*Test", "^[A-Z].*Spec", "^Test", "^Spec", "^It", "^Should", "^Can" };

//     internal void FindAndExecuteTests() {
//       foreach (var type in _assembly.GetTypes()) {
//         var isTestType = false;
//         foreach (var pattern in TEST_OR_TEST_GROUP_NAME_PATTERNS)
//           if (new Regex(pattern).IsMatch(type.Name) || new Regex(pattern).IsMatch(type.FullName)) { isTestType = true; break; }

//         var typeMethods = new List<MethodInfo>(type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance));
//         typeMethods.AddRange(type.GetMethods(BindingFlags.NonPublic | BindingFlags.Static));

//         foreach (var method in typeMethods) {
//           var methodName = GetMethodOrFunctionSimpleName(method.Name);
//           var isTestMethod = false;
//           var patternsToTest = (isTestType || ParentMethodIsTestType(method.Name)) ? TEST_GROUP_CHILD_TEST_NAME_PATTERNS : TEST_OR_TEST_GROUP_NAME_PATTERNS;
//           foreach (var pattern in patternsToTest)
//               if (new Regex(pattern).IsMatch(methodName)) { isTestMethod = true; break; }
//           if (isTestMethod && MethodHasAnyLocalFunctionsWhichAreTests(method.Name, typeMethods, patternsToTest))
//             isTestMethod = false;
//           if (isTestMethod) {
//               var test = new Test(method);
//               _reporter.BeforeTest(test);
//               var result = _executor.ExecuteTestMethod(test);
//           }
//         }
//       }
//     }

//     string GetMethodOrFunctionSimpleName(string reflectionMethodName) {
//       // Example local function format: "<<Main>$>g__UnreleatedFunction1|0_1"
//       if (reflectionMethodName.Contains(">g__") && reflectionMethodName.Contains("|"))
//         return new Regex(".*g__([^|]+)|").Match(reflectionMethodName).Groups[1].Value;
//       else
//         return reflectionMethodName;
//     }

//     string GetLocalFunctionMethodName(string reflectionMethodName) {
//       // Example local function format: "<<Main>$>g__UnreleatedFunction1|0_1"
//       if (reflectionMethodName.Contains(">g__") && reflectionMethodName.Contains("|"))
//         return new Regex("([<]+)([^>]+)([>]+)").Match(reflectionMethodName).Groups[2].Value;
//       else
//         return null;
//     }

//     bool ParentMethodIsTestType(string reflectionMethodName) {
//       var methodName = GetLocalFunctionMethodName(reflectionMethodName);
//       if (methodName is null) return false;
//       foreach (var pattern in TEST_OR_TEST_GROUP_NAME_PATTERNS)
//         if (new Regex(pattern).IsMatch(methodName)) return true;
//       return false;
//     }

//     bool MatchesAnyPatterns(string text, List<string> patterns) {
//       foreach (var pattern in patterns)
//         if (new Regex(pattern).IsMatch(text))
//           return true;
//       return false;
//     }

//     bool MethodHasAnyLocalFunctionsWhichAreTests(string reflectionMethodName, List<MethodInfo> typeMethods, List<string> patternsToTest) {
//       foreach (var method in typeMethods)
//         if (method.Name.StartsWith($"<{reflectionMethodName}>"))
//           if (MatchesAnyPatterns(GetLocalFunctionMethodName(method.Name), patternsToTest))
//             return true;
//       return false;
//     }
//   }
// }
