﻿namespace SixtenLabs.Spawn.Tests
{
	public class TestGeneratorSettings : GeneratorSettings
	{
		public TestGeneratorSettings()
			: base("SolutionName", "RootNamespace", "FileName", "uri", "1.0.0.0", XmlLoadMethod.FromFile)
		{
		}
	}
}
