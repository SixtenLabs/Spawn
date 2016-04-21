﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn
{
	public class UnionDefinition : TypeDefinition
	{
		public UnionDefinition()
		{
		}

		public void AddField(string specName, string name, string specType)
		{
			var field = new FieldDefinition() { SpecName = specName, TranslatedName = name, SpecType = specType };

			Fields.Add(field);
		}

		public IList<FieldDefinition> Fields { get; } = new List<FieldDefinition>();

		public bool ReturnedOnly { get; set; }

		public List<string> Validity { get; } = new List<string>();
	}
}