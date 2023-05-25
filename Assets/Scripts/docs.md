# How does everything work?

For this project, we're using a few different libraries. We're using the Snapdragon SPACES SDK, and we're also using
libraries built into Unity, in particular, AR Foundation.

One of the confusing things about this is that depending on what starting guide you look at (i.e. either the QCHT
starting guide [^1] or the general starting guide [^2], it'll give you different starting instructions.
Why is this?

Basically, there are two ways to set up a scene. You can either use an AR Session combined with an AR Session Origin
(as specified in the overall starting guide), or you can use an AR Session combined with an XR Camera set up for AR
(you still need both; this is mentioned here [^3]).

In AR Foundation version 5 and up, the XR Camera setup has become the preferred method, with the AR Session Origin
method being deprecated [^4]. *However*, the Snapdragon Spaces SDK installs version 4 by default, which requires you to
use the AR Session Origin to use AR.

Thus, there are two options here.

- Use the default version of AR Foundation installed by the Snapdragon SPACES SDK (4.2.7), and use the AR Session and
  the AR Session Origin.
- Manually upgrade the version of AR Foundation used, as described here [^5], and use the AR Session with the XR Camera.

For more info, check the manuals for AR Foundation 4.2 (the current version) [^6] and 5.0 (the new version) [^7].
These also have info on how to use different AR features (e.g. plane tracking, which we need for doing bowling ball
spawning).

# Hand-tracking and input handling

To do hand-tracking, we use QCHT 4.0, which in turns uses the XR Interaction Toolkit.
The basic scene setup tutorial [^8] describes roughly how QCHT works. QCHT's novel contribution is that it adds
the XR Hand Tracking Manager along with the XRControllerPointer{Left/Right} prefabs.

The meat of the configuration for hand-tracking is in the XR Controller component for each of these prefab items.
This component maps hand tracking data (i.e. position and rotation) and hand gestures (i.e. selection via pinch,
etc.) to *actions*. These actions are part of Unity's input system, and they're how we can get controller position/rotation
data and do on-pinch callbacks. The broader reason for this input/action system is to be able to do this in a
controller-generic way and map controller inputs to general actions (e.g. move, click, ...). More info about actions
(and Unity's input system) is available in the Unity Input System documentation [^10].

## Raycasting etc.

For doing controller ray casting for e.g. choosing where to spawn the bowling thing, we can use the XR Ray Interactor,
which can do ray casts and see what objects it intersects with.

There are two possibilities:

- Use the XR Ray Interactor's built in raycasting intersection stuff [^9]. The AR Plane Manager adds detected planes
  as ARPlane objects in the hierarchy, under XR Origin/Trackables.
- Just get the ray from the XR Ray Interactor (the `effectiveRayOrigin` field of this component, which is a transform
  containing both position/origin and direction/forward), then use the AR Raycast Manager to do a raycast. The manager
  takes care of limiting raycast checks to just trackables (point cloud or, in our case, planes) [^11].

# Useful Docs

- [QCHT Guide](https://docs.spaces.qualcomm.com/unity/handtracking/HandTrackingOverview.html)
- [XR Device Simulator](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.0/manual/xr-device-simulator.html)
- [Bowling Lane Dimension](https://www.dimensions.com/element/bowling-lane), we are using 2:1 of this.
- [Detect Gestures](https://www.youtube.com/watch?v=3IEilGl-mm0)


# Footnotes

[^1] https://docs.spaces.qualcomm.com/unity/handtracking/BasicSceneSetup.html

[^2] https://docs.spaces.qualcomm.com/unity/samples/SceneSetup.html

[^3] https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@5.0/manual/project-setup/scene-setup.html

[^4] https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@5.0/manual/version-history/migration-guide-5-x.html

[^5] https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@5.0/manual/project-setup/edit-your-project-manifest.html

[^6] https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@4.2/manual/index.html

[^7] https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@5.0/manual/index.html

[^8] https://docs.spaces.qualcomm.com/unity/handtracking/BasicSceneSetup.html

[^9] https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.0/api/UnityEngine.XR.Interaction.Toolkit.XRRayInteractor.html#UnityEngine_XR_Interaction_Toolkit_XRRayInteractor_TryGetCurrent3DRaycastHit_UnityEngine_RaycastHit__

[^10] https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Actions.html

[^11] https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@4.0/manual/raycast-manager.html