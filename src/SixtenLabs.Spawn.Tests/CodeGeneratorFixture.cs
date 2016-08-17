using Xunit;
using FluentAssertions;
using NSubstitute;

using Microsoft.CodeAnalysis;
using System;

namespace SixtenLabs.Spawn.Tests
{
	public class CodeGeneratorFixture : IDisposable
	{
		public CodeGenerator SubjectUnderTest()
		{
			MockSpawnService = Substitute.For<ISpawnService>();
			MockSpawnService.Workspace.Returns(new AdhocWorkspace());

			return new CodeGenerator(MockSpawnService, LanguageNames.CSharp);
		}

		public void Dispose()
		{
			
		}


		public ISpawnService MockSpawnService { get; set; }
	}
}
