using AutoMapper;

namespace SixtenLabs.Spawn.Tests
{
  public class TestSpecMapper : SpecMapper<TestRegistry>
  {
    public override void MapSpecTypes(IMapper mapper, TestRegistry registry)
    {
      var map = mapper.Map<string, string>("bob");
    }
  }
}
