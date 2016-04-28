﻿using System;

namespace SixtenLabs.Spawn.Vulkan.Target
{
    [Flags]
    public enum AccessFlags
    {
        /// <summary>
                /// Controls coherency of indirect command reads
                /// </summary>
        AccessIndirectCommandReadBit = 0,
        /// <summary>
                /// Controls coherency of index reads
                /// </summary>
        AccessIndexReadBit = 1,
        /// <summary>
                /// Controls coherency of vertex attribute reads
                /// </summary>
        AccessVertexAttributeReadBit = 2,
        /// <summary>
                /// Controls coherency of uniform buffer reads
                /// </summary>
        AccessUniformReadBit = 3,
        /// <summary>
                /// Controls coherency of input attachment reads
                /// </summary>
        AccessInputAttachmentReadBit = 4,
        /// <summary>
                /// Controls coherency of shader reads
                /// </summary>
        AccessShaderReadBit = 5,
        /// <summary>
                /// Controls coherency of shader writes
                /// </summary>
        AccessShaderWriteBit = 6,
        /// <summary>
                /// Controls coherency of color attachment reads
                /// </summary>
        AccessColorAttachmentReadBit = 7,
        /// <summary>
                /// Controls coherency of color attachment writes
                /// </summary>
        AccessColorAttachmentWriteBit = 8,
        /// <summary>
                /// Controls coherency of depth/stencil attachment reads
                /// </summary>
        AccessDepthStencilAttachmentReadBit = 9,
        /// <summary>
                /// Controls coherency of depth/stencil attachment writes
                /// </summary>
        AccessDepthStencilAttachmentWriteBit = 10,
        /// <summary>
                /// Controls coherency of transfer reads
                /// </summary>
        AccessTransferReadBit = 11,
        /// <summary>
                /// Controls coherency of transfer writes
                /// </summary>
        AccessTransferWriteBit = 12,
        /// <summary>
                /// Controls coherency of host reads
                /// </summary>
        AccessHostReadBit = 13,
        /// <summary>
                /// Controls coherency of host writes
                /// </summary>
        AccessHostWriteBit = 14,
        /// <summary>
                /// Controls coherency of memory reads
                /// </summary>
        AccessMemoryReadBit = 15,
        /// <summary>
                /// Controls coherency of memory writes
                /// </summary>
        AccessMemoryWriteBit = 16
    }
}