using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class BowlingController : MonoBehaviour
{
    public GameObject bowlingSetupPrefab;

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
        //var ctrl = leftController.GetComponent<ActionBasedController>();
        //ctrl.positionAction.action.performed += ctx => debug.text = "Left Pos: " + ctx.ReadValue<Vector3>();
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
        // Orient it correctly
        bowlingSetup.transform.forward = forward;
    }

    public void Restart() {
        // Restart just despawns
        if (bowlingSetup != null) {
            Destroy(bowlingSetup);
        }
    }

    public void SpawnAR() {
        // TODO: Put the raycasting/plane stuff here.

        var ctrl = leftController.GetComponent<XRRayInteractor>();
        //ctrl.positionAction.action.performed += ctx => debug.text = "Left Pos: " + ctx.ReadValue<Vector3>();
        ctrl.enableUIInteraction = true;
        //ctrl.raycastMask = (layermask)
        Transform rayTransf = ctrl.rayOriginTransform;

        Ray ray = new Ray(rayTransf.position, rayTransf.forward);
        RaycastHit hit; 

        //random value rn
        LayerMask interactableLayermask = 6;

        if(Physics.Raycast(ray, out hit, 10f, interactableLayermask)) {
            Vector3 position = hit.point;
            Vector3 direction = rayTransf.forward;
            direction.y = 0;
            Vector3.Normalize(direction);
            position = position += direction*5f;

            Spawn(position, direction);
        }
        //Spawn(new Vector3(0,0,0), new Vector3(0,0,1f));
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
