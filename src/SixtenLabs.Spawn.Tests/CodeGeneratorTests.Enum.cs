using Xunit;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace SixtenLabs.Spawn.Tests
{
	public partial class CodeGeneratorTests
	{
		[Fact]
		public void enumTest()
		{
			var subject = Fixture.SubjectUnderTest();

			var outputDefinition = new OutputDefinition();

			outputDefinition.AddStandardUsingDirective("System");

			var definition = new EnumDefinition() { SpecName = "TestEnum", IsFlags = true };

			definition.AccessModifier = Accessibility.Public;
			definition.DeclarationModifiers = DeclarationModifiers.Static;

			var member1 = new EnumMemberDefinition() { SpecName = "None", Value = 0 };
			definition.Members.Add(member1);

			var member2 = new EnumMemberDefinition() { SpecName = "XPath", Value = 1 };
			definition.Members.Add(member2);

			var member3 = new EnumMemberDefinition() { SpecName = "YPath", Value = 2 };
			definition.Members.Add(member3);

			var member4 = new EnumMemberDefinition() { SpecName = "ZPath", Value = 3 };
			definition.Members.Add(member4);

			var actual = subject.GenerateEnum(outputDefinition, definition);

			actual.Should().NotBeNull();
		}
	}
}
