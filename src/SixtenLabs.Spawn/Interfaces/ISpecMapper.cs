using AutoMapper;

namespace SixtenLabs.Spawn
{
  public interface ISpecMapper<T>
  {
    void MapSpecTypes(IMapper mapper, T registry);
  }
}
