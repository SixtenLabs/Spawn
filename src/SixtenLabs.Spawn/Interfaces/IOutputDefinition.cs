using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn
{
	public interface IOutputDefinition
	{
		string TargetSolution { get; set; }

		string FileName { get; set; }

    string Extension { get; set; }

		string OutputDirectory { get; set; }
	}
}
