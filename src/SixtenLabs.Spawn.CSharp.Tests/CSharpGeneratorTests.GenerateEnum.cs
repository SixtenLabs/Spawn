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
    public void GenerateEnum_SpecNameNull_ThrowsException()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var enumDef = new EnumDefinition();

      Action act = () => subject.GenerateEnum(output, enumDef);

      act.ShouldThrow<ArgumentNullException>("Enum must at least have a valid SpecName property set.");
    }

    [Fact]
    public void GenerateEnum_TranslatedNameNull_SpecNameUsed()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var enumDef = new EnumDefinition("FancyEnum");

      var actual = subject.GenerateEnum(output, enumDef);

      actual.Should().Be($"enum FancyEnum{NewLine}{{{NewLine}}}");
    }

    [Fact]
    public void GenerateEnum_BaseTypeSet_BaseTypeUsed()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var enumDef = new EnumDefinition("FancyEnum") { BaseType = SyntaxKindDto.UIntKeyword };

      var actual = subject.GenerateEnum(output, enumDef);

      actual.Should().Be($"enum FancyEnum : uint{NewLine}{{{NewLine}}}");
    }

    [Fact]
    public void GenerateEnum_HasNamespace_NamespaceGenerated()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var namespaceDef = new NamespaceDefinition("SixtenLabs.EnumTest");
      var output = new OutputDefinition() { Namespace = namespaceDef };
      var enumDef = new EnumDefinition("FancyEnum");

      var actual = subject.GenerateEnum(output, enumDef);

      actual.Should().Be($"namespace SixtenLabs.EnumTest{NewLine}{{{NewLine}    enum FancyEnum{NewLine}    {{{NewLine}    }}{NewLine}}}");
    }

    [Fact]
    public void GenerateEnum_ModifierAdded_ModifierOutput()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var enumDef = new EnumDefinition("FancyEnum").WithModifier(SyntaxKindDto.PublicKeyword);

      var actual = subject.GenerateEnum(output, enumDef);

      actual.Should().Be($"public enum FancyEnum{NewLine}{{{NewLine}}}");
    }

    [Fact]
    public void GenerateEnum_EnumValueAdded_ModifierOutput()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var enumDef = new EnumDefinition("FancyEnum");

      enumDef.AddEnumMember("None").WithValue("0");

      var actual = subject.GenerateEnum(output, enumDef);

      actual.Should().Be($"enum FancyEnum{NewLine}{{{NewLine}    None = 0{NewLine}}}");
    }

    [Fact]
    public void GenerateEnum_TwoEnumValuesAdded_ModifierOutput()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var enumDef = new EnumDefinition("FancyEnum");

      enumDef.AddEnumMember("None").WithValue("0");
      enumDef.AddEnumMember("One").WithValue("0x01");

      var actual = subject.GenerateEnum(output, enumDef);

      actual.Should().Be($"enum FancyEnum{NewLine}{{{NewLine}    None = 0,{NewLine}    One = 0x01{NewLine}}}");
    }

    [Fact]
    public void GenerateEnum_TwoEnumValuesWithComments_OutputCorrect()
   
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var enumDef = new EnumDefinition("FancyEnum");

      enumDef.AddEnumMember("None").WithValue("0").WithComment("Zero");
      enumDef.AddEnumMember("One").WithValue("0x01").WithComment("One");

      var actual = subject.GenerateEnum(output, enumDef);

      actual.Should().Be($"enum FancyEnum{NewLine}{{{NewLine}    /// <summary>\r\n        /// Zero\r\n        /// </summary>\r\n    None = 0,{NewLine}    /// <summary>\r\n        /// One\r\n        /// </summary>\r\n    One = 0x01{NewLine}}}");
    }

    [Fact]
    public void GenerateEnum_Attribute_OutputCorrect()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var enumDef = new EnumDefinition("FancyEnum");

      enumDef
        .WithModifier(SyntaxKindDto.PublicKeyword)
        .WithFlagsAttribute();

      var actual = subject.GenerateEnum(output, enumDef);

      actual.Should().Be($"[Flags()]{NewLine}public enum FancyEnum{NewLine}{{{NewLine}}}");
    }

    [Fact]
    public void GenerateEnum_WithFlagsAttribute_OutputCorrect()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var enumDef = new EnumDefinition("FancyEnum").WithFlagsAttribute();

      var actual = subject.GenerateEnum(output, enumDef);

      actual.Should().Be($"[Flags()]{NewLine}enum FancyEnum{NewLine}{{{NewLine}}}");
    }

    [Fact]
    public void GenerateEnum_WithComments_OutputCorrect()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var enumDef = new EnumDefinition("FancyEnum");
      enumDef.WithModifier(SyntaxKindDto.PublicKeyword).WithComment("line 1");

      var actual = subject.GenerateEnum(output, enumDef);

      actual.Should().Be($"/// <summary>{NewLine}/// line 1{NewLine}/// </summary>{NewLine}public enum FancyEnum{NewLine}{{{NewLine}}}");
    }
  }
}
