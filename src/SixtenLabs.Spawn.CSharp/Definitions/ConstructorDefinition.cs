using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp
{
	public class ConstructorDefinition : Definition
	{
    public ConstructorDefinition(string name = null)
      : base(name)
    {
    }

    public void AddCodeLineToBody(string code)
		{
			Block.AddStatement(code);
		}

    public BlockDefinition Block { get; set; } = new BlockDefinition();

    public List<AttributeDefinition> Attributes { get; set; } = new List<AttributeDefinition>();

    public List<ModifierDefinition> ModifierDefinitions { get; set; } = new List<ModifierDefinition>();

    public List<ParameterDefinition> Parameters { get; set; } = new List<ParameterDefinition>();
  }
}
