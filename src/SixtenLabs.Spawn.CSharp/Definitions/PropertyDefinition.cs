using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp
{
	public class PropertyDefinition : Definition
	{
    public PropertyDefinition(string name = null)
      : base(name)
    {
    }

    public DefinitionName ReturnType { get; set; } = new DefinitionName();

    public AccessorDefinition AddAccessor(SyntaxKind type, SyntaxKind modifier = SyntaxKind.None, string block = null)
		{
			var accessor = new AccessorDefinition() { AccessorType = type, Modifier = modifier };

			if (!string.IsNullOrEmpty(block))
			{
				var blockDefinition = new BlockDefinition("block");
				blockDefinition.AddStatement(block);
				accessor.AddBlock(blockDefinition);
			}

			return accessor;
    }

		public void AddGetter(SyntaxKind modifier = SyntaxKind.None, string block = null)
		{
			Getter = AddAccessor(SyntaxKind.GetAccessorDeclaration, modifier, block);
		}

		public void AddSetter(SyntaxKind modifier = SyntaxKind.None, string block = null)
		{
			Setter = AddAccessor(SyntaxKind.SetAccessorDeclaration, modifier, block);
		}
		
		/// <summary>
		/// The default value of the field. 
		/// Null by default
		/// </summary>
		public LiteralDefinition DefaultValue { get; set; }

		public AccessorDefinition Getter { get; set; }

		public AccessorDefinition Setter { get; set; }

    public List<AttributeDefinition> AttributeDefinitions { get; set; } = new List<AttributeDefinition>();

    public List<ModifierDefinition> ModifierDefinitions { get; set; } = new List<ModifierDefinition>();
  }
}
