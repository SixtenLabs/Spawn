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
  public enum QueryPipelineStatisticFlags
  {
    /// <summary> 
    /// Optional
    /// </summary> 
    QUERY_PIPELINE_STATISTIC_INPUT_ASSEMBLY_VERTICES_BIT = 0,

    /// <summary> 
    /// Optional
    /// </summary> 
    QUERY_PIPELINE_STATISTIC_INPUT_ASSEMBLY_PRIMITIVES_BIT = 1,

    /// <summary> 
    /// Optional
    /// </summary> 
    QUERY_PIPELINE_STATISTIC_VERTEX_SHADER_INVOCATIONS_BIT = 2,

    /// <summary> 
    /// Optional
    /// </summary> 
    QUERY_PIPELINE_STATISTIC_GEOMETRY_SHADER_INVOCATIONS_BIT = 3,

    /// <summary> 
    /// Optional
    /// </summary> 
    QUERY_PIPELINE_STATISTIC_GEOMETRY_SHADER_PRIMITIVES_BIT = 4,

    /// <summary> 
    /// Optional
    /// </summary> 
    QUERY_PIPELINE_STATISTIC_CLIPPING_INVOCATIONS_BIT = 5,

    /// <summary> 
    /// Optional
    /// </summary> 
    QUERY_PIPELINE_STATISTIC_CLIPPING_PRIMITIVES_BIT = 6,

    /// <summary> 
    /// Optional
    /// </summary> 
    QUERY_PIPELINE_STATISTIC_FRAGMENT_SHADER_INVOCATIONS_BIT = 7,

    /// <summary> 
    /// Optional
    /// </summary> 
    QUERY_PIPELINE_STATISTIC_TESSELLATION_CONTROL_SHADER_PATCHES_BIT = 8,

    /// <summary> 
    /// Optional
    /// </summary> 
    QUERY_PIPELINE_STATISTIC_TESSELLATION_EVALUATION_SHADER_INVOCATIONS_BIT = 9,

    /// <summary> 
    /// Optional
    /// </summary> 
    QUERY_PIPELINE_STATISTIC_COMPUTE_SHADER_INVOCATIONS_BIT = 10
  }
}