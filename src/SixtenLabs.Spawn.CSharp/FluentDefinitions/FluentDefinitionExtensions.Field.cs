using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.CSharp.FluentDefinitions
{
  /// <summary>
  /// Fluent FieldDefinition Extensions
  /// </summary>
  public static partial class FluentDefinitionExtensions
  {
    public static FieldDefinition AddField(this IHaveFields parentDefinition, string name)
    {
      var fieldDefinition = new FieldDefinition(name);

      parentDefinition.FieldDefinitions.Add(fieldDefinition);

      return fieldDefinition;
    }

    public static FieldDefinition WithReturnType(this FieldDefinition definition, string returnType)
    {
      definition.ReturnType.OriginalName = returnType;

      return definition;
    }

    public static FieldDefinition WithDefaultValue(this FieldDefinition fieldDefinition, string defaultValue, SyntaxKindDto kind, params string[] arguments)
    {
      var literalDefinition = new LiteralDefinition(defaultValue) { LiteralType = defaultValue.GetType() };

      literalDefinition.Kind = kind;
      fieldDefinition.DefaultValue = literalDefinition;

      foreach(var argument in arguments)
      {
        var arg = new ArgumentDefinition(argument);
        literalDefinition.Arguments.Add(arg);
      }

      return fieldDefinition;
    }
  }
}
