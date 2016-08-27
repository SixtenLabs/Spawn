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

    private static SyntaxToken CreateSyntaxTokenDocumentationCommentExterior(this CommentDefinition comment)
    {
      var commentToken = SF.XmlTextLiteral($"/// {comment.Comment}");

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

    private static SyntaxTokenList GetCommentTokens(this CommentCollection comments)
    {
      List<SyntaxToken> syntaxTokens = new List<SyntaxToken>();

      int count = 0;

      foreach (var comment in comments)
      {
        syntaxTokens.Add(CreateSyntaxTokenXmlNewLine());
        syntaxTokens.Add(comment.CreateSyntaxTokenDocumentationCommentExterior());

        count++;

        if(count == comments.Count)
        {
          syntaxTokens.Add(CreateSyntaxTokenXmlNewLine());
        }
      }

      syntaxTokens.Add(new CommentDefinition().CreateSyntaxTokenDocumentationCommentExterior());

      return SF.TokenList(syntaxTokens);
    }

    /// <summary>
    /// Comments are generating but all lines after the first are indented 8 spaces.
    /// </summary>
    /// <param name="comments"></param>
    /// <returns></returns>
    public static SyntaxTriviaList GetCommentTriviaSyntax(this CommentCollection commentDefinitions)
    {
      SyntaxTriviaList triviaList = SF.TriviaList();

      if (commentDefinitions.IsNotEmpty)
      {

        var commentTrivia = SF.DocumentationCommentTrivia(SyntaxKind.SingleLineDocumentationCommentTrivia,
              SF.List(new XmlNodeSyntax[]
              {
                SF.XmlText()
                  .WithTextTokens(SF.TokenList(new CommentDefinition().CreateSyntaxTokenDocumentationCommentExterior())),
                SF.XmlElement(
                  CreateXmlElementStartTag("summary"),
                  CreateXmlElementEndTag("summary"))
                  .WithContent(SF.SingletonList<XmlNodeSyntax>(SF.XmlText()
                  .WithTextTokens(commentDefinitions.GetCommentTokens()))),
                SF.XmlText()
                  .WithTextTokens(SF.TokenList(CreateSyntaxTokenXmlNewLine()))
              }));

        triviaList = SF.TriviaList(SF.Trivia(commentTrivia));
      }

      return triviaList;
    }

    public static DocumentationCommentTriviaSyntax DocumentationCommentSyntax(this DocumentationCommentDefinition definition)
    {


      var syntax = SF.DocumentationComment();

      return syntax;
    }
  }
}
