using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn
{
  public class DefinitionDictionary : IDefinitionDictionary
  {
    public string GetTranslatedName(string specName)
    {
      var definition = FindTypeDefinition(specName);

      if (definition == null)
      {
        throw new InvalidOperationException($"Not allowed to not have a type mapping for: {specName}");
      }
      else
      {
        return definition.Name.Translated;
      }
    }

    public string GetTranslatedChildName(string specName, string childSpecName)
    {
      var definition = FindTypeDefinition(specName);

      if (definition == null)
      {
        throw new InvalidOperationException($"Not allowed to not have a type mapping for: {specName}");
      }
      else
      {
        var childSpec = definition.Children.Where(x => x.Name.Original == childSpecName).FirstOrDefault();

        if (childSpec != null)
        {
          return childSpec.Name.Translated;
        }
        else
        {
          throw new InvalidOperationException($"Not allowed to not have a type mapping for: {childSpecName}");
        }
      }
    }

    public SpecTypeDefinition FindTypeDefinition(string specName)
    {
      return AllSpecTypeDefinitions.Where(x => x.Name.Original == specName).FirstOrDefault();
    }

    public void AddSpecTypeDefinition(SpecTypeDefinition specTypeDefinition)
    {
      var alreadyExists = AllSpecTypeDefinitions.Contains(specTypeDefinition);

      if (!alreadyExists)
      {
        AllSpecTypeDefinitions.Add(specTypeDefinition);
      }
      else
      {
        throw new InvalidOperationException($"SpecTypeDefinition for {specTypeDefinition.Name.Original} already exists.");
      }
    }

    public int SpecTypeCount
    {
      get
      {
        return AllSpecTypeDefinitions.Count;
      }
    }

    /// <summary>
		/// Mapping of all types we care about from the spec
		/// </summary>
		private IList<SpecTypeDefinition> AllSpecTypeDefinitions { get; } = new List<SpecTypeDefinition>();
  }
}
