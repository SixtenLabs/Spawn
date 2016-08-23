using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.CSharp.FluentDefinitions
{
  /// <summary>
  /// Fluent ConstructorDefinition Extensions
  /// </summary>
  public static partial class FluentDefinitionExtensions
  {
    public static ConstructorDefinition AddConstructor<T>(this T definition, string name) where T : IHaveConstructors
    {
      var constructorDefinition = new ConstructorDefinition(name);

      definition.ConstructorDefinitions.Add(constructorDefinition);

      return constructorDefinition;
    }
  }
}
