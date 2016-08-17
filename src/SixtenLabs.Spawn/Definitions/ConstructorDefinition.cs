using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn
{
	public class ConstructorDefinition : Definition
	{
		public Accessibility AccessModifier { get; set; }

		public DeclarationModifiers DeclarationModifiers { get; set; }

		public IList<ParameterDefinition> Parameters { get; set; } = new List<ParameterDefinition>();
	}
}
