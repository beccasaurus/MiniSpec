namespace MiniSpec {
    public static class Console {
        public static int Main(string[] arguments) => MiniSpec.Tests.Run(System.Console.Out, System.Console.Error, arguments);
    }
}
