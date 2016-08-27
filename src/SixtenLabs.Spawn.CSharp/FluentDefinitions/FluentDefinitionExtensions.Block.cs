namespace SixtenLabs.Spawn.CSharp.FluentDefinitions
{
  /// <summary>
  /// Fluent BlockDefinition Extensions
  /// </summary>
  public static partial class FluentDefinitionExtensions
  {
    public static BlockDefinition AddBlock<T>(this T parentDefinition, string name) where T : IHaveBlock
    {
      var blockDefinition = new BlockDefinition(name);
      parentDefinition.BlockDefinition = blockDefinition;

      return blockDefinition;
    }
  }
}
