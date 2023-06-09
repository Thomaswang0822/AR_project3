using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMouse : MonoBehaviour {

    [SerializeField]
    public float sensitivity = 3.0f;
    [SerializeField]
    public float smoothing = 2.0f;
    // get the incremental value of mouse moving
    private Vector2 mouseLook;
    // smooth the mouse moving
    private Vector2 smoothV;

    private bool pause = false;

    // Update is called once per frame
    void Update() {
        // md is mosue delta
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        // the interpolated float result between the two float values
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        smoothV.y = Mathf.Clamp(smoothV.y, -88f, 88f);

        // incrementally add to the camera look
        mouseLook += smoothV;

        // vector3.right means the x-axis
        if (!pause) {
            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right) * Quaternion.AngleAxis(mouseLook.x, Vector3.up);
        }

        if (Input.GetKeyDown("escape")) {
            // turn on the cursor
            pause = !pause;
        }
    }
}
