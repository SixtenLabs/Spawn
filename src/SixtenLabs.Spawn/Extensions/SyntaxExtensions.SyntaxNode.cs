using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.Formatting;

namespace SixtenLabs.Spawn
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
	}
}
