using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SixtenLabs.Spawn
{
  public abstract class SpecMapper : ISpecMapper
  {
    public SpecMapper(IMapper typeMapper)
    {
      TypeMapper = typeMapper;
    }

    public abstract void MapSpecTypes();

    protected IMapper TypeMapper { get; }
  }
}
