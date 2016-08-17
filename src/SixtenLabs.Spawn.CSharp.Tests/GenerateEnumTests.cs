using Xunit;
using FluentAssertions;
using NSubstitute;
using System;

using static System.Environment;
using Microsoft.CodeAnalysis.MSBuild;

namespace SixtenLabs.Spawn.CSharp.Tests
{
	public class GenerateEnumTests
	{
		private CSharpGenerator NewSubjectUnderTest()
		{
			MockSpawnService = Substitute.For<ISpawnService>();
			MockSpawnService.Workspace.Returns(MSBuildWorkspace.Create());

			return new CSharpGenerator(MockSpawnService);
		}

		[Fact]
		public void GenerateEnum_SpecNameNull_ThrowsException()
		{
			var subject = NewSubjectUnderTest();

			var output = new OutputDefinition();
			var enumDef = new EnumDefinition();

			Action act = () => subject.GenerateEnum(output, enumDef);

			act.ShouldThrow<ArgumentNullException>("Enum must at least have a valid SpecName property set.");
		}

		[Fact]
		public void GenerateEnum_TranslatedNameNull_SpecNameUsed()
		{
			var subject = NewSubjectUnderTest();

			var output = new OutputDefinition();
			var enumDef = new EnumDefinition() { SpecName = "FancyEnum" };

			var actual =  subject.GenerateEnum(output, enumDef);

			actual.Should().Be($"enum FancyEnum{NewLine}{{{NewLine}}}");
		}

		[Fact]
		public void GenerateEnum_BaseTypeSet_BaseTypeUsed()
		{
			var subject = NewSubjectUnderTest();

			var output = new OutputDefinition();
			var enumDef = new EnumDefinition() { SpecName = "FancyEnum", BaseType = SyntaxKindDto.UIntKeyword };

			var actual = subject.GenerateEnum(output, enumDef);

			actual.Should().Be($"enum FancyEnum : uint{NewLine}{{{NewLine}}}");
		}

		[Fact]
		public void GenerateEnum_HasFlagsTrue_FlagsAttributeSet()
		{
			var subject = NewSubjectUnderTest();

			var output = new OutputDefinition();
			var enumDef = new EnumDefinition() { SpecName = "FancyEnum", HasFlags = true };

			var actual = subject.GenerateEnum(output, enumDef);

			actual.Should().Be($"[Flags]{NewLine}enum FancyEnum{NewLine}{{{NewLine}}}");
		}

		[Fact]
		public void GenerateEnum_HasNamespace_NamespaceGenerated()
		{
			var subject = NewSubjectUnderTest();
						
			var namespaceDef = new NamespaceDefinition() { SpecName = "SixtenLabs.EnumTest" };
			var output = new OutputDefinition() { Namespace = namespaceDef };
			var enumDef = new EnumDefinition() { SpecName = "FancyEnum" };
			

			var actual = subject.GenerateEnum(output, enumDef);

			actual.Should().Be($"namespace SixtenLabs.EnumTest{NewLine}{{{NewLine}    enum FancyEnum{NewLine}    {{{NewLine}    }}{NewLine}}}");
		}

		[Fact]
		public void GenerateEnum_ModifierAdded_ModifierOutput()
		{
			var subject = NewSubjectUnderTest();

			var output = new OutputDefinition();
			var enumDef = new EnumDefinition() { SpecName = "FancyEnum" };
			enumDef.AddModifier(SyntaxKindDto.PublicKeyword);

			var actual = subject.GenerateEnum(output, enumDef);

			actual.Should().Be($"public enum FancyEnum{NewLine}{{{NewLine}}}");
		}

		[Fact]
		public void GenerateEnum_EnumValueAdded_ModifierOutput()
		{
			var subject = NewSubjectUnderTest();

			var output = new OutputDefinition();
			var enumDef = new EnumDefinition() { SpecName = "FancyEnum" };
			enumDef.Members.Add(new EnumMemberDefinition() { SpecName = "None", Value = "0" });

			var actual = subject.GenerateEnum(output, enumDef);

			actual.Should().Be($"enum FancyEnum{NewLine}{{{NewLine}    None = 0{NewLine}}}");
		}

		[Fact]
		public void GenerateEnum_TwoEnumValuesAdded_ModifierOutput()
		{
			var subject = NewSubjectUnderTest();

			var output = new OutputDefinition();
			var enumDef = new EnumDefinition() { SpecName = "FancyEnum" };
			enumDef.Members.Add(new EnumMemberDefinition() { SpecName = "None", Value = "0" });
			enumDef.Members.Add(new EnumMemberDefinition() { SpecName = "One", Value = "0x01" });

			var actual = subject.GenerateEnum(output, enumDef);

			actual.Should().Be($"enum FancyEnum{NewLine}{{{NewLine}    None = 0,{NewLine}    One = 0x01{NewLine}}}");
		}

		private ISpawnService MockSpawnService { get; set; }
	}
}
