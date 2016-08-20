using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp
{
	/// <summary>
	/// An interface contains only the signatures of methods, properties, events or indexers. 
	/// A class or struct that implements the interface must implement the members of the interface that are specified in the interface definition. 
	/// </summary>
	public class InterfaceDefinition : Definition
	{
    public InterfaceDefinition(string name = null)
      : base(name)
    {
    }

    public List<FieldDefinition> FieldDefinitions { get; set; } = new List<FieldDefinition>();
	}
}
