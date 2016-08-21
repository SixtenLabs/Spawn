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

		[Fact(Skip="This creates files. Run manaully if testing is needed on file creation.")]
		public void xx()
		{
			var subject = SubjectUnderTest();

      subject.Initialize(@"C:\Users\pglas\Documents\GitHub\SixtenLabs\Spawn\Spawn.sln");

			subject.AddDocumentToProject("SixtenLabs.Spawn.Tests", "Api.Instance", "cs", "public class Test{  }", new[] { "Api" } );
		}
	}
}
