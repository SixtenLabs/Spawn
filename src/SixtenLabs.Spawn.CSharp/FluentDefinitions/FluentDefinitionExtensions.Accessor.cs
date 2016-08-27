namespace SixtenLabs.Spawn.CSharp.FluentDefinitions
{
  /// <summary>
  /// Fluent AccessorDefinition Extensions
  /// </summary>
  public static partial class FluentDefinitionExtensions
  {
    public static AccessorDefinition AddAccessor<T>(this T parentDefinition, string name, SyntaxKindDto type, SyntaxKindDto modifier = SyntaxKindDto.None, string statement = null) where T : IHaveAccessors
    {
      var accessor = new AccessorDefinition(name);

      if (!string.IsNullOrEmpty(statement))
      {
        accessor.AddBlock(name).WithStatement(statement);
      }

      return accessor;
    }

    public static T AddGetter<T>(this T parentDefinition, SyntaxKindDto modifier = SyntaxKindDto.None, string block = null) where T : IHaveAccessors
    {
      parentDefinition.Setter = parentDefinition.AddAccessor("getter", SyntaxKindDto.GetAccessorDeclaration, modifier, block);

      return parentDefinition;
    }

    public static T AddSetter<T>(this T parentDefinition, SyntaxKindDto modifier = SyntaxKindDto.None, string block = null) where T : IHaveAccessors
    {
      parentDefinition.Setter = parentDefinition.AddAccessor("setter", SyntaxKindDto.SetAccessorDeclaration, modifier, block);

      return parentDefinition;
    }
  }
}
