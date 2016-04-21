﻿using System;

namespace SixtenLabs.Spawn.Vulkan.Target
{
  /// <summary>
  /// *** Do Not Edit ***
  /// This file was generated by the Spawn Code Generator.
  /// https://github.com/SixtenLabs/Spawn
  /// 
  /// Generated from the vk.xml registry file from Khronos Group.
  /// https://raw.githubusercontent.com/KhronosGroup/Vulkan-Docs/1.0/src/spec/vk.xml
  /// 
  /// </summary>
  [Flags]
  public enum CommandPoolCreateFlags
  {
    /// <summary> 
    /// Command buffers have a short lifetime
    /// </summary> 
    CommandPoolCreateTransientBit = 0,

    /// <summary> 
    /// Command buffers may release their memory individually
    /// </summary> 
    CommandPoolCreateResetCommandBufferBit = 1
  }
}