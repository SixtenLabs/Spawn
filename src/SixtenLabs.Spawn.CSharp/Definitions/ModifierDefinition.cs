using Microsoft.CodeAnalysis.CSharp;

namespace SixtenLabs.Spawn.CSharp
{
	/// <summary>
	/// Modifiers are used to modify declarations of types and type members
	/// 
	/// Access Modifiers
	///   public
	///   private
	///   internal
	///   protected
	/// 
	/// Other Modifiers
	///   abstract
	///   async
	///   const
	///   event
	///   extern
	///   in
	///   out
	///   new
	///   override
	///   partial
	///   readonly
	///   sealed
	///   static
	///   unsafe
	///   virtual
	///   volatile
	/// 
	/// </summary>
	public class ModifierDefinition : Definition
	{
    public ModifierDefinition(string name = null)
      : base(name)
    {
    }

    public SyntaxKindDto Modifier { get; set; }
	}
}
