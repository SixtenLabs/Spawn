using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn
{
	public interface ISpawnService
	{
		/// <summary>
		/// Before generating any code, intialize the workspace with the solution file.
		/// </summary>
		/// <param name="solutionPath"></param>
		void Initialize(string solutionPath);

		/// <summary>
		/// Create a file in the target project with generated code from the passed in SyntaxNode
		/// </summary>
		/// <param name="targetProject"></param>
		/// <param name="newFileName"></param>
		/// <param name="contents"></param>
		void AddDocumentToProject(string targetProject, string newFileName, string extension, string contents, IEnumerable<string> folders = null, string filePath = null);

		Workspace Workspace { get; }

	}
}
