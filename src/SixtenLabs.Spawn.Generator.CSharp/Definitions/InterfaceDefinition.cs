﻿using System.Collections.Generic;

namespace SixtenLabs.Spawn.Generator.CSharp
{
	/// <summary>
	/// An interface contains only the signatures of methods, properties, events or indexers. 
	/// A class or struct that implements the interface must implement the members of the interface that are specified in the interface definition. 
	/// </summary>
	public class InterfaceDefinition : TypeDefinition
	{
		public InterfaceDefinition()
    {
		}

		public List<FieldDefinition> FieldDefinitions { get; } = new List<FieldDefinition>();
	}
}
