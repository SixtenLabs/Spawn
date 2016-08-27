using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp
{
	public class DocumentationCommentDefinition : Definition
	{
    public DocumentationCommentDefinition()
      : base("DocumentationComment")
    {
    }

    public CommentCollection CommentDefinitions { get; set; } = new CommentCollection();
	}
}
