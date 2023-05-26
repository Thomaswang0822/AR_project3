using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class BowlingController : MonoBehaviour
{
    public GameObject bowlingSetupPrefab;
    public Camera mainCamera;

    public GameObject leftController;
    public GameObject rightController;
    public TextMeshProUGUI debug;

    // The instance of the bowling setup that we spawn into the world.
    GameObject bowlingSetup;
    Rigidbody bbrb; // bowling ball rigid body
    GameObject leftGuard;
    GameObject rightGuard;

    // Start is called before the first frame update
    void Start()
    {
        // var ctrl = leftController.GetComponent<ActionBasedController>();
        // ctrl.positionAction.action.performed += ctx => {

        //     var ctrl = leftController.GetComponent<XRRayInteractor>();
        //     // ctrl.enableUIInteraction = true;

        //     RaycastHit hit; 
        //     if (ctrl.TryGetCurrent3DRaycastHit(out hit)) {
        //         var obj = hit.collider.gameObject;
        //         debug.text = "hit! " + obj.name;
        //     }
        // };
    }

    // Update is called once per frame
    void Update()
    {
        // Debug inputs
        if (Input.GetKeyDown("e")) {
            Spawn(new Vector3(0.0f, 0.0f, 1.5f), new Vector3(0.0f, 0.0f, -1.0f));
        } else if (Input.GetKeyDown("r")) {
            Restart();
        } else if (Input.GetKeyDown("t")) {
            ToggleGuards();
        }
    }


    // Spawns the bowling setup into the world at a given location and facing a particular direction.
    public void Spawn(Vector3 pos, Vector3 forward) {
        // First, spawn everything into the world!
        if (bowlingSetup != null) {
            Destroy(bowlingSetup);
        }

        bowlingSetup = Instantiate(bowlingSetupPrefab, pos, Quaternion.identity);
        GameObject station = GameObject.Find("Station").gameObject;
        station.GetComponent<Canvas>().worldCamera = mainCamera;
        // Orient it correctly
        bowlingSetup.transform.forward = forward;

        GameObject numpad = GameObject.Find("Numpad").gameObject;
        numpad.GetComponent<Numpad>().UpdateDisplay();
    }

    public void Restart() {
        // Respawn in the same position
        if (bowlingSetup != null) {
            var xf = bowlingSetup.transform;
            Spawn(xf.position, xf.forward);
        } else {
            SpawnAR();
        }
    }

    public void SpawnAR() {
        var ctrl = leftController.GetComponent<XRRayInteractor>();
        // ctrl.enableUIInteraction = true;

        RaycastHit hit; 
        if (ctrl.TryGetCurrent3DRaycastHit(out hit)) {
            GameObject obj = hit.collider.gameObject;
            ARPlane plane = obj.GetComponent<ARPlane>();
            if (plane != null) {
                Vector3 position = hit.point;
                Vector3 direction = ctrl.rayOriginTransform.forward;
                Vector3 normal = plane.normal;

                // To get forward direction, we project the ray direction onto the plane.
                // https://math.stackexchange.com/questions/633181/formula-to-project-a-vector-onto-a-plane
                Vector3 forward = direction - Vector3.Dot(direction, normal) * normal;
                forward = Vector3.Normalize(forward);

                position += forward * 3f;

                Spawn(position, -forward);
            }
        }
    }

    public void ToggleGuards() {
        if (leftGuard == null || rightGuard == null) {
            leftGuard = GameObject.Find("GuardRail_L");
            rightGuard = GameObject.Find("GuardRail_R");
        }

        if (leftGuard != null && rightGuard != null) {
            leftGuard.SetActive(!leftGuard.activeInHierarchy);
            rightGuard.SetActive(!rightGuard.activeInHierarchy);
        }
    }

    public void ChangeBallMass(float val) {
        if (bbrb == null) {
            GameObject ball = GameObject.Find("BowlingBall").gameObject;
            if (ball != null) {
                bbrb = ball.GetComponent<Rigidbody>();
            }
        }

        if (bbrb != null) {
            bbrb.mass = val;
        }
    }

    // TODO: Incorporate BowlBehavior into here?
}
