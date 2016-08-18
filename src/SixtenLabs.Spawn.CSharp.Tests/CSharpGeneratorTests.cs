using Xunit;

namespace SixtenLabs.Spawn.CSharp.Tests
{
  public partial class CSharpGeneratorTests : IClassFixture<GeneratorFixture>
  {
    public CSharpGeneratorTests(GeneratorFixture fixture)
    {
      Fixture = fixture;
    }
    

    private GeneratorFixture Fixture { get; }
  }
}
