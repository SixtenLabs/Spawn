﻿using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp
{
	public class MethodDefinition : Definition, IHaveBlock, IHaveAttributes, IHaveModifiers, IHaveParameters
	{
    public MethodDefinition(string name = null)
      : base(name)
    {
    }

    public DefinitionName ReturnType { get; set; } = new DefinitionName();

    public BlockDefinition BlockDefinition { get; set; } = new BlockDefinition();

    public IList<AttributeDefinition> AttributeDefinitions { get; set; } = new List<AttributeDefinition>();

    public IList<ParameterDefinition> ParameterDefinitions { get; set; } = new List<ParameterDefinition>();

    public IList<ModifierDefinition> ModifierDefinitions { get; set; } = new List<ModifierDefinition>();
  }
}