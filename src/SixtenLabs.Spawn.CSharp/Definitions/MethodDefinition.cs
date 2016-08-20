using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp
{
	public class MethodDefinition : Definition
	{
    public MethodDefinition(string name = null)
      : base(name)
    {
    }

    public DefinitionName ReturnType { get; set; } = new DefinitionName();

    public void AddCodeLineToBody(string code)
		{
			Block.AddStatement(code);
		}

    public BlockDefinition Block { get; set; } = new BlockDefinition();

    public List<AttributeDefinition> Attributes { get; set; } = new List<AttributeDefinition>();

    public List<ParameterDefinition> Parameters { get; set; } = new List<ParameterDefinition>();

    public List<ModifierDefinition> ModifierDefinitions { get; set; } = new List<ModifierDefinition>();
  }
}