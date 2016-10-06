﻿namespace SixtenLabs.Spawn.Tests
{
	public class TestSpec : SpawnSpec<TestRegistry>
	{
		public TestSpec(XmlFileLoader xmlFileLoader, ISpecMapper<TestRegistry> specMapper, IDefinitionDictionary definitionDictionary)
			: base(xmlFileLoader, specMapper, definitionDictionary)
		{
		}
	}
}
