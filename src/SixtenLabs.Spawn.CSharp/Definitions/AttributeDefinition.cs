using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp
{
	public class AttributeDefinition : Definition, IHaveArguments
  {
    public AttributeDefinition(string name = null)
      : base(name)
    {
    }

    public IList<ArgumentDefinition> ArgumentDefinitions { get; set; } = new List<ArgumentDefinition>();
	}
}
