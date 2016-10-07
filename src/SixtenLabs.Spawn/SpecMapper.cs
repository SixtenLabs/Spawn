using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SixtenLabs.Spawn
{
  public abstract class SpecMapper<T> : ISpecMapper<T> where T : class
  {
    public abstract void MapSpecTypes(IMapper mapper, T registry);
  }
}
