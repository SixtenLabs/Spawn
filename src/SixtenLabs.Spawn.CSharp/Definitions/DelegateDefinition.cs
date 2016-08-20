using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.CSharp
{
	/// <summary>
	/// The declaration of a delegate type is similar to a method signature. It has a return value and any number of parameters of any type:
	/// 
	/// A delegate is a reference type that can be used to encapsulate a named or an anonymous method. 
	/// Delegates are similar to function pointers in C++; however, delegates are type-safe and secure. 
	/// For applications of delegates, see Delegates and Generic Delegates.
	/// </summary>
	public class DelegateDefinition : Definition
	{
    public DelegateDefinition(string name = null)
      : base(name)
    {
    }
  }
}
