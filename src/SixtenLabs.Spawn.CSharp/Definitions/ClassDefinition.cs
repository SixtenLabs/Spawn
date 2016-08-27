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
  public class ClassDefinition : Definition, IHaveProperties, IHaveFields, IHaveModifiers, IHaveMethods, IHaveInterfaces, IHaveAttributes, IHaveConstructors, IHaveComments
  {
    public ClassDefinition(string name = null)
      : base(name)
    {
    }

		public DefinitionName DerivedType { get; set; }

		public IList<ConstructorDefinition> ConstructorDefinitions { get; set; } = new List<ConstructorDefinition>();

		public IList<FieldDefinition> FieldDefinitions { get; set; } = new List<FieldDefinition>();

		public IList<PropertyDefinition> PropertyDefinitions { get; set; } = new List<PropertyDefinition>();

    public IList<InterfaceDefinition> InterfaceDefinitions { get; set; } = new List<InterfaceDefinition>();

    public IList<MethodDefinition> MethodDefinitions { get; set; } = new List<MethodDefinition>();

		public AttributeCollection AttributeDefinitions { get; set; } = new AttributeCollection();

    public IList<ModifierDefinition> ModifierDefinitions { get; set; } = new List<ModifierDefinition>();

    public DocumentationCommentDefinition DocumentationCommentDefinition { get; set; } = new DocumentationCommentDefinition();
  }
}
