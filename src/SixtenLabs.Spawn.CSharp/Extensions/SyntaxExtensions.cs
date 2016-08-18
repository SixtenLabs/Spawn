using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.Formatting;
using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SixtenLabs.Spawn.CSharp.Extensions
{
	public static partial class SyntaxExtensions
	{
    public static string GetFormattedCode(this SyntaxNode code)
    {
      var cw = new AdhocWorkspace();
      cw.Options.WithChangedOption(CSharpFormattingOptions.IndentBraces, true);
      var formattedCode = Formatter.Format(code, cw);

      return formattedCode.ToFullString();
    }

    public static SyntaxTriviaList LineFeed()
    {
      return SyntaxTriviaList.Create(SF.ElasticCarriageReturnLineFeed);
    }
  }
}
