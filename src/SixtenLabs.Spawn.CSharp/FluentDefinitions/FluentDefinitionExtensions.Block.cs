namespace SixtenLabs.Spawn.CSharp.FluentDefinitions
{
  /// <summary>
  /// Fluent BlockDefinition Extensions
  /// </summary>
  public static partial class FluentDefinitionExtensions
  {
    public static T WithBlock<T>(this T parentDefinition, params string[] codeLines) where T : IHaveBlock
    {
      foreach(var codeLine in codeLines)
      {
        parentDefinition.BlockDefinition.AddStatement(codeLine);
      }

      return parentDefinition;
    }
  }
}
