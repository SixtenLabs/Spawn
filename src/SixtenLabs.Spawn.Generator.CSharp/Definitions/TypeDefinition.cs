﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.Generator.CSharp
{
	public abstract class TypeDefinition : BaseTypeDefinition
	{
		protected TypeDefinition()
		{
		}

		public CommentDefinition Comments { get; set; }
	}
}
