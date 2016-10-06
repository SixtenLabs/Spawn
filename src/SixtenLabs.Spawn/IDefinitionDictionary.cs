using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn
{
  public interface IDefinitionDictionary
  {
    SpecTypeDefinition FindTypeDefinition(string specName);

    string GetTranslatedName(string specName);

    string GetTranslatedChildName(string specName, string childSpecName);

    void AddSpecTypeDefinition(SpecTypeDefinition specTypeDefinition);

    int SpecTypeCount { get; }
  }
}
