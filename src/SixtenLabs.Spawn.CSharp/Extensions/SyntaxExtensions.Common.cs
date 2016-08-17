using System;
using System.Collections.Generic;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.Formatting;

namespace SixtenLabs.Spawn.CSharp
{
	public static partial class SyntaxExtensions
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
		private static DocumentationCommentTriviaSyntax GetCommentTriviaSyntax(IList<string> comments)
		{
			var x = SF.DocumentationCommentTrivia(SyntaxKind.SingleLineDocumentationCommentTrivia,
							SF.List(new XmlNodeSyntax[]
							{
								SF.XmlText()
									.WithTextTokens(SF.TokenList(CreateSyntaxTokenDocumentationCommentExterior(""))),
								SF.XmlElement(
									CreateXmlElementStartTag("summary"), 
									CreateXmlElementEndTag("summary"))
									.WithContent(SF.SingletonList<XmlNodeSyntax>(SF.XmlText()
									.WithTextTokens(GetCommentTokens(comments)))),
								SF.XmlText()
									.WithTextTokens(SF.TokenList(CreateSyntaxTokenXmlNewLine()))
							}));

			return x;
		}

		/// <summary>
		/// Currently support statements defined completely by the user.
		/// 
		/// TODO : Explore support all the different statement syntaxes (a lot of work not sure it brings any value at this point).
		/// 
		/// </summary>
		/// <param name="definition"></param>
		/// <returns></returns>
		private static BlockSyntax CreateBlock(BlockDefinition definition)
		{
			var statementList = new List<StatementSyntax>();

			foreach (var statement in definition.Statements)
			{
				var declaration = SF.ParseStatement(statement.Code);
				statementList.Add(declaration);
			}

			return SF.Block(statementList);
		}

		private static ExpressionSyntax GetLiteralExpression(LiteralDefinition literalDefinition)
		{
			ExpressionSyntax expression = null;

			if (literalDefinition.LiteralType == typeof(string))
			{
				expression = SF.LiteralExpression(SyntaxKind.StringLiteralExpression, SF.Literal(Convert.ToString(literalDefinition.Value)));
			}
			else if (literalDefinition.LiteralType == typeof(bool))
			{
				bool value = Convert.ToBoolean(literalDefinition.Value);
				var valueToken = value ? SF.Token(SyntaxKind.TrueKeyword) : SF.Token(SyntaxKind.FalseKeyword);
				expression = SF.LiteralExpression(value ? SyntaxKind.TrueLiteralExpression : SyntaxKind.FalseLiteralExpression).WithToken(valueToken);
			}
			else if (literalDefinition.LiteralType == typeof(int))
			{
				int intValue;
				var isInt = int.TryParse(literalDefinition.Value, out intValue);

				if (isInt)
				{
					expression = SF.LiteralExpression(SyntaxKind.NumericLiteralExpression, SF.Literal(intValue));
				}
				else if (literalDefinition.Value.StartsWith("int."))
				{
					var tokenint = SF.Token(SyntaxKind.IntKeyword);
					var indentifier = SF.IdentifierName(literalDefinition.Value.Replace("int.", ""));
					expression = SF.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, SF.PredefinedType(tokenint), indentifier);
				}
			}
			else if (literalDefinition.LiteralType == typeof(double))
			{
				expression = SF.LiteralExpression(SyntaxKind.NumericLiteralExpression, SF.Literal(Convert.ToDouble(literalDefinition.Value)));
			}
			else if (literalDefinition.LiteralType == typeof(decimal))
			{
				expression = SF.LiteralExpression(SyntaxKind.NumericLiteralExpression, SF.Literal(Convert.ToDecimal(literalDefinition.Value)));
			}
			else if (literalDefinition.LiteralType == typeof(uint))
			{
				uint uintValue;
				var isUint = uint.TryParse(literalDefinition.Value, out uintValue);

				if (isUint)
				{
					expression = SF.LiteralExpression(SyntaxKind.NumericLiteralExpression, SF.Literal(uintValue));
				}
				else if (literalDefinition.Value.StartsWith("uint."))
				{
					var tokenUint = SF.Token(SyntaxKind.UIntKeyword);
					var indentifier = SF.IdentifierName(literalDefinition.Value.Replace("uint.", ""));
					expression = SF.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, SF.PredefinedType(tokenUint), indentifier);
				}
			}
			else if (literalDefinition.LiteralType == typeof(byte))
			{
				expression = SF.LiteralExpression(SyntaxKind.NumericLiteralExpression, SF.Literal(Convert.ToByte(literalDefinition.Value)));
			}
			else if (literalDefinition.LiteralType == typeof(sbyte))
			{
				expression = SF.LiteralExpression(SyntaxKind.NumericLiteralExpression, SF.Literal(Convert.ToSByte(literalDefinition.Value)));
			}
			else if (literalDefinition.LiteralType == typeof(float))
			{
				expression = SF.LiteralExpression(SyntaxKind.NumericLiteralExpression, SF.Literal(Convert.ToSingle(literalDefinition.Value)));
			}
			else if (literalDefinition.LiteralType == typeof(char))
			{
				expression = SF.LiteralExpression(SyntaxKind.StringLiteralExpression, SF.Literal(Convert.ToChar(literalDefinition.Value)));
			}
			else if (literalDefinition.LiteralType == typeof(long))
			{
				expression = SF.LiteralExpression(SyntaxKind.NumericLiteralExpression, SF.Literal(Convert.ToInt64(literalDefinition.Value)));
			}
			else if (literalDefinition.LiteralType == typeof(ulong))
			{
				ulong ulongValue;
				var isUlong = ulong.TryParse(literalDefinition.Value, out ulongValue);

				if (isUlong)
				{
					expression = SF.LiteralExpression(SyntaxKind.NumericLiteralExpression, SF.Literal(ulongValue));
				}
				else if (literalDefinition.Value.StartsWith("ulong."))
				{
					var tokenUlong = SF.Token(SyntaxKind.ULongKeyword);
					var indentifier = SF.IdentifierName(literalDefinition.Value.Replace("ulong.", ""));
					expression = SF.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, SF.PredefinedType(tokenUlong), indentifier);
				}
			}
			else if (literalDefinition.LiteralType == typeof(short))
			{
				expression = SF.LiteralExpression(SyntaxKind.NumericLiteralExpression, SF.Literal(Convert.ToInt16(literalDefinition.Value)));
			}
			else if (literalDefinition.LiteralType == typeof(ushort))
			{
				expression = SF.LiteralExpression(SyntaxKind.NumericLiteralExpression, SF.Literal(Convert.ToUInt16(literalDefinition.Value)));
			}
			else if(literalDefinition.LiteralType == null)
			{
				// I guess it is much more complex to do a new object initializer.
				expression = SF.LiteralExpression(SyntaxKind.TrueLiteralExpression, SF.Literal(literalDefinition.Value));
			}

			return expression;
		}

		private static UsingDirectiveSyntax StandardUsing(string dllName)
		{
			return SF.UsingDirective(SF.IdentifierName(dllName)).WithUsingKeyword(SF.Token(SyntaxKind.UsingKeyword)).WithSemicolonToken(SF.Token(SyntaxKind.SemicolonToken));
		}

		private static UsingDirectiveSyntax StaticUsing(string dllName)
		{
			return SF.UsingDirective(SF.IdentifierName(dllName)).WithUsingKeyword(SF.Token(SyntaxKind.UsingKeyword)).WithStaticKeyword(SF.Token(SyntaxKind.StaticKeyword)).WithSemicolonToken(SF.Token(SyntaxKind.SemicolonToken));
		}

		private static UsingDirectiveSyntax AliasUsing(string dllName, string alias)
		{
			var aliasNameEqualsSyntax = SF.NameEquals(SF.IdentifierName(alias));
			return SF.UsingDirective(SF.IdentifierName(dllName)).WithUsingKeyword(SF.Token(SyntaxKind.UsingKeyword)).WithAlias(aliasNameEqualsSyntax).WithSemicolonToken(SF.Token(SyntaxKind.SemicolonToken));
		}

		/// <summary>
		/// This will create a sinlge using statement syntax.
		/// 
		/// Example:
		///   using System; -> from System
		///   using System.Linq; -> from System.Linq
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		private static UsingDirectiveSyntax ComposeUsing(UsingDirectiveDefinition usingDefinition)
		{
			if (usingDefinition.IsStatic)
			{
				return StaticUsing(usingDefinition.TranslatedName);
			}
			else if (usingDefinition.UseAlias)
			{
				return AliasUsing(usingDefinition.TranslatedName, usingDefinition.Alias);
			}
			else
			{
				return StandardUsing(usingDefinition.TranslatedName);
			}
		}

		public static SyntaxTriviaList GetComments(this CommentDefinition commentDefinition)
		{
			SyntaxTriviaList triviaList = SF.TriviaList();

			if (commentDefinition.HasComments)
			{
				var triv1 = GetCommentTriviaSyntax(commentDefinition.CommentLines);
				triviaList = SF.TriviaList(SF.Trivia(triv1));
			}

			return triviaList;
		}

		/// <summary>
		/// Adds using statements to the syntax in the compliation unit (represents the final file)
		/// </summary>
		/// <param name="compilationUnit">The compliation unit to update</param>
		/// <param name="usings">a list of usings to create using statements with</param>
		/// <returns>the updated compilation unit</returns>
		public static CompilationUnitSyntax AddUsingDirectives(this CompilationUnitSyntax compilationUnit, IList<UsingDirectiveDefinition> usingDefinitions)
		{
			if (usingDefinitions != null && usingDefinitions.Count > 0)
			{
				var usingList = new List<UsingDirectiveSyntax>();

				foreach (var usingDefinition in usingDefinitions)
				{
					var directive = ComposeUsing(usingDefinition);

					if (directive != null)
					{
						usingList.Add(directive);
					}
				}

				return compilationUnit.WithUsings(SF.List(usingList));
			}
			else
			{
				return compilationUnit;
			}
		}

		private static SyntaxTokenList GetModifierTokens(IList<ModifierDefinition> modifierDefinitions)
		{
			var list = new List<SyntaxToken>();

			foreach (var modifier in modifierDefinitions)
			{
				list.Add(SF.Token((SyntaxKind)modifier.Modifier));
			}

			return SF.TokenList(list);
		}

		public static NamespaceDeclarationSyntax AddNamespace(NamespaceDefinition @namespace)
		{
			if(@namespace == null)
			{
				return null;
			}

			return SF.NamespaceDeclaration(SF.IdentifierName(@namespace.TranslatedName));
		}

		public static string GetFormattedCode(this SyntaxNode code)
		{
			var cw = new AdhocWorkspace();
			cw.Options.WithChangedOption(CSharpFormattingOptions.IndentBraces, true);
			var formattedCode = Formatter.Format(code, cw);

			return formattedCode.ToFullString();
		}
	}
}
