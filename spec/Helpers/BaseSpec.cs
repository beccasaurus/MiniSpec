using Xunit;

public class IntegrationTest {

    [Fact]
    public void ExpectedSpecsPassAndFail() {
        // Arrange
        var minispecExe = System.IO.File.Exists("minispec.exe") ?
            "minispec.exe" : "minispec"; // No .exe extension on Linux
            
        using var minispec = new System.Diagnostics.Process {
            StartInfo = {
                RedirectStandardOutput = true, // Get the STDOUT
                RedirectStandardError = true,  // Get the STDERR
                FileName = minispecExe,
                Arguments = "MyTests.dll"
            }
        };

        // Act
        minispec.Start();
        minispec.WaitForExit();
        var stdout = minispec.StandardOutput.ReadToEnd();
        var stderr = minispec.StandardError.ReadToEnd();
        var output = $"{stdout}{stderr}";
        minispec.Kill();

        // Assert
        Assert.Contains("PASS TestShouldPass", output);
        Assert.Contains("FAIL TestShouldFail", output);
        Assert.Contains("Kaboom!", output);
    }
}