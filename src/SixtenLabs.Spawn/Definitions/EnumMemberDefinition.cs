using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn
{
	public class EnumMemberDefinition : Definition
	{
		public object Value { get; set; }

		public SpecialType ValueType { get; set; }

		public IList<string> Comments { get; set; } = new List<string>();
	}
}
