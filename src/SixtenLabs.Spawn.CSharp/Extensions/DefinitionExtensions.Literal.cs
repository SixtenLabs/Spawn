using Microsoft.CodeAnalysis.CSharp;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;

namespace SixtenLabs.Spawn.CSharp.Extensions
{
  /// <summary>
  /// Literal Definition Extensions
  /// </summary>
  public static partial class DefinitionExtensions
  {
    public static EqualsValueClauseSyntax GetInitializer(this LiteralDefinition literalDefinition)
    {
      EqualsValueClauseSyntax equalsSyntax = null;

      if (literalDefinition != null)
      {
        if (literalDefinition.Kind == SyntaxKindDto.ObjectCreationExpression)
        {
          var objectCreationSyntax = literalDefinition.GetObjectCreationExpression();
          equalsSyntax = SF.EqualsValueClause(objectCreationSyntax);
        }
        else
        {
          var expression = GetLiteralExpression(literalDefinition);
          equalsSyntax = SF.EqualsValueClause(expression);
        }
      }

      return equalsSyntax;
    }

    public static ExpressionSyntax GetObjectCreationExpression(this LiteralDefinition literalDefintion)
    {
      var typeSyntax = SF.IdentifierName(literalDefintion.TranslatedName);

      var objectExpressionDeclaration = SF.ObjectCreationExpression(typeSyntax);

      if(literalDefintion.Arguments.Count > 0)
      {
        var argumentList = literalDefintion.Arguments.GetArgumentList();

        objectExpressionDeclaration = objectExpressionDeclaration.WithArgumentList(argumentList);
      }

      return objectExpressionDeclaration;
    }

    public static ExpressionSyntax GetLiteralExpression(this LiteralDefinition literalDefinition)
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

      return expression;
    }
  }
}
