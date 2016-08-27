using Xunit;
using FluentAssertions;
using NSubstitute;

using System;
using Microsoft.CodeAnalysis.MSBuild;
using System.IO;

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

    public string ReadClassFromFile(string name, string path)
    {
      var filePath = Path.Combine(path, name);

      return File.ReadAllText(filePath);
    }
  }
}
