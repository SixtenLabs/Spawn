﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtenLabs.Spawn.CSharp
{
  public interface IHaveArguments
  {
    IList<ArgumentDefinition> ArgumentDefinitions { get; }
  }
}
