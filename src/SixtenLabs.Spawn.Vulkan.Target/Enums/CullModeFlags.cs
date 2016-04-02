﻿using System;

namespace SixtenLabs.Spawn.Vulkan.Target
{
  [Flags]
  public enum CullModeFlags
  {
    CULL_MODE_NONE = 0,

    CULL_MODE_FRONT_BIT = 0,

    CULL_MODE_BACK_BIT = 1,

    CULL_MODE_FRONT_AND_BACK = 0x00000003
  }
}