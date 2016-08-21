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
    public void GenerateStruct_SpecNameNull_ThrowsException()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var structDef = new StructDefinition();

      Action act = () => subject.GenerateStruct(output, structDef);

      act.ShouldThrow<ArgumentNullException>("Struct must at least have a valid SpecName property set.");
    }

    [Fact]
    public void GenerateStruct_TranslatedNameNull_SpecNameUsed()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var structDef = new StructDefinition("MyStruct");

      var actual = subject.GenerateStruct(output, structDef);

      actual.Should().Be($"struct MyStruct{NewLine}{{{NewLine}}}");
    }

    [Fact]
    public void GenerateStruct_AddFieldNoReturnType_ThrowsException()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var structDef = new StructDefinition("MyStruct");
      structDef.Fields.Add(new FieldDefinition("FieldName"));

      Action act = () => subject.GenerateStruct(output, structDef);

      act.ShouldThrow<ArgumentNullException>("The OriginalName or TranslatedName must be set.");
    }

    [Fact]
    public void GenerateStruct_AddField_AddsField()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var structDef = new StructDefinition("MyStruct");
      structDef.Fields.Add(new FieldDefinition("FieldName").WithReturnType("void"));

      var actual = subject.GenerateStruct(output, structDef);

      actual.Should().Be($"struct MyStruct{NewLine}{{{NewLine}    void FieldName;{NewLine}}}");
    }

    [Fact]
    public void GenerateStruct_NameSpace_AddsNamespace()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var nameSpaceDef = new NamespaceDefinition("SixtenLabs.StructTest");
      var output = new OutputDefinition() { Namespace = nameSpaceDef };
      var structDef = new StructDefinition("MyStruct");

      var actual = subject.GenerateStruct(output, structDef);

      actual.Should().Be($"namespace SixtenLabs.StructTest{NewLine}{{{NewLine}    struct MyStruct{NewLine}    {{{NewLine}    }}{NewLine}}}");
    }

    [Fact]
    public void GenerateStruct_AddAttribute_AddsAttribute()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var attribute = new AttributeDefinition("StructLayout");
      attribute.ArgumentList.Add("LayoutKind.Explicit");
      var structDef = new StructDefinition("MyStruct");
      structDef.Attributes.Add(attribute);

      var fieldDef = new FieldDefinition("FieldName").WithReturnType("void");
      var fieldAttribute = new AttributeDefinition("FieldOffset");
      fieldAttribute.ArgumentList.Add("0");
      fieldDef.AttributeDefinitions.Add(fieldAttribute);
      structDef.Fields.Add(fieldDef);

      var actual = subject.GenerateStruct(output, structDef);

      actual.Should().Be($"[StructLayout(LayoutKind.Explicit)]{NewLine}struct MyStruct{NewLine}{{{NewLine}    [FieldOffset(0)]{NewLine}    void FieldName;{NewLine}}}");
    }

    [Fact]
    public void GenerateStruct_Constructor_IsCorrect()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var structDef = new StructDefinition("WindowHandle");
      structDef.AddModifier(SyntaxKindDto.PublicKeyword);
      var attribute = new AttributeDefinition("StructLayout");
      attribute.ArgumentList.Add("LayoutKind.Explicit");
      structDef.Attributes.Add(attribute);

      var constructor = new ConstructorDefinition("WindowHandle");
      constructor.AddModifier(SyntaxKindDto.PrivateKeyword);
      constructor.AddCodeLineToBody("this.pointer = pointer;");
      constructor.AddParameter("pointer", "IntPtr");

      structDef.Comments.CommentLines.Add("Test Summary");
      structDef.Constructors.Add(constructor);

      structDef.AddField("pointer")
        .WithReturnType("IntPtr")
        .AddModifier(SyntaxKindDto.PublicKeyword)
        .AddAttribute("FieldOffset", "0");

      var defaultValue = new LiteralDefinition("WindowHandle") { Kind = SyntaxKindDto.ObjectCreationExpression };
      defaultValue.Arguments.Add(new ArgumentDefinition("IntPtr.Zero"));

      structDef.AddField("Null")
        .WithReturnType("WindowHandle")
        .WithDefaultValue(defaultValue)
        .AddModifier(SyntaxKindDto.PublicKeyword)
        .AddModifier(SyntaxKindDto.ReadOnlyKeyword)
        .AddModifier(SyntaxKindDto.StaticKeyword);

      var expected = $"/// <summary>{NewLine}/// Test Summary{NewLine}/// </summary>{NewLine}[StructLayout(LayoutKind.Explicit)]{NewLine}public struct WindowHandle{NewLine}{{{NewLine}    [FieldOffset(0)]{NewLine}    public IntPtr pointer;{NewLine}    public readonly static WindowHandle Null = new WindowHandle(IntPtr.Zero);{NewLine}    private WindowHandle(IntPtr @pointer){NewLine}    {{{NewLine}        this.pointer = pointer;{NewLine}    }}{NewLine}}}";

      var actual = subject.GenerateStruct(output, structDef);

      actual.Should().Be(expected);
    }

    [Fact]
    public void GenerateStruct_WithArrayField_IsCorrect()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var structDef = new StructDefinition("DebugMarkerMarkerInfoExt");
      structDef.AddModifier(SyntaxKindDto.PublicKeyword);

      structDef.AddField("color")
        .WithReturnType("float[4]")
        .AddModifier(SyntaxKindDto.InternalKeyword)
        .AddModifier(SyntaxKindDto.UnsafeKeyword)
        .AddModifier(SyntaxKindDto.FixedKeyword);

      var expected = $"public struct DebugMarkerMarkerInfoExt{NewLine}{{{NewLine}    internal unsafe fixed float[4] color;{NewLine}}}";
      var actual = subject.GenerateStruct(output, structDef);

      actual.Should().Be(expected);
    }
  }
}
