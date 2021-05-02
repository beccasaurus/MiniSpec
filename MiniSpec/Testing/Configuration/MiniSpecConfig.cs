namespace MiniSpec.Testing.Configuration {
  public class MiniSpecConfig : Config, IConfig {
    
  }
}

// IList<Regex> _testNamePatterns = new List<Regex>() { new Regex("^[A-Z].*Test"), new Regex("^[A-Z].*Spec"), new Regex("^Test"), new Regex("^Spec") };
// IList<Regex> _testGroupPatterns = new List<Regex>() { new Regex("^[A-Z].*Test"), new Regex("^[A-Z].*Spec"), new Regex("^Test"), new Regex("^Spec") };
// IList<Regex> _specGroupPatterns = new List<Regex>() { new Regex("^[A-Z].*Spec"), new Regex("^Spec") };
// IList<Regex> _testNameWithinGroupPatterns = new List<Regex>() { new Regex("^[A-Z].*Test"), new Regex("^[A-Z].*Spec"), new Regex("^Test"), new Regex("^Spec"), new Regex("^It"), new Regex("^Can"), new Regex("^Should"), new Regex("^Example") };
// IList<Regex> _setupPatterns = new List<Regex>() { new Regex("^[A-Z].*Set[uU]p"), new Regex("^[A-Z].*Before"), new Regex("^Set[uU]p"), new Regex("^Before") };
// IList<Regex> _teardownPatterns = new List<Regex>() { new Regex("^[A-Z].*Tear[dD]own"), new Regex("^[A-Z].*After"), new Regex("^Tear[dD]own"), new Regex("^After") };
// IList<Regex> _globalSetupPatterns = new List<Regex>() { new Regex("Global.*Set[uU]p") };
// IList<Regex> _globalTeardownPatterns = new List<Regex>() { new Regex("Global.*Tear[dD]own") };