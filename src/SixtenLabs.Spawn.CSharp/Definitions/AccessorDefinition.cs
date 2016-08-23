using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp
{
	public class AccessorDefinition : Definition, IHaveBlock, IHaveModifiers
	{
    public AccessorDefinition(string name = null)
      : base(name)
    {
    }

    public IList<ModifierDefinition> ModifierDefinitions { get; set; } = new List<ModifierDefinition>();

		public SyntaxKindDto AccessorType { get; set; }

    public BlockDefinition BlockDefinition { get; private set; } = new BlockDefinition();
	}
}
