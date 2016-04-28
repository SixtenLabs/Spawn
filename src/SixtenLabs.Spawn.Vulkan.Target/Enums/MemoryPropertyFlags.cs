﻿using System;

namespace SixtenLabs.Spawn.Vulkan.Target
{
    [Flags]
    public enum MemoryPropertyFlags
    {
        /// <summary>
                /// If otherwise stated, then allocate memory on device
                /// </summary>
        MemoryPropertyDeviceLocalBit = 0,
        /// <summary>
                /// Memory is mappable by host
                /// </summary>
        MemoryPropertyHostVisibleBit = 1,
        /// <summary>
                /// Memory will have i/o coherency. If not set, application may need to use vkFlushMappedMemoryRanges and vkInvalidateMappedMemoryRanges to flush/invalidate host cache
                /// </summary>
        MemoryPropertyHostCoherentBit = 2,
        /// <summary>
                /// Memory will be cached by the host
                /// </summary>
        MemoryPropertyHostCachedBit = 3,
        /// <summary>
                /// Memory may be allocated by the driver when it is required
                /// </summary>
        MemoryPropertyLazilyAllocatedBit = 4
    }
}