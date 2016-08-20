using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp
{
	public class FieldDefinition : Definition
	{
    public FieldDefinition(string name = null)
      : base(name)
    {
    }

    public DefinitionName ReturnType { get; set; } = new DefinitionName();

		/// <summary>
		/// The default value of the field. 
		/// Null by default
		/// </summary>
		public LiteralDefinition DefaultValue { get; set; }

		public bool ReturnTypeIsArray { get; set; }

		public int ArraySize { get; set; }

		public List<AttributeDefinition> AttributeDefinitions { get; set; } = new List<AttributeDefinition>();

    public List<ModifierDefinition> ModifierDefinitions { get; set; } = new List<ModifierDefinition>();
  }
}
