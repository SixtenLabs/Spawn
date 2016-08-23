using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.CSharp.FluentDefinitions
{
  /// <summary>
  /// Fluent StatementDefinition Extensions
  /// </summary>
  public static partial class FluentDefinitionExtensions
  {
    public static T AddStatement<T>(this T parentDefinition, string code) where T : IHaveStatements
    {
      var definition = new StatementDefinition("statement") { Code = code };
      parentDefinition.StatementDefinitions.Add(definition);

      return parentDefinition;
    }
  }
}
