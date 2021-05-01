using System.Collections.Generic;
using MiniSpec.Specs.DSL;

namespace MiniSpec.Private.Specs.DSL {
  internal class DescribeBlock : IDescribeBlock {
    internal DescribeBlock(string description, IDescribeBlock? parent = null) {
      _description = description;
      _parent = parent;
    }

    public string FullDescription {
      get {
        if (Parent is null)
          return Description;
        else
          return $"{Parent.Description} {Description}";
      }
    }

    string _description;
    IDescribeBlock? _parent;
    List<IDescribeBlock> _childDescribeBlocks = new List<IDescribeBlock>();

    public string Description { get => _description; }
    public IDescribeBlock? Parent { get => _parent; }
    public IEnumerable<IDescribeBlock> ChildDescribeBlocks { get => _childDescribeBlocks; }
  }
}