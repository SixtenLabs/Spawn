using System.Collections.Generic;

namespace SixtenLabs.Spawn
{
	/// <summary>
	/// This is used to keep track of all the names from 
	/// the spec that is being converted, and all of the translations
	/// for the converted output. This allows for changing the names in
	/// the spec to be more aligned with the language or conventions
	/// of the generated code.
	/// </summary>
	public class SpecTypeDefinition : IDefinition
	{
    public DefinitionName Name { get; set; } = new DefinitionName();

		/// <summary>
		/// This is used to capture meta data about parameters, properties, and method names
		/// </summary>
		public IList<SpecTypeDefinition> Children { get; } = new List<SpecTypeDefinition>();
	}
}
