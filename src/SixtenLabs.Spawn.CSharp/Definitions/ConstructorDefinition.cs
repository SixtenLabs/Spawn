using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp
{
	public class ConstructorDefinition : Definition, IHaveAttributes, IHaveModifiers, IHaveParameters, IHaveBlock
	{
    public ConstructorDefinition(string name = null)
      : base(name)
    {
    }

    public BlockDefinition BlockDefinition { get; set; } = new BlockDefinition();

    public IList<AttributeDefinition> AttributeDefinitions { get; set; } = new List<AttributeDefinition>();

    public IList<ModifierDefinition> ModifierDefinitions { get; set; } = new List<ModifierDefinition>();

    public IList<ParameterDefinition> ParameterDefinitions { get; set; } = new List<ParameterDefinition>();
  }
}
