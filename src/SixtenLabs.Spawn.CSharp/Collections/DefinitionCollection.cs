using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.CSharp
{
  public abstract class DefinitionCollection<T> : List<T> where T : Definition
  {
    public DefinitionCollection()
    {
    }

    public bool IsEmpty
    {
      get
      {
        return Count == 0;
      }
    }

    public bool IsNotEmpty
    {
      get
      {
        return Count > 0;
      }
    }
  }
}
