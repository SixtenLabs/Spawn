using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.CSharp.FluentDefinitions
{
  /// <summary>
  /// Fluent StructDefinition Extensions
  /// </summary>
  public static partial class FluentDefinitionExtensions
  {
    public static FieldDefinition AddField(this StructDefinition definition, string name, string returnType = "string", string defaultValue = null)
    {
      var fieldDefinition = new FieldDefinition(name).WithReturnType(returnType);

       if (defaultValue != null)
      {
        var literalDefinition = new LiteralDefinition() { Value = defaultValue, LiteralType = defaultValue.GetType() };
        fieldDefinition.WithDefaultValue(literalDefinition);
      }
      
      definition.Fields.Add(fieldDefinition);

      return fieldDefinition;
    }

    public static PropertyDefinition AddProperty(this StructDefinition definition, string name, string returnType = "string", string defaultValue = null)
    {
      var propertyDefinition = new PropertyDefinition(name).WithReturnType(returnType);

      if (defaultValue != null)
      {
        var literalDefinition = new LiteralDefinition() { Value = defaultValue, LiteralType = defaultValue.GetType() };
        propertyDefinition.WithDefaultValue(literalDefinition);
      }

      definition.Properties.Add(propertyDefinition);

      return propertyDefinition;
    }

    public static StructDefinition AddModifier(this StructDefinition definition, SyntaxKindDto kind)
    {
      var modifierDefinition = new ModifierDefinition() { Modifier = kind };
      definition.ModifierDefinitions.Add(modifierDefinition);

      return definition;
    }
  }
}
