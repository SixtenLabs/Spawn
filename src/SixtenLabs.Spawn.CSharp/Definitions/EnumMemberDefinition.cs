namespace SixtenLabs.Spawn.CSharp
{
	public class EnumMemberDefinition : Definition
	{
    public EnumMemberDefinition(string name = null)
      : base(name)
    {
    }

    public string Value { get; set; }

		public CommentDefinition Comments { get; set; } = new CommentDefinition();
	}
}
