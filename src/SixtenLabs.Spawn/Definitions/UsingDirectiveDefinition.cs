using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn
{
	/// <summary>
	/// Define a Using Directive.
	/// 
	/// There are three types of Using Directives in C#
	/// 
	/// Standard: To allow the use of types in a namespace so that you do not have to qualify the use of a type in that namespace:
	///   using System.Text;
	/// 
	/// Static: To allow you to access static members of a type without having to qualify the access with the type name:
	///   using static System.Math;
	/// 
	/// Alias: To create an alias for a namespace or a type. This is called a using alias directive.
	///   using Project = PC.MyCompany.Project;
	/// 
	/// </summary>
	public class UsingDirectiveDefinition : Definition
	{
		/// <summary>
		/// Is this a static using directive?
		/// </summary>
		public bool IsStatic { get; set; }

		/// <summary>
		/// Is this an aliased using directive?
		/// </summary>
		public bool UseAlias { get; set; }

		/// <summary>
		/// The alias to use in the using directive
		/// </summary>
		public string Alias { get; set; }
	}
}
