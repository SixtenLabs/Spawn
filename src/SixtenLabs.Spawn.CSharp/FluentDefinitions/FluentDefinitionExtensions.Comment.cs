using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.CSharp.FluentDefinitions
{
  /// <summary>
  /// Fluent CommentDefinition Extensions
  /// </summary>
  public static partial class FluentDefinitionExtensions
  {
    public static T WithComment<T>(this T parentDefinition, params string[] commentLines) where T : IHaveComments
    {
      foreach (var commentLine in commentLines)
      {
        parentDefinition.CommentDefinition.CommentLines.Add(commentLine);
      }

      return parentDefinition;
    }
  }
}
