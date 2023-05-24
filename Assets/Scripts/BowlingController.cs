using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingController : MonoBehaviour
{
    public GameObject bowlingSetupPrefab;

    // The instance of the bowling setup that we spawn into the world.
    GameObject bowlingSetup;
    GameObject leftGuard;
    GameObject rightGuard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

    public void ToggleGuards() {
        if (leftGuard == null || rightGuard == null) {
            leftGuard = GameObject.Find("GuardRail_L");
            rightGuard = GameObject.Find("GuardRail_R");
        }

        leftGuard.SetActive(!leftGuard.activeInHierarchy);
        rightGuard.SetActive(!rightGuard.activeInHierarchy);
    }
}
