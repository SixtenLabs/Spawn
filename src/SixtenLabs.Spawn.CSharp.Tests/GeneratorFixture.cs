using Xunit;
using FluentAssertions;
using NSubstitute;

using System;
using Microsoft.CodeAnalysis.MSBuild;

namespace SixtenLabs.Spawn.CSharp.Tests
{
  public class GeneratorFixture : IDisposable
  {
    public CSharpGenerator NewSubjectUnderTest()
    {
      MockSpawnService = Substitute.For<ISpawnService>();
      MockSpawnService.Workspace.Returns(MSBuildWorkspace.Create());

      return new CSharpGenerator(MockSpawnService);
    }

    public void Dispose()
    {
      MockSpawnService = null;
    }

    private ISpawnService MockSpawnService { get; set; }
  }
}
