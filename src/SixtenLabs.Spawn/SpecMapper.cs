using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SixtenLabs.Spawn
{
  public abstract class SpecMapper<T> : ISpecMapper<T> where T : class
  {
    public SpecMapper(IMapper typeMapper)
    {
      TypeMapper = typeMapper;
    }

    public abstract void MapSpecTypes(T registry);

    protected IMapper TypeMapper { get; }
  }
}
