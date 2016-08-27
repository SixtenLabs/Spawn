using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.CSharp.FluentDefinitions
{
  /// <summary>
  /// Fluent DocumentationCommentDefinition Extensions
  /// </summary>
  public static partial class FluentDefinitionExtensions
  {
    public static T WithComment<T>(this T parentDefinition, params string[] commentLines) where T : IHaveComments
    {
      foreach (var commentLine in commentLines)
      {
        var commentDefinition = new CommentDefinition();
        parentDefinition.DocumentationCommentDefinition.CommentDefinitions.Add(commentDefinition);
      }

      return parentDefinition;
    }
  }
}
