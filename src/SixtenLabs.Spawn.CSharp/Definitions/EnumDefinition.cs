using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp
{
	/// <summary>
	/// Define an enum to Generate.
	/// 
	/// The enum keyword is used to declare an enumeration, a distinct type that consists of a set of named constants called the enumerator list.
	/// 
	/// </summary>
	public class EnumDefinition : Definition, IHaveAttributes, IHaveModifiers, IHaveComments
  {
    public EnumDefinition(string name = null)
      : base(name)
    {
    }

    public IList<EnumMemberDefinition> Members { get; set; } = new List<EnumMemberDefinition>();

    public AttributeCollection AttributeDefinitions { get; set; } = new AttributeCollection();

    public IList<ModifierDefinition> ModifierDefinitions { get; set; } = new List<ModifierDefinition>();

    public bool HasFlags { get; set; }

    public SyntaxKindDto BaseType { get; set; }

    public DocumentationCommentDefinition DocumentationCommentDefinition { get; set; } = new DocumentationCommentDefinition();
  }
}
