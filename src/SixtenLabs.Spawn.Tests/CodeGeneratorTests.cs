using Xunit;
using FluentAssertions;

namespace SixtenLabs.Spawn.Tests
{
	public partial class CodeGeneratorTests : IClassFixture<CodeGeneratorFixture>
	{
		public CodeGeneratorTests(CodeGeneratorFixture fixture)
		{
			Fixture = fixture;
		}

		private CodeGeneratorFixture Fixture { get; }
	}
}
