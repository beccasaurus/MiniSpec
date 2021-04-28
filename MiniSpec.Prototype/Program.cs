using System;
using System.Linq;
using System.Reflection;
// using System.Runtime.Loader;
using System.Text.RegularExpressions;

// foreach (var dll in args) {
//     var dllPath = System.IO.Path.GetFullPath(dll);
//     var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dllPath);
//     foreach (var type in assembly.GetTypes()) {
//         var testMethods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
//             .Where(m => m.Name.Contains("Test"));
//         foreach (var method in testMethods) {
//             var displayName = method.Name;
//             if (Regex.IsMatch(displayName, @"[^\w]"))
//                 displayName =
//                     Regex.Match(displayName, @"Test([\w]+)").Value;
//             try {
//                 method.Invoke(null, null);
//                 Console.WriteLine($"PASS {displayName}");
//             } catch (Exception e) {
//                 Console.WriteLine($"FAIL {displayName}");
//                 Console.WriteLine($"ERROR {e.InnerException.Message}");
//             }
//         }
//     }
// }

namespace MiniSpec {
    public class Program {
        public static void Main(string[] args) {
            #if NET472
                Console.WriteLine("Hi from MiniSpec in NET472");
            #elif NET40
                Console.WriteLine("Hi from MiniSpec in NET40");
            #elif NET45
                Console.WriteLine("Hi from MiniSpec in NET45");
            #elif NET50
                Console.WriteLine("Hi from MiniSpec in NET50");
            #elif NETSTANDARD
                Console.WriteLine("Hi from MiniSpec in NET STANDARD");
            #elif NETFRAMEWORK
                Console.WriteLine("Hi from MiniSpec in NET FRAMEWORK");
            #elif NETCORE
                Console.WriteLine("Hi from MiniSpec in NET CORE");
            #else
                Console.WriteLine("Hi from MiniSpec (unknown .NET version)");
            #endif
        }
    }
}