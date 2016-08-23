using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.CSharp.FluentDefinitions
{
  /// <summary>
  /// Fluent ArgumentDefinition Extensions
  /// </summary>
  public static partial class FluentDefinitionExtensions
  {
    public static T WithArgument<T>(this T definition, string argument) where T : IHaveArguments
    {
      var attributeDefinition = new ArgumentDefinition(argument);
      definition.ArgumentDefinitions.Add(attributeDefinition);

      return definition;
    }

    public static T WithArguments<T>(this T definition, params string[] arguments) where T : IHaveArguments
    {
      foreach (var argument in arguments)
      {
        definition.WithArgument(argument);
      }

      return definition;
    }
  }
}
