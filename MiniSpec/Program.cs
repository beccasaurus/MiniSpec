using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text.RegularExpressions;

foreach (var dll in args) {
    var dllPath = System.IO.Path.GetFullPath(dll);
    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dllPath);
    foreach (var type in assembly.GetTypes()) {
        var testMethods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
            .Where(m => m.Name.Contains("Test"));
        foreach (var method in testMethods) {
            var displayName = method.Name;
            if (Regex.IsMatch(displayName, @"[^\w]"))
                displayName =
                    Regex.Match(displayName, @"Test([\w]+)").Value;
            try {
                method.Invoke(null, null);
                Console.WriteLine($"PASS {displayName}");
            } catch (Exception e) {
                Console.WriteLine($"FAIL {displayName}");
                Console.WriteLine($"ERROR {e.InnerException.Message}");
            }
        }
    }
}