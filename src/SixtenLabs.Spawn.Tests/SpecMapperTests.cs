using Xunit;
using FluentAssertions;
using NSubstitute;
using System;
using AutoMapper;

namespace SixtenLabs.Spawn.Tests
{
  public class SpecMapperTests
  {
    private TestSpecMapper SubjectUnderTest()
    {
      MockTypeMapper = Substitute.For<IMapper>();
      
      return new TestSpecMapper(MockTypeMapper);
    }

    [Fact]
    public void MapSpecTypes_WhenCalled_TypeMapperCalledOnce()
    {
      var subject = SubjectUnderTest();

      subject.MapSpecTypes(new TestRegistry());

      MockTypeMapper.Received(1).Map<string, string>(Arg.Any<string>());
    }

    private IMapper MockTypeMapper { get; set; }
  }
}
