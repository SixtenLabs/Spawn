using Xunit;
using FluentAssertions;

using System;
using static System.Environment;
using SixtenLabs.Spawn.CSharp.FluentDefinitions;

namespace SixtenLabs.Spawn.CSharp.Tests
{
  public partial class CSharpGeneratorTests
  {
    [Fact]
    public void GenerateClass_SpecNameNull_ThrowsException()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var classDef = new ClassDefinition();

      Action act = () => subject.GenerateClass(output, classDef);

      act.ShouldThrow<ArgumentNullException>("Class must at least have a valid SpecName property set.");
    }

    [Fact]
    public void GenerateClass_TranslatedNameNull_SpecNameUsed()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var classDef = new ClassDefinition("MyClass");

      var actual = subject.GenerateClass(output, classDef);

      actual.Should().Be($"class MyClass{NewLine}{{{NewLine}}}");
    }

    [Fact]
    public void GenerateClass_Constructor_IsCorrect()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      var classDef = new ClassDefinition("MyClass").WithModifier(SyntaxKindDto.PublicKeyword);

      var expected = $"public class MyClass{NewLine}{{{NewLine}}}";

      var actual = subject.GenerateClass(output, classDef);

      actual.Should().Be(expected);
    }

    [Fact]
    public void GeneratorClass_WithMethods_IsCorrect()
    {
      var subject = Fixture.NewSubjectUnderTest();

      var output = new OutputDefinition();
      
      output.AddStandardUsingDirective("System");
      output.AddNamespace("SixtenLabs.Interop.Glfw");

      var classDef = new ClassDefinition("GlfwApi").WithModifiers(SyntaxKindDto.PublicKeyword, SyntaxKindDto.PartialKeyword);

      classDef
        .AddMethod("MakeContextCurrent")
        .WithModifier(SyntaxKindDto.PublicKeyword)
        .WithReturnType("void")
        .WithParameter("window", "WindowHandle")
        .AddBlock("body")
        .WithStatement("Delegates.glfwMakeContextCurrent(window);");

      classDef
        .AddMethod("GetCurrentContext")
        .WithModifier(SyntaxKindDto.PublicKeyword)
        .WithReturnType("WindowHandle")
        .AddBlock("body")
        .WithStatement("return Delegates.glfwGetCurrentContext();");

      classDef
        .AddMethod("SwapInterval")
        .WithModifier(SyntaxKindDto.PublicKeyword)
        .WithReturnType("void")
        .WithParameter("interval", "int")
        .AddBlock("body")
        .WithStatement("Delegates.glfwSwapInterval(interval);");

      classDef
        .AddMethod("ExtensionSupported")
        .WithModifier(SyntaxKindDto.PublicKeyword)
        .WithReturnType("bool")
        .WithParameter("extension", "string")
        .AddBlock("body")
        .WithStatement("return Delegates.glfwExtensionSupported(extension) == 1;");

      classDef
        .AddMethod("GetProcAddress")
        .WithModifier(SyntaxKindDto.PublicKeyword)
        .WithReturnType("IntPtr")
        .WithParameter("procname", "string")
        .AddBlock("body")
        .WithStatement("return Delegates.glfwGetProcAddress(procname);");

      var expected = Fixture.ReadClassFromFile("GlfwApi.txt", "TestFiles");

      var actual = subject.GenerateClass(output, classDef);

      actual.Should().Be(expected);
    }
  }
}
