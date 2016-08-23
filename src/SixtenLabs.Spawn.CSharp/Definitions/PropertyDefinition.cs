using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp
{
	public class PropertyDefinition : Definition, IHaveAccessors, IHaveAttributes, IHaveModifiers
	{
    public PropertyDefinition(string name = null)
      : base(name)
    {
    }

    public DefinitionName ReturnType { get; set; } = new DefinitionName();
		
		/// <summary>
		/// The default value of the field. 
		/// Null by default
		/// </summary>
		public LiteralDefinition DefaultValue { get; set; }

		public AccessorDefinition Getter { get; set; }

		public AccessorDefinition Setter { get; set; }

    public IList<AttributeDefinition> AttributeDefinitions { get; set; } = new List<AttributeDefinition>();

    public IList<ModifierDefinition> ModifierDefinitions { get; set; } = new List<ModifierDefinition>();
  }
}
