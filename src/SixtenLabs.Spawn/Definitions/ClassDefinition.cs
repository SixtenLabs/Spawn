using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn
{
	public class ClassDefinition : Definition
	{
		public Accessibility AccessModifier { get; set; }

		public DeclarationModifiers DeclarationModifiers { get; set; }

		public IList<FieldDefinition> Fields { get; set; } = new List<FieldDefinition>();

		public IList<ConstructorDefinition> Constructors { get; set; } = new List<ConstructorDefinition>();

		public IList<PropertyDefinition> Properties { get; set; } = new List<PropertyDefinition>();
	}
}
