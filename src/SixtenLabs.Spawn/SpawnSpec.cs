using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SixtenLabs.Spawn
{
  public abstract class SpawnSpec<T> : ISpawnSpec<T> where T : class
  {
    public SpawnSpec(XmlFileLoader xmlFileLoader, ISpecMapper specMapper)
    {
      FileLoader = xmlFileLoader;
      SpecMapper = specMapper;
    }

    public void ProcessRegistry()
    {
      FileLoader.LoadRegistry();
    }

    public void CreateSpecTree(IMapper mapper)
    {
      SpecTree = mapper.Map<T>(FileLoader.Registry);
    }

    private XmlFileLoader FileLoader { get; set; }

    /// <summary>
    /// The spectree holds the class that comprise the objects that we are 
    /// generating code for. This is typically created by hand or from an xml specification
    /// and we use this property to query the Specification and setup the structure of the code to generate.
    /// </summary>
    public T SpecTree { get; set; }

    public ISpecMapper SpecMapper { get; }
  }
}
