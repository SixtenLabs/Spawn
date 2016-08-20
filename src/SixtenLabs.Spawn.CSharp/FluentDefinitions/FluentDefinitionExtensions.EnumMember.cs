using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.CSharp.FluentDefinitions
{
  /// <summary>
  /// Fluent EnumMemberDefinition Extensions
  /// </summary>
  public static partial class FluentDefinitionExtensions
  {
    public static EnumMemberDefinition WithValue(this EnumMemberDefinition definition, string value)
    {
      definition.Value = value;

      return definition;
    }

    /// <summary>
    /// Add AttributeDefintion to mark this as a Flags enum.
    /// </summary>
    /// <param name="definition"></param>
    /// <returns></returns>
    public static EnumMemberDefinition WithComments(this EnumMemberDefinition definition, params string[] comments)
    {
      foreach (var comment in comments)
      {
        definition.Comments.CommentLines.Add(comment);
      }

      return definition;
    }
  }
}
