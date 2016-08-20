using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.CSharp.FluentDefinitions
{
  /// <summary>
  /// Fluent ParameterDefinition Extensions
  /// </summary>
  public static partial class FluentDefinitionExtensions
  {
    public static ParameterDefinition WithParameterType(this ParameterDefinition definition, string returnType)
    {
      definition.ParameterType.OriginalName = returnType;

      return definition;
    }
  }
}
