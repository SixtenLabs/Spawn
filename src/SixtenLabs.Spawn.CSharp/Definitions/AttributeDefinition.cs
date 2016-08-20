using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp
{
	public class AttributeDefinition : Definition
	{
    public AttributeDefinition(string name = null)
      : base(name)
    {
    }

    public IList<string> ArgumentList { get; set; } = new List<string>();
	}
}
