using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp
{
	/// <summary>
	/// Define a struct to generate.
	/// 
	/// A struct type is a value type that is typically used to encapsulate small groups of related variables, such as the coordinates of a rectangle
  /// or the characteristics of an item in an inventory.
	/// 
	/// Structs can contain:
	///   constructors
	///   constants
	///   fields
	///   methods
	///   properties
	///   indexers
	///   operators
	///   events
	///   nested types
	/// 
	/// </summary>
	public class StructDefinition : Definition, IHaveProperties, IHaveFields, IHaveMethods, IHaveModifiers, IHaveAttributes, IHaveConstructors, IHaveComments
	{
    public StructDefinition(string name = null)
      : base(name)
    {
    }

    public string SpecDerivedType { get; set; }

		public string DerivedType { get; set; }

		public bool NeedsMarshalling { get; set; }

		public IList<ConstructorDefinition> ConstructorDefinitions { get; set; } = new List<ConstructorDefinition>();

		public IList<FieldDefinition> FieldDefinitions { get; set; } = new List<FieldDefinition>();

    public IList<MethodDefinition> MethodDefinitions { get; set; } = new List<MethodDefinition>();

    public IList<PropertyDefinition> PropertyDefinitions { get; set; } = new List<PropertyDefinition>();

    public AttributeCollection AttributeDefinitions { get; set; } = new AttributeCollection();

    public IList<ModifierDefinition> ModifierDefinitions { get; set; } = new List<ModifierDefinition>();

    public DocumentationCommentDefinition DocumentationCommentDefinition { get; set; } = new DocumentationCommentDefinition();
  }
}
