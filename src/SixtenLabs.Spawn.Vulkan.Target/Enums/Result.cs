﻿namespace SixtenLabs.Spawn.Vulkan.Target
{
    public enum Result
    {
        /// <summary>
                /// Command completed successfully
                /// </summary>
        SUCCESS = 0,
        /// <summary>
                /// A fence or query has not yet completed
                /// </summary>
        NotReady = 1,
        /// <summary>
                /// A wait operation has not completed in the specified time
                /// </summary>
        TIMEOUT = 2,
        /// <summary>
                /// An event is signaled
                /// </summary>
        EventSet = 3,
        /// <summary>
                /// An event is unsignalled
                /// </summary>
        EventReset = 4,
        /// <summary>
                /// A return array was too small for the resul
                /// </summary>
        INCOMPLETE = 5,
        /// <summary>
                /// A host memory allocation has failed
                /// </summary>
        ErrorOutOfHostMemory = -1,
        /// <summary>
                /// A device memory allocation has failed
                /// </summary>
        ErrorOutOfDeviceMemory = -2,
        /// <summary>
                /// The logical device has been lost. See <<devsandqueues-lost-device>>
                /// </summary>
        ErrorInitializationFailed = -3,
        /// <summary>
                /// Initialization of a object has failed
                /// </summary>
        ErrorDeviceLost = -4,
        /// <summary>
                /// Mapping of a memory object has failed
                /// </summary>
        ErrorMemoryMapFailed = -5,
        /// <summary>
                /// Layer specified does not exist
                /// </summary>
        ErrorLayerNotPresent = -6,
        /// <summary>
                /// Extension specified does not exist
                /// </summary>
        ErrorExtensionNotPresent = -7,
        /// <summary>
                /// Requested feature is not available on this device
                /// </summary>
        ErrorFeatureNotPresent = -8,
        /// <summary>
                /// Unable to find a Vulkan driver
                /// </summary>
        ErrorIncompatibleDriver = -9,
        /// <summary>
                /// Too many objects of the type have already been created
                /// </summary>
        ErrorTooManyObjects = -10,
        /// <summary>
                /// Requested format is not supported on this device
                /// </summary>
        ErrorFormatNotSupported = -11
    }
}