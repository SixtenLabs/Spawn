﻿using Xunit;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;

namespace SixtenLabs.Spawn.Test.Definitions
{
  public class ClassDefinitionTests
  {
    [Fact]
    public void Constructor_Name_IsSet()
    {
      var subject = NewSubject("Batman");

      subject.Name.Should().Be("Batman");
    }

    [Fact]
    public void AddField_FieldDefinitions_CountIsCorrect()
    {
      var subject = NewSubject("Batman");

      subject.AddField("count");

      subject.Fields.Count.Should().Be(1);
    }

    [Fact]
    public void AddField_WithDefaultValue_ValueIsCorrect()
    {
      var subject = NewSubject("Batman");

      subject.AddField("count", "int", 10);

      subject.Fields[0].DefaultValue.Value.Should().Be(10);
    }

    [Fact]
    public void AddModifier_ModifierDefinitions_CountIsCorrect()
    {
      var subject = NewSubject("Batman");

      subject.AddModifier(SyntaxKindX.PublicKeyword);

      subject.ModifierDefinitions.Count.Should().Be(1);
    }

    #region Subjects under test

    private ClassDefinition NewSubject(string name)
    {
      return new ClassDefinition(name);
    }

    #endregion
  }
}
