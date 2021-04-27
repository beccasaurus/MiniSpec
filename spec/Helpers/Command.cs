public struct CommandResult {
    public int ExitCode { get; init; }
    public string StandardOutput { get; init; }
    public string StandardError { get; init; }
    public bool Passed { get => ExitCode == 0; }
    public bool Success { get => ExitCode == 0; }
    public bool OK { get => ExitCode == 0; }
    public bool Failed { get => ExitCode != 0; }
}

public class Command {
    public static CommandResult Run(string commandName, string directory, params string[] arguments) {
        using var process = new System.Diagnostics.Process {
            StartInfo = { FileName = commandName, WorkingDirectory = directory, RedirectStandardOutput = true, RedirectStandardError = true }
        };
        foreach (var argument in arguments) process.StartInfo.ArgumentList.Add(argument);
        process.Start();
        process.WaitForExit();
        var result = new CommandResult {
            ExitCode = process.ExitCode,
            StandardOutput = process.StandardOutput.ReadToEnd(),
            StandardError = process.StandardError.ReadToEnd()
        };
        process.Kill();
        return result;
    }
}