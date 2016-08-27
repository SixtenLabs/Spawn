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

    [Fact]
    public void GenerateStruct_WithMethod_IsCorrect()
    {
      var subject = Fixture.NewSubjectUnderTest();
       
      var output = new OutputDefinition();

      output.AddStandardUsingDirective("System");
      output.AddStandardUsingDirective("System.Runtime.InteropServices");
      output.AddNamespace("SixtenLabs.Interop.Glfw");

      var structDef = new StructDefinition("CursorHandle");

      structDef
        .WithModifier(SyntaxKindDto.PublicKeyword)
        .WithAttribute("StructLayout", "LayoutKind.Explicit")
        .WithComment("Opaque cursor object.", "", "typedef struct GLFWcursor GLFWcursor;", "", "Added in version 3.1.");

      structDef.AddConstructor("CursorHandle")
        .WithModifier(SyntaxKindDto.PrivateKeyword)
        .WithParameter("innerPointer", "IntPtr")
        .AddBlock("body")
        .WithStatement("this.innerPointer = innerPointer;");

      structDef.AddMethod("TestMethod")
          .WithModifier(SyntaxKindDto.PublicKeyword)
          .WithReturnType("string")
          .WithParameter("test", "string")
          .AddBlock("body").WithStatement(@"var x = ""test"";").WithStatement("return x;");

      structDef.AddField("innerPointer")
        .WithReturnType("IntPtr")
        .WithModifier(SyntaxKindDto.PublicKeyword)
        .WithAttribute("FieldOffset", "0");

      structDef.AddField("Null")
        .WithReturnType("CursorHandle")
        .WithDefaultValue("CursorHandle", typeof(string), SyntaxKindDto.ObjectCreationExpression, "IntPtr.Zero")
        .WithModifiers(SyntaxKindDto.PublicKeyword, SyntaxKindDto.ReadOnlyKeyword, SyntaxKindDto.StaticKeyword);

      var expected = Fixture.ReadClassFromFile("CursorHandle.txt", "TestFiles");

      var actual = subject.GenerateStruct(output, structDef);

      actual.Should().Be(expected);
    }
  }
}
