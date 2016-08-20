using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp
{
	/// <summary>
	/// Define a class to generate.
	///  
	/// Can Contain
	///   constructors
	///   destructors
	///   constants
	///   fields
	///   methods
	///   properties
	///   indexers
	///   operators
	///   events
	///   delegates
	///   classes
	///   interfaces
	///   structs
	///   
	/// </summary>
  public class ClassDefinition : Definition
  {
    public ClassDefinition(string name = null)
      : base(name)
    {
    }

    public string SpecDerivedType { get; set; }

		public string DerivedType { get; set; }

		public List<ConstructorDefinition> Constructors { get; set; } = new List<ConstructorDefinition>();

		public IList<FieldDefinition> Fields { get; set; } = new List<FieldDefinition>();

		public IList<PropertyDefinition> Properties { get; set; } = new List<PropertyDefinition>();

		public List<MethodDefinition> Methods { get; set; } = new List<MethodDefinition>();

		public List<AttributeDefinition> Attributes { get; set; } = new List<AttributeDefinition>();

    public List<ModifierDefinition> ModifierDefinitions { get; set; } = new List<ModifierDefinition>();

    public CommentDefinition Comments { get; set; } = new CommentDefinition();
  }
}
