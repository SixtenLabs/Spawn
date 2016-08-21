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
    /// If the file already exists it will remove the old one and add the new version.
    /// </summary>
    /// <param name="targetProject"></param>
    /// <param name="newFileName"></param>
    /// <param name="contents"></param>
    void AddDocumentToProject(string targetProject, string newFileName, string extension, string contents, IEnumerable<string> folders = null, string filePath = null);

    /// <summary>
    /// This will remove all files in a projects folder. Generated or not.
    /// Be wary.
    /// </summary>
    /// <param name="targetProject"></param>
    /// <param name="folder"></param>
    void FlushFolderOfDocuments(string targetProject, string folder);

    Workspace Workspace { get; }

	}
}
