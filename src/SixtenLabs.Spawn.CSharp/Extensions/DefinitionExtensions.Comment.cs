using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SixtenLabs.Spawn.CSharp.Extensions
{
  /// <summary>
  /// CommentDefintion Extensions
  /// </summary>
  public static partial class DefinitionExtensions
  {
    private static SyntaxToken CreateSyntaxTokenXmlNewLine()
    {
      var newLineToken = SF.XmlTextNewLine(SF.TriviaList(), Environment.NewLine, Environment.NewLine, SF.TriviaList());

      return newLineToken;
    }

    private static SyntaxToken CreateSyntaxTokenDocumentationCommentExterior(string comment)
    {
      var commentToken = SF.XmlTextLiteral(SF.TriviaList(SF.DocumentationCommentExterior("///")), $" {comment}", $" {comment}", SF.TriviaList());

      return commentToken;
    }

    private static XmlElementStartTagSyntax CreateXmlElementStartTag(string name)
    {
      var startTag = SF.XmlElementStartTag(SF.XmlName(SF.Identifier(name)));

      return startTag;
    }

    private static XmlElementEndTagSyntax CreateXmlElementEndTag(string name)
    {
      var endTag = SF.XmlElementEndTag(SF.XmlName(SF.Identifier(name)));

      return endTag;
    }

    private static SyntaxTokenList GetCommentTokens(IList<string> comments)
    {
      List<SyntaxToken> syntaxTokens = new List<SyntaxToken>();

      foreach (var comment in comments)
      {
        syntaxTokens.Add(CreateSyntaxTokenXmlNewLine());
        syntaxTokens.Add(CreateSyntaxTokenDocumentationCommentExterior(comment));
        syntaxTokens.Add(CreateSyntaxTokenXmlNewLine());
      }

      syntaxTokens.Add(CreateSyntaxTokenDocumentationCommentExterior(""));

      return SF.TokenList(syntaxTokens);
    }

    /// <summary>
    /// Comments are generating but all lines after the first are indented 8 spaces.
    /// </summary>
    /// <param name="comments"></param>
    /// <returns></returns>
    public static SyntaxTriviaList GetCommentTriviaSyntax(this CommentDefinition commentDefinition)
    {
      SyntaxTriviaList triviaList = SF.TriviaList();

      if (commentDefinition.HasComments)
      {

        var commentTrivia = SF.DocumentationCommentTrivia(SyntaxKind.SingleLineDocumentationCommentTrivia,
              SF.List(new XmlNodeSyntax[]
              {
                SF.XmlText()
                  .WithTextTokens(SF.TokenList(CreateSyntaxTokenDocumentationCommentExterior(""))),
                SF.XmlElement(
                  CreateXmlElementStartTag("summary"),
                  CreateXmlElementEndTag("summary"))
                  .WithContent(SF.SingletonList<XmlNodeSyntax>(SF.XmlText()
                  .WithTextTokens(GetCommentTokens(commentDefinition.CommentLines)))),
                SF.XmlText()
                  .WithTextTokens(SF.TokenList(CreateSyntaxTokenXmlNewLine()))
              }));

        triviaList = SF.TriviaList(SF.Trivia(commentTrivia));
      }

      return triviaList;
    }
  }
}
