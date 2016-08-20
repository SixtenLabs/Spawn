using System.Collections.Generic;

namespace SixtenLabs.Spawn.CSharp
{
	public class CommentDefinition : Definition
	{
    public CommentDefinition(string name = null)
      : base(name)
    {
    }

    public bool HasComments
		{
			get
			{
				return CommentLines.Count > 0;
			}
		}

		public IList<string> CommentLines { get; set; } = new List<string>(); 
	}
}
