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
    public void GetTranslatedName_NoSpecTypeDefinitionExists_ThrowsException()
    {
      var subject = SubjectUnderTest();

      Action act = () => subject.GetTranslatedName("Bob");

      act.ShouldThrow<InvalidOperationException>($"Not allowed to not have a type mapping for: Bob");
    }

    [Fact]
    public void GetTranslatedChildName_NoSpecTypeDefinitionExists_ThrowsException()
    {
      var subject = SubjectUnderTest();

      Action act = () => subject.GetTranslatedChildName("Bob", "Timothy");

      act.ShouldThrow<InvalidOperationException>($"Not allowed to not have a type mapping for: Bob");
    }

    [Fact]
    public void GetTranslatedChildName_NoChildSpecTypeDefinitionExists_ThrowsException()
    {
      var subject = SubjectUnderTest();

      var specTypeDef = new SpecTypeDefinition();
      specTypeDef.Name.Original = "Robert";
      specTypeDef.Name.Translated = "Bob";
      subject.AddSpecTypeDefinition(specTypeDef);

      Action act = () => subject.GetTranslatedChildName("Robert", "Timothy");

      act.ShouldThrow<InvalidOperationException>($"Not allowed to not have a type mapping for: Timothy");
    }

    [Fact]
    public void AddSpecTypeDefinition_NewSpecDef_SpecTypeCountCorrect()
    {
      var subject = SubjectUnderTest();

      var specTypeDef = new SpecTypeDefinition();
      specTypeDef.Name.Original = "Robert";
      specTypeDef.Name.Translated = "Bob";

      subject.AddSpecTypeDefinition(specTypeDef);

      subject.SpecTypeCount.Should().Be(1);
    }

    [Fact]
    public void AddSpecTypeDefinition_SpecTypeDefinitionExists_ThrowsException()
    {
      var subject = SubjectUnderTest();

      var specTypeDef = new SpecTypeDefinition();
      specTypeDef.Name.Original = "Robert";
      specTypeDef.Name.Translated = "Bob";

      subject.AddSpecTypeDefinition(specTypeDef);

      Action act = () => subject.AddSpecTypeDefinition(specTypeDef);

      act.ShouldThrow<InvalidOperationException>($"SpecTypeDefinition for {specTypeDef.Name.Original} already exists.");
    }

    [Fact]
    public void GetTranslatedName_SpecTypeExists_ReturnsTranslatedName()
    {
      var subject = SubjectUnderTest();

      var specTypeDef = new SpecTypeDefinition();
      specTypeDef.Name.Original = "Robert";
      specTypeDef.Name.Translated = "Bob";

      subject.AddSpecTypeDefinition(specTypeDef);

      var actual = subject.GetTranslatedName("Robert");

      actual.Should().Be("Bob");
    }

    [Fact]
    public void GetTranslatedChildName_SpecTypeExists_ReturnsTranslatedName()
    {
      var subject = SubjectUnderTest();

      var specTypeDef = new SpecTypeDefinition();
      specTypeDef.Name.Original = "Robert";
      specTypeDef.Name.Translated = "Bob";

      var child = new SpecTypeDefinition();
      child.Name.Original = "Timothy";
      child.Name.Translated = "Tim";
      specTypeDef.Children.Add(child);
      subject.AddSpecTypeDefinition(specTypeDef);

      var actual = subject.GetTranslatedChildName("Robert", "Timothy");

      actual.Should().Be("Tim");
    }

    [Fact]
    public void MapSpecTypes_WhenCalled_TypeMapperCalledOnce()
    {
      var subject = SubjectUnderTest();

      subject.MapSpecTypes();

      MockTypeMapper.Received(1).Map<string, string>(Arg.Any<string>());
    }

    private IMapper MockTypeMapper { get; set; }
  }
}
