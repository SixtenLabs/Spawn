using Xunit;
using FluentAssertions;
using NSubstitute;
using System;

using static System.Environment;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace SixtenLabs.Spawn.CSharp.Tests
{
	public class GenerateStructTests
	{
		private CSharpGenerator NewSubjectUnderTest()
		{
			MockSpawnService = Substitute.For<ISpawnService>();
			MockSpawnService.Workspace.Returns(MSBuildWorkspace.Create());

			return new CSharpGenerator(MockSpawnService);
		}

		[Fact]
		public void GenerateStruct_SpecNameNull_ThrowsException()
		{
			var subject = NewSubjectUnderTest();

			var output = new OutputDefinition();
			var structDef = new StructDefinition();

			Action act = () => subject.GenerateStruct(output, structDef);

			act.ShouldThrow<ArgumentNullException>("Struct must at least have a valid SpecName property set.");
		}

		[Fact]
		public void GenerateStruct_TranslatedNameNull_SpecNameUsed()
		{
			var subject = NewSubjectUnderTest();

			var output = new OutputDefinition();
			var structDef = new StructDefinition() { SpecName = "MyStruct" };

			var actual = subject.GenerateStruct(output, structDef);

			actual.Should().Be($"struct MyStruct{NewLine}{{{NewLine}}}");
		}

		[Fact]
		public void GenerateStruct_AddFieldNoReturnType_ThrowsException()
		{
			var subject = NewSubjectUnderTest();

			var output = new OutputDefinition();
			var structDef = new StructDefinition() { SpecName = "MyStruct" };
			structDef.Fields.Add(new FieldDefinition() { SpecName = "FieldName" });

			Action act = () => subject.GenerateStruct(output, structDef);

			act.ShouldThrow<ArgumentNullException>("Field must have a return type.");
		}

		[Fact]
		public void GenerateStruct_AddField_AddsField()
		{
			var subject = NewSubjectUnderTest();

			var output = new OutputDefinition();
			var structDef = new StructDefinition() { SpecName = "MyStruct" };
			structDef.Fields.Add(new FieldDefinition() { SpecName = "FieldName", SpecReturnType = "void" });

			var actual = subject.GenerateStruct(output, structDef);

			actual.Should().Be($"struct MyStruct{NewLine}{{{NewLine}    void FieldName;{NewLine}}}");
		}

		[Fact]
		public void GenerateStruct_NameSpace_AddsNamespace()
		{
			var subject = NewSubjectUnderTest();

			var nameSpaceDef = new NamespaceDefinition() { SpecName = "SixtenLabs.StructTest" };
			var output = new OutputDefinition() { Namespace = nameSpaceDef };
			var structDef = new StructDefinition() { SpecName = "MyStruct" };

			var actual = subject.GenerateStruct(output, structDef);

			actual.Should().Be($"namespace SixtenLabs.StructTest{NewLine}{{{NewLine}    struct MyStruct{NewLine}    {{{NewLine}    }}{NewLine}}}");
		}

		[Fact]
		public void GenerateStruct_AddAttribute_AddsAttribute()
		{
			var subject = NewSubjectUnderTest();

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

		[Fact(Skip = "fix later maybe")]
		public void GenerateStruct_Constructor_IsCorrect()
		{
			var subject = NewSubjectUnderTest();

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

			structDef.Fields.Add(pointerField);

			var defaultValue = new LiteralDefinition() { Value = "new WindowHandle(IntPtr.Zero)" };
			var nullField = new FieldDefinition() { SpecName = "Null", SpecReturnType = "IntPtr", DefaultValue = defaultValue };
			nullField.AddModifier(SyntaxKindDto.PublicKeyword);
			nullField.AddModifier(SyntaxKindDto.ReadOnlyKeyword);
			nullField.AddModifier(SyntaxKindDto.StaticKeyword);

			structDef.Fields.Add(nullField);

			var actual = subject.GenerateStruct(output, structDef);

			actual.Should().Be(TestStruct);
		}

		[Fact]
		public void xx()
		{
			var subject = NewSubjectUnderTest();

			// Create using/Imports directives
			var usingDirectives = subject.Generator.NamespaceImportDeclaration("System");

			// Generate two private fields
			var lastNameField = subject.Generator.FieldDeclaration("_lastName", subject.Generator.TypeExpression(SpecialType.System_String), Accessibility.Private);
			var firstNameField = subject.Generator.FieldDeclaration("_firstName", subject.Generator.TypeExpression(SpecialType.System_String), Accessibility.Private);

			// Generate two properties with explicit get/set
			var lastNameProperty = subject.Generator.PropertyDeclaration("LastName",
				subject.Generator.TypeExpression(SpecialType.System_String), Accessibility.Public, getAccessorStatements: new SyntaxNode[]
				{ subject.Generator.ReturnStatement(subject.Generator.IdentifierName("_lastName")) }, setAccessorStatements: new SyntaxNode[]
				{ subject.Generator.AssignmentStatement(subject.Generator.IdentifierName("_lastName"), subject.Generator.IdentifierName("value"))});
			var firstNameProperty = subject.Generator.PropertyDeclaration("FirstName",
				subject.Generator.TypeExpression(SpecialType.System_String), Accessibility.Public, getAccessorStatements: new SyntaxNode[]
				{ subject.Generator.ReturnStatement(subject.Generator.IdentifierName("_firstName")) }, setAccessorStatements: new SyntaxNode[]
				{ subject.Generator.AssignmentStatement(subject.Generator.IdentifierName("_firstName"), subject.Generator.IdentifierName("value")) });

			// Generate the method body for the Clone method
			var cloneMethodBody = subject.Generator.ReturnStatement(subject.Generator.InvocationExpression(subject.Generator.IdentifierName("MemberwiseClone")));

			// Generate the Clone method declaration
			var cloneMethoDeclaration = subject.Generator.MethodDeclaration("Clone", null,
				null, null,
				Accessibility.Public,
				DeclarationModifiers.Virtual,
				new SyntaxNode[] { cloneMethodBody });

			// Generate a SyntaxNode for the interface's name you want to implement
			var ICloneableInterfaceType = subject.Generator.IdentifierName("ICloneable");

			// Explicit ICloneable.Clone implemenation
			var cloneMethodWithInterfaceType = subject.Generator.AsPublicInterfaceImplementation(cloneMethoDeclaration, ICloneableInterfaceType);

			// Generate parameters for the class' constructor
			var constructorParameters = new SyntaxNode[] {
	subject.Generator.ParameterDeclaration("LastName",
	subject.Generator.TypeExpression(SpecialType.System_String)),
	subject.Generator.ParameterDeclaration("FirstName",
	subject.Generator.TypeExpression(SpecialType.System_String)) };

			// Generate the constructor's method body
			var constructorBody = new SyntaxNode[] {
	subject.Generator.AssignmentStatement(subject.Generator.IdentifierName("_lastName"),
	subject.Generator.IdentifierName("LastName")),
	subject.Generator.AssignmentStatement(subject.Generator.IdentifierName("_firstName"),
	subject.Generator.IdentifierName("FirstName"))};

			// Generate the class' constructor
			var constructor = subject.Generator.ConstructorDeclaration("Person", constructorParameters, Accessibility.Public, statements: constructorBody);

			// An array of SyntaxNode as the class members
			var members = new SyntaxNode[] { lastNameField, firstNameField, lastNameProperty, firstNameProperty, cloneMethodWithInterfaceType, constructor };

			// Generate the class
			var classDefinition = subject.Generator.ClassDeclaration(
				"Person", typeParameters: null,
				accessibility: Accessibility.Public,
				modifiers: DeclarationModifiers.Abstract,
				baseType: null,
				interfaceTypes: new SyntaxNode[] { ICloneableInterfaceType },
				members: members);

			// Declare a namespace
			var namespaceDeclaration = subject.Generator.NamespaceDeclaration("MyTypes", classDefinition);

			// Get a CompilationUnit (code file) for the generated code
			var newNode = subject.Generator.CompilationUnit(usingDirectives, namespaceDeclaration).NormalizeWhitespace();

			var code = newNode.GetFormattedCode();
		}

		private ISpawnService MockSpawnService { get; set; }

		private const string TestStruct =
	@"[StructLayout(LayoutKind.Explicit)]
  public struct WindowHandle
  {
    private WindowHandle(IntPtr ptr)
    {
      this.pointer = pointer;
    }

    [FieldOffset(0)]
    public IntPtr pointer;

    public readonly static WindowHandle Null = new WindowHandle(IntPtr.Zero);
  }";
	}
}
