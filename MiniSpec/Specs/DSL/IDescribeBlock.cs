using System.Collections.Generic;

namespace MiniSpec.Specs.DSL {
  public interface IDescribeBlock {
    string Description { get; }
    string FullDescription { get; }
    IDescribeBlock? Parent { get; }
    IEnumerable<IDescribeBlock> ChildDescribeBlocks { get; }

    // IEnumerable spec + setup + teardown
  }
}