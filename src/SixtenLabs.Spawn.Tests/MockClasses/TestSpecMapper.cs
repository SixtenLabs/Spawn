using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.Tests
{
  public class TestSpecMapper : SpecMapper
  {
    public TestSpecMapper(IMapper typeMapper)
      : base(typeMapper)
    {

    }

    public override void MapSpecTypes()
    {
      var map = TypeMapper.Map<string, string>("bob");
    }
  }
}
