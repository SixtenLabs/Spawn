namespace SixtenLabs.Spawn.CSharp.FluentDefinitions
{
  /// <summary>
  /// Fluent EnumDefinition Extensions
  /// </summary>
  public static partial class FluentDefinitionExtensions
  {
    /// <summary>
    /// Add AttributeDefintion to mark this as a Flags enum.
    /// </summary>
    /// <param name="definition"></param>
    /// <returns></returns>
    public static EnumDefinition WithFlagsAttribute(this EnumDefinition definition)
    {
      definition.WithAttribute("Flags");

      return definition;
    }
  }
}
