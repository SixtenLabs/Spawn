using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp
{
	public class FieldDefinition : Definition, IHaveModifiers, IHaveAttributes
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

		public IList<AttributeDefinition> AttributeDefinitions { get; set; } = new List<AttributeDefinition>();

    public IList<ModifierDefinition> ModifierDefinitions { get; set; } = new List<ModifierDefinition>();

    public bool LineFeed { get; set; } = true;
  }
}
