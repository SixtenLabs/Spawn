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
      structDef.AddField("FieldName");

      Action act = () => subject.GenerateStruct(output, structDef);

      act.ShouldThrow<ArgumentNullException>("The OriginalName or TranslatedName must be set.");
    }

    [Fact]
    public void GenerateStruct_AddField_AddsField()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var structDef = new StructDefinition("MyStruct");
      structDef.AddField("FieldName").WithReturnType("void");

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
      var structDef = new StructDefinition("MyStruct");

      structDef.WithAttribute("StructLayout", "LayoutKind.Explicit");
      structDef.AddField("FieldName").WithReturnType("void").WithAttribute("FieldOffset", "0");

      var actual = subject.GenerateStruct(output, structDef);

      actual.Should().Be($"[StructLayout(LayoutKind.Explicit)]{NewLine}struct MyStruct{NewLine}{{{NewLine}    [FieldOffset(0)]{NewLine}    void FieldName;{NewLine}}}");
    }

    [Fact]
    public void GenerateStruct_Constructor_IsCorrect()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var structDef = new StructDefinition("WindowHandle");
      structDef.WithModifier(SyntaxKindDto.PublicKeyword);
      structDef.WithAttribute("StructLayout", "LayoutKind.Explicit");

      //constructor.AddCodeLineToBody("this.pointer = pointer;");

      structDef.Comments.CommentLines.Add("Test Summary");
      structDef.AddConstructor("WindowHandle").WithModifier(SyntaxKindDto.PrivateKeyword).WithParameter("pointer", "IntPtr").WithBlock("this.pointer = pointer;");

      structDef.AddField("pointer")
        .WithReturnType("IntPtr")
        .WithModifier(SyntaxKindDto.PublicKeyword)
        .WithAttribute("FieldOffset", "0");

      structDef.AddField("Null")
        .WithReturnType("WindowHandle")
        .WithDefaultValue("WindowHandle", typeof(string), SyntaxKindDto.ObjectCreationExpression, "IntPtr.Zero")
        .WithModifiers(SyntaxKindDto.PublicKeyword, SyntaxKindDto.ReadOnlyKeyword, SyntaxKindDto.StaticKeyword);

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
      structDef.WithModifier(SyntaxKindDto.PublicKeyword);

      structDef
        .AddField("color")
        .WithReturnType("float[4]")
        .WithModifiers(SyntaxKindDto.InternalKeyword, SyntaxKindDto.UnsafeKeyword, SyntaxKindDto.FixedKeyword);

      var expected = $"public struct DebugMarkerMarkerInfoExt{NewLine}{{{NewLine}    internal unsafe fixed float[4] color;{NewLine}}}";
      var actual = subject.GenerateStruct(output, structDef);

      actual.Should().Be(expected);
    }
  }
}
