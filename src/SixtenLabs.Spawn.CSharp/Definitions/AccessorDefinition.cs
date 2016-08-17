using Microsoft.CodeAnalysis.CSharp;
using System;

namespace SixtenLabs.Spawn.CSharp
{
	public class AccessorDefinition : Definition
	{
		public AccessorDefinition()
		{
    }

		public void AddBlock(BlockDefinition block)
		{
			Block = block;
		}

		public SyntaxKind Modifier { get; set; }

		public SyntaxKind AccessorType { get; set; }

		public BlockDefinition Block { get; private set; }
	}
}
