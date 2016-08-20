namespace SixtenLabs.Spawn.CSharp.FluentDefinitions
{
  /// <summary>
  /// Fluent ClassDefinition Extensions
  /// </summary>
  public static partial class FluentDefinitionExtensions
  {
    public static ClassDefinition AddField(this ClassDefinition classDefinition, string name, string returnType = "string", string defaultValue = null)
    {
      var fieldDefinition = new FieldDefinition(name).WithReturnType(returnType);

      if (defaultValue != null)
      {
        var literalDefinition = new LiteralDefinition() { Value = defaultValue, LiteralType = defaultValue.GetType() };
        fieldDefinition.WithDefaultValue(literalDefinition);
      }

      return classDefinition;
    }

    public static ClassDefinition AddProperty(this ClassDefinition classDefinition, string name, string returnType = "string", string defaultValue = null)
    {
      var definition = new PropertyDefinition(name).WithReturnType(returnType);

      if (defaultValue != null)
      {
        var literalDefinition = new LiteralDefinition() { Value = defaultValue, LiteralType = defaultValue.GetType() };
        definition.WithDefaultValue(literalDefinition);
      }

      return classDefinition;
    }

    public static ClassDefinition AddModifier(this ClassDefinition definition, SyntaxKindDto kind)
    {
      var modifierDefinition = new ModifierDefinition() { Modifier = kind };
      definition.ModifierDefinitions.Add(modifierDefinition);

      return definition;
    }
  }
}
