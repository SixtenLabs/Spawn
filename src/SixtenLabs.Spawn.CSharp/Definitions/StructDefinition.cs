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
	public class StructDefinition : TypeDefinition
	{
		public StructDefinition()
    {
		}

		public string SpecDerivedType { get; set; }

		public string DerivedType { get; set; }

		public bool NeedsMarshalling { get; set; }

		public List<ConstructorDefinition> Constructors { get; set; } = new List<ConstructorDefinition>();

		public List<FieldDefinition> Fields { get; set; } = new List<FieldDefinition>();

		public List<AttributeDefinition> Attributes { get; set; } = new List<AttributeDefinition>();
	}
}
