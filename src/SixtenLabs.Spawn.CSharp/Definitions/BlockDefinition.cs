using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.CSharp
{
	public class BlockDefinition : Definition, IHaveStatements
  {
    public BlockDefinition(string name = null)
      : base(name)
    {
    }

    public bool IsEmpty
    {
      get
      {
        return StatementDefinitions.Count == 0;
      }
    }

		public IList<StatementDefinition> StatementDefinitions { get; set; } = new List<StatementDefinition>();
	}
}
