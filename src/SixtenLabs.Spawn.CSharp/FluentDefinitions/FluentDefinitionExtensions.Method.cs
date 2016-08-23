namespace SixtenLabs.Spawn.CSharp.FluentDefinitions
{
  /// <summary>
  /// Fluent MethodDefinition Extensions
  /// </summary>
  public static partial class FluentDefinitionExtensions
  {
    public static MethodDefinition AddMethod<T>(this T parentDefinition, string name) where T : IHaveMethods
    {
      var methodDefinition = new MethodDefinition(name);
      
      parentDefinition.MethodDefinitions.Add(methodDefinition);

      return methodDefinition;
    }

    public static MethodDefinition WithReturnType(this MethodDefinition definition, string returnType)
    {
      definition.ReturnType.OriginalName = returnType;

      return definition;
    }
  }
}
