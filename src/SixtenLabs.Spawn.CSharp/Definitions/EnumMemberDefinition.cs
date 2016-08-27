namespace SixtenLabs.Spawn.CSharp
{
	public class EnumMemberDefinition : Definition, IHaveComments
	{
    public EnumMemberDefinition(string name = null)
      : base(name)
    {
    }

    public string Value { get; set; }

    public DocumentationCommentDefinition DocumentationCommentDefinition { get; set; } = new DocumentationCommentDefinition();
  }
}
