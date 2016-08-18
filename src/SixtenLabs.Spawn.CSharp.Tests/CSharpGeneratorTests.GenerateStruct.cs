using Xunit;
using FluentAssertions;

using System;
using static System.Environment;

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
      var structDef = new StructDefinition() { SpecName = "MyStruct" };

      var actual = subject.GenerateStruct(output, structDef);

      actual.Should().Be($"struct MyStruct{NewLine}{{{NewLine}}}");
    }

    [Fact]
    public void GenerateStruct_AddFieldNoReturnType_ThrowsException()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var structDef = new StructDefinition() { SpecName = "MyStruct" };
      structDef.Fields.Add(new FieldDefinition() { SpecName = "FieldName" });

      Action act = () => subject.GenerateStruct(output, structDef);

      act.ShouldThrow<ArgumentNullException>("Field must have a return type.");
    }

    [Fact]
    public void GenerateStruct_AddField_AddsField()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var structDef = new StructDefinition() { SpecName = "MyStruct" };
      structDef.Fields.Add(new FieldDefinition() { SpecName = "FieldName", SpecReturnType = "void" });

      var actual = subject.GenerateStruct(output, structDef);

      actual.Should().Be($"struct MyStruct{NewLine}{{{NewLine}    void FieldName;{NewLine}}}");
    }

    [Fact]
    public void GenerateStruct_NameSpace_AddsNamespace()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var nameSpaceDef = new NamespaceDefinition() { SpecName = "SixtenLabs.StructTest" };
      var output = new OutputDefinition() { Namespace = nameSpaceDef };
      var structDef = new StructDefinition() { SpecName = "MyStruct" };

      var actual = subject.GenerateStruct(output, structDef);

      actual.Should().Be($"namespace SixtenLabs.StructTest{NewLine}{{{NewLine}    struct MyStruct{NewLine}    {{{NewLine}    }}{NewLine}}}");
    }

    [Fact]
    public void GenerateStruct_AddAttribute_AddsAttribute()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var attribute = new AttributeDefinition() { SpecName = "StructLayout" };
      attribute.ArgumentList.Add("LayoutKind.Explicit");
      var structDef = new StructDefinition() { SpecName = "MyStruct" };
      structDef.Attributes.Add(attribute);

      var fieldDef = new FieldDefinition() { SpecName = "FieldName", SpecReturnType = "void" };
      var fieldAttribute = new AttributeDefinition() { SpecName = "FieldOffset" };
      fieldAttribute.ArgumentList.Add("0");
      fieldDef.Attributes.Add(fieldAttribute);
      structDef.Fields.Add(fieldDef);

      var actual = subject.GenerateStruct(output, structDef);

      actual.Should().Be($"[StructLayout(LayoutKind.Explicit)]{NewLine}struct MyStruct{NewLine}{{{NewLine}    [FieldOffset(0)]{NewLine}    void FieldName;{NewLine}}}");
    }

    [Fact]
    public void GenerateStruct_Constructor_IsCorrect()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var structDef = new StructDefinition() { SpecName = "WindowHandle" };
      structDef.AddModifier(SyntaxKindDto.PublicKeyword);
      var attribute = new AttributeDefinition() { SpecName = "StructLayout" };
      attribute.ArgumentList.Add("LayoutKind.Explicit");
      structDef.Attributes.Add(attribute);

      var constructor = new ConstructorDefinition() { SpecName = "WindowHandle" };
      constructor.AddModifier(SyntaxKindDto.PrivateKeyword);
      constructor.AddCodeLineToBody("this.pointer = pointer;");
      constructor.AddParameter("pointer", "IntPtr");

      structDef.Comments.CommentLines.Add("Test Summary");
      structDef.Constructors.Add(constructor);

      var pointerField = new FieldDefinition() { SpecName = "pointer", SpecReturnType = "IntPtr" };
      pointerField.AddModifier(SyntaxKindDto.PublicKeyword);
      var attributePointerField = new AttributeDefinition() { SpecName = "FieldOffset" };
      attributePointerField.ArgumentList.Add("0");
      pointerField.Attributes.Add(attributePointerField);

      structDef.Fields.Add(pointerField);

      var defaultValue = new LiteralDefinition() { SpecName = "WindowHandle", Kind = SyntaxKindDto.ObjectCreationExpression };
      defaultValue.Arguments.Add(new ArgumentDefinition() { SpecName = "IntPtr.Zero" });
      var nullField = new FieldDefinition() { SpecName = "Null", SpecReturnType = "WindowHandle", DefaultValue = defaultValue };
      nullField.AddModifier(SyntaxKindDto.PublicKeyword);
      nullField.AddModifier(SyntaxKindDto.ReadOnlyKeyword);
      nullField.AddModifier(SyntaxKindDto.StaticKeyword);

      structDef.Fields.Add(nullField);

      var actual = subject.GenerateStruct(output, structDef);

      actual.Should().Be(TestStruct);
    }

    private string TestStruct = $"[StructLayout(LayoutKind.Explicit)]{NewLine}public struct WindowHandle{NewLine}{{{NewLine}    [FieldOffset(0)]{NewLine}    public IntPtr pointer;{NewLine}    public readonly static WindowHandle Null = new WindowHandle(IntPtr.Zero);{NewLine}    private WindowHandle(IntPtr @pointer){NewLine}    {{{NewLine}      this.pointer = pointer;{NewLine}    }}{NewLine}}}";
  }
}
