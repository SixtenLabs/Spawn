using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.CSharp.FluentDefinitions
{
  /// <summary>
  /// Fluent EnumMemberDefinition Extensions
  /// </summary>
  public static partial class FluentDefinitionExtensions
  {
    public static EnumMemberDefinition AddEnumMember(this EnumDefinition parentDefinition, string name)
    {
      var member = new EnumMemberDefinition(name);
      parentDefinition.Members.Add(member);

      return member;
    }

    public static EnumMemberDefinition WithValue(this EnumMemberDefinition definition, string value)
    {
      definition.Value = value;

      return definition;
    }
  }
}
