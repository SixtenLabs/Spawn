using Microsoft.CodeAnalysis.CSharp.Syntax;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SixtenLabs.Spawn.CSharp.Extensions
{
  public static partial class DefinitionExtensions
  {
    public static ClassDeclarationSyntax CreateClassDeclaration(this ClassDefinition classDefinition)
    {
      var memberList = SF.List<MemberDeclarationSyntax>();

      var fields = classDefinition.Fields.GetFieldDeclarations();
      var methods = classDefinition.Methods.GetMethodDeclarations();
      var properties = classDefinition.Properties.GetPropertyDeclarations();

      memberList = memberList.AddRange(fields);
      memberList = memberList.AddRange(methods);
      memberList = memberList.AddRange(properties);

      var modifierTokens = GetModifierTokens(classDefinition.ModifierDefinitions);

      var classDeclaration = SF.ClassDeclaration(classDefinition.TranslatedName)
        .WithModifiers(modifierTokens)
        .WithMembers(memberList);

      return classDeclaration;
    }
  }
}
