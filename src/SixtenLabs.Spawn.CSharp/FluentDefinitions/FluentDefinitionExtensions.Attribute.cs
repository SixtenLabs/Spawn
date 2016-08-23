using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.CSharp.FluentDefinitions
{
  /// <summary>
  /// Fluent AttributeDefinition Extensions
  /// </summary>
  public static partial class FluentDefinitionExtensions
  {
    public static T WithAttribute<T>(this T definition, string name, params string[] arguments) where T : IHaveAttributes
    {
      var attributeDefinition = new AttributeDefinition(name);
      attributeDefinition.WithArguments(arguments);
 
      definition.AttributeDefinitions.Add(attributeDefinition);

      return definition;
    }
  }
}
