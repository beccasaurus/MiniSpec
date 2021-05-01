using System.Collections.Generic;

namespace MiniSpec.Private.Utilities {
  public class EnumerableUtility {
    public static int GetCount<T>(IEnumerable<T> enumerable) {
      int count = 0;
      var enumerator = enumerable.GetEnumerator();
      while (enumerator.MoveNext()) count++;
      return count;
    }
  }
}