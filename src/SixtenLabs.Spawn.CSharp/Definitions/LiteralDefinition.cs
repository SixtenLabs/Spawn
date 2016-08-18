using System;
using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp
{
  public class LiteralDefinition : Definition
  {
    public LiteralDefinition()
		{
    }

    public string Value { get; set; }

    public Type LiteralType { get; set; }

    public SyntaxKindDto Kind { get; set; }

    public IList<ArgumentDefinition> Arguments { get; set; } = new List<ArgumentDefinition>();
  }
}
