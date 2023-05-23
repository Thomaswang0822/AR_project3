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
method being deprecated. *However*, the Snapdragon Spaces SDK installs version 4 by default, 

# Useful Docs

- [QCHT Guide](https://docs.spaces.qualcomm.com/unity/handtracking/HandTrackingOverview.html)
- [XR Device Simulator](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.0/manual/xr-device-simulator.html)
- [Bowling Lane Dimension](https://www.dimensions.com/element/bowling-lane), we are using 2:1 of this.


# Footnotes

[^1] https://docs.spaces.qualcomm.com/unity/handtracking/BasicSceneSetup.html
[^2] https://docs.spaces.qualcomm.com/unity/samples/SceneSetup.html
[^3] https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@5.0/manual/project-setup/scene-setup.html