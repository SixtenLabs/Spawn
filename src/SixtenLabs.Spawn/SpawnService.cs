using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SixtenLabs.Spawn
{
	public class SpawnService : ISpawnService
	{
		public SpawnService()
		{
		}

		private void OpenWorkspace()
		{
			var workspace = MSBuildWorkspace.Create();
			var solution = workspace.OpenSolutionAsync(SolutionPath).Result;
			Workspace = workspace;
		}

		private Project GetProject(string projectName)
		{
			var newSolution = Workspace.CurrentSolution;
			var project = newSolution.Projects.Where(x => x.Name == projectName).FirstOrDefault();
			
			return project;
		}

		private void ApplyChanges(Solution solution)
		{
			var result = Workspace.TryApplyChanges(solution);

			if(!result)
			{
				throw new InvalidOperationException("Could not apply changes to workspace...why not?");
			}
		}

		/// <summary>
		/// Before generating any code, intialize the workspace with the solution file.
		/// </summary>
		/// <param name="solutionPath"></param>
		public void Initialize(string solutionPath)
		{
			SolutionPath = solutionPath;
			OpenWorkspace();
		}

		/// <summary>
		/// Create a file in the target project with generated code from the passed in contents
		/// </summary>
		/// <param name="targetProject"></param>
		/// <param name="newFileName"></param>
		/// <param name="contents"></param>
		public void AddDocumentToProject(string targetProject, string newFileName, string extension, string contents, IEnumerable<string> folders = null, string filePath = null)
		{
			var project = GetProject(targetProject);

			var documentName = $"{newFileName}.{extension}";
			var document = project.Documents.Where(x => x.Name == documentName && x.FilePath.Contains(folders.First())).FirstOrDefault();

			Document newDocument = null;

			if (document != null)
			{
				newDocument = document.WithText(SourceText.From(contents));
			}
			else
			{
				newDocument = project.AddDocument(newFileName, contents, folders, filePath);
			}

			ApplyChanges(newDocument.Project.Solution);
    }

		public Workspace Workspace { get; private set; }

		private string SolutionPath { get; set; }
	}
}
