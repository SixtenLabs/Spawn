using Xunit;
using FluentAssertions;
using NSubstitute;

namespace SixtenLabs.Spawn.Tests
{
	public class SpawnServiceTests
	{
		private SpawnService SubjectUnderTest()
		{
			return new SpawnService();
		}

    /// <summary>
    /// have not been able to create standard naming convention for file nesting.
    /// 
    /// Api.Instance.cs this pattern does not work. Ends up as Api.cs (I am guessing multiple periods is a no no)
    /// </summary>
    [Fact(Skip="Run manually only. Just a place to test if creating files in a project works properly.")]
		public void AddDocumentToProject()
		{
			var subject = SubjectUnderTest();

      subject.Initialize(@"C:\Users\pglas\Documents\GitHub\SixtenLabs\Spawn\Spawn.sln");

			subject.AddDocumentToProject("SixtenLabs.Spawn.Tests", "ApiInstance", "cs", "public partial class Api{  }", new[] { "Api" } );

      subject.AddDocumentToProject("SixtenLabs.Spawn.Tests", "ApiDevice", "cs", "public partial class Api{  }", new[] { "Api" });
    }

    [Fact(Skip="Only run this manually. We do not need to run this every time unit tests change.")]
    public void xxx()
    {
      var subject = SubjectUnderTest();

      subject.Initialize(@"C:\Users\pglas\Documents\GitHub\SixtenLabs\Spawn\Spawn.sln");

      subject.FlushFolderOfDocuments("SixtenLabs.Spawn.Tests", "Api");
    }
  }
}
