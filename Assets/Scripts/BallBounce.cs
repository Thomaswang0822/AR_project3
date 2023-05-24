using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    private Rigidbody rb;
    Vector3 vp;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        vp = rb.velocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "GuardRail_L" || collision.gameObject.name == "GuardRail_R") {
            rb.velocity = Vector3.Reflect(vp.normalized, collision.contacts[0].normal) * Mathf.Max(0f, vp.magnitude);
        }
    }
}
