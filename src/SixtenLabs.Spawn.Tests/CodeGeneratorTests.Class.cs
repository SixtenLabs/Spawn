using Xunit;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace SixtenLabs.Spawn.Tests
{
	public partial class CodeGeneratorTests
	{
		[Fact]
		public void xx()
		{
			var subject = Fixture.SubjectUnderTest();

			var outputDefinition = new OutputDefinition();

			outputDefinition.AddStandardUsingDirective("System");

			var classDefinition = new ClassDefinition() { SpecName = "TestClass" };

			classDefinition.AccessModifier = Accessibility.Public;
			classDefinition.DeclarationModifiers = DeclarationModifiers.Static;

			var field = new FieldDefinition()
			{
				SpecName = "pointer",
				AccessModifier = Accessibility.Private,
				DeclarationModifiers = DeclarationModifiers.None,
				FieldType = SpecialType.System_String,
				Initializer = "Bob"
			};

			classDefinition.Fields.Add(field);

			var constructor = new ConstructorDefinition() { SpecName = "TestClass", AccessModifier = Accessibility.Public };
			classDefinition.Constructors.Add(constructor);

			var actual = subject.GenerateClass(outputDefinition, classDefinition);

			actual.Should().NotBeNull();
		}
	}
}
