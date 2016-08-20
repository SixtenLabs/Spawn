namespace SixtenLabs.Spawn.CSharp.FluentDefinitions
{
  /// <summary>
  /// Fluent EnumDefinition Extensions
  /// </summary>
  public static partial class FluentDefinitionExtensions
  {
    public static EnumDefinition AddEnumMember(this EnumDefinition definition, string name, string value, params string[] comments)
    {
      var member = new EnumMemberDefinition(name).WithValue(value).WithComments(comments);
      definition.Members.Add(member);

      return definition;
    }

    public static EnumDefinition AddModifier(this EnumDefinition definition, SyntaxKindDto kind)
    {
      var modifierDefinition = new ModifierDefinition() { Modifier = kind };
      definition.ModifierDefinitions.Add(modifierDefinition);

      return definition;
    }

    public static EnumDefinition AddAttribute(this EnumDefinition definition, string name, params string[] arguments)
    {
      var attributeDefinition = new AttributeDefinition(name);

      foreach (var argument in arguments)
      {
        attributeDefinition.ArgumentList.Add(argument);
      }

      definition.AttributeDefinitions.Add(attributeDefinition);

      return definition;
    }

    /// <summary>
    /// Add AttributeDefintion to mark this as a Flags enum.
    /// </summary>
    /// <param name="definition"></param>
    /// <returns></returns>
    public static EnumDefinition WithFlagsAttribute(this EnumDefinition definition)
    {
      definition.AddAttribute("Flags");

      return definition;
    }

    /// <summary>
    /// Add AttributeDefintion to mark this as a Flags enum.
    /// </summary>
    /// <param name="definition"></param>
    /// <returns></returns>
    public static EnumDefinition WithComments(this EnumDefinition definition, params string[] comments)
    {
      foreach (var comment in comments)
      {
        definition.Comments.CommentLines.Add(comment);
      }
      
      return definition;
    }
  }
}
