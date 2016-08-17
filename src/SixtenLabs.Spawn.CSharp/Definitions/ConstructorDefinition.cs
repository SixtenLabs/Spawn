using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp
{
	public class ConstructorDefinition : TypeMemberDefinition
	{
		public ConstructorDefinition()
		{

		}

		public void AddCodeLineToBody(string code)
		{
			if (Block == null)
			{
				var body = new BlockDefinition() { SpecName = "body" };
				AddBlock(body);
			}

			Block.AddStatement(code);
		}

		public List<AttributeDefinition> Attributes { get; set; } = new List<AttributeDefinition>();
	}
}
