using Xunit;
using FluentAssertions;

using System;

namespace SixtenLabs.Spawn.Tests
{
  public class DefinitionNameTests
  {
    private DefinitionName SubjectUnderTest(string original, string translated)
    {
      var definitionName = new DefinitionName();

      if (!string.IsNullOrEmpty(original))
      {
        definitionName.Original = original;
      }

      if (!string.IsNullOrEmpty(translated))
      {
        definitionName.Translated = translated;
      }

      return definitionName;
    }

    [Fact]
    public void Output_TranslatedNameEmpty_ReturnsOriginal()
    {
      var subject = SubjectUnderTest("Patrick", null);

      subject.Output.Should().Be("Patrick");
    }

    [Fact]
    public void Output_TranslatedNameSet_ReturnsTranslated()
    {
      var subject = SubjectUnderTest("Patrick", "Pat");

      subject.Output.Should().Be("Pat");
    }

    [Fact]
    public void Output_OriginalAndTranslatedNotSet_ThrowsException()
    {
      var subject = SubjectUnderTest(null, null);

      string actual = null;
      Action act = () => actual = subject.Output;

      act.ShouldThrow<ArgumentNullException>($"The OriginalName or TranslatedName must be set.");
    }
  }
}
