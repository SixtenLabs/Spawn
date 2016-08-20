using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.CSharp
{
	public class BlockDefinition : Definition
	{
    public BlockDefinition(string name = null)
      : base(name)
    {
    }

    public void AddStatement(string code)
		{
			var definition = new StatementDefinition("statement") { Code = code };
			Statements.Add(definition);
		}

    public bool IsEmpty
    {
      get
      {
        return Statements.Count == 0;
      }
    }

		public IList<StatementDefinition> Statements { get; set; } = new List<StatementDefinition>();
	}
}
