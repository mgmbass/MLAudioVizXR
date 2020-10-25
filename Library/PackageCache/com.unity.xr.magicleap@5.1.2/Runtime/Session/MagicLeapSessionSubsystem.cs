using UnityEngine.Scripting;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.MagicLeap.Internal;

namespace UnityEngine.XR.MagicLeap
{
    /// <summary>
    /// The Magic Leap implementation of the <c>XRSessionSubsystem</c>. Do not create this directly.
    /// Use <c>MagicLeapSessionSubsystemDescriptor.Create()</c> instead.
    /// </summary>
    [Preserve]
    public sealed class MagicLeapSessionSubsystem : XRSessionSubsystem
    {
        protected override Provider CreateProvider() => new MagicLeapProvider();

        class MagicLeapProvider : Provider
        {
            PerceptionHandle m_PerceptionHandle;

            public MagicLeapProvider()
            {
                m_PerceptionHandle = PerceptionHandle.Acquire();
            }

            public override Promise<SessionAvailability> GetAvailabilityAsync()
            {
                var availability =
#if PLATFORM_LUMIN
                SessionAvailability.Installed | SessionAvailability.Supported;
#else
                SessionAvailability.None;
#endif
                return Promise<SessionAvailability>.CreateResolvedPromise(availability);
            }

            public override TrackingState trackingState
            {
                get
                {
                    var device = InputDevices.GetDeviceAtXRNode(XRNode.CenterEye);
                    if (device.TryGetFeatureValue(CommonUsages.trackingState, out InputTrackingState inputTrackingState))
                    {
                        if (inputTrackingState == InputTrackingState.None)
                            return TrackingState.None;
                        else if (inputTrackingState == (InputTrackingState.Position | InputTrackingState.Rotation))
                            return TrackingState.Tracking;
                        else
                            return TrackingState.Limited;
                    }
                    else
                    {
                        return TrackingState.None;
                    }
                }
            }

            public override void Destroy()
            {
                m_PerceptionHandle.Dispose();
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void RegisterDescriptor()
        {
#if PLATFORM_LUMIN
            XRSessionSubsystemDescriptor.RegisterDescriptor(new XRSessionSubsystemDescriptor.Cinfo
            {
                id = "MagicLeap-Session",
                subsystemImplementationType = typeof(MagicLeapSessionSubsystem),
                supportsInstall = false
            });
#endif
        }
    }
}
