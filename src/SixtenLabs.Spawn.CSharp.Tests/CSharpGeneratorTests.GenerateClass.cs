using Xunit;
using FluentAssertions;

using System;
using static System.Environment;
using SixtenLabs.Spawn.CSharp.FluentDefinitions;

namespace SixtenLabs.Spawn.CSharp.Tests
{
  public partial class CSharpGeneratorTests
  {
    [Fact]
    public void GenerateClass_SpecNameNull_ThrowsException()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var classDef = new ClassDefinition();

      Action act = () => subject.GenerateClass(output, classDef);

      act.ShouldThrow<ArgumentNullException>("Class must at least have a valid SpecName property set.");
    }

    [Fact]
    public void GenerateClass_TranslatedNameNull_SpecNameUsed()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var classDef = new ClassDefinition("MyClass");

      var actual = subject.GenerateClass(output, classDef);

      actual.Should().Be($"class MyClass{NewLine}{{{NewLine}}}");
    }

    [Fact]
    public void GenerateClass_Constructor_IsCorrect()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var classDef = new ClassDefinition("MyClass").WithModifier(SyntaxKindDto.PublicKeyword).WithComment("Test Summary");

      var expected = $"/// <summary>{NewLine}/// Test Summary{NewLine}/// </summary>{NewLine}public class MyClass{NewLine}{{{NewLine}}}";

      var actual = subject.GenerateClass(output, classDef);

      actual.Should().Be(expected);
    }
  }
}
