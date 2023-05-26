using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Assume this is attached to the bowling ball
public class BowlBehavior : MonoBehaviour
{
    Rigidbody rb;

    void Awake() {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePosition;
    }

    public void OnBowl() {
        float angle = GameObject.Find("Station/X").gameObject.GetComponent<Slider>().value;
        float speed = GameObject.Find("Station/Speed").gameObject.GetComponent<Slider>().value;

        // Vector3 force = new Vector3(speed * rb.mass * Mathf.Sin(angle), 0.0f, speed * rb.mass * Mathf.Cos(angle));
        // rb.AddForce(force, ForceMode.Impulse);

        // Get the overall bowling setup forward. This is our forward vector.
        Transform t = GameObject.FindWithTag("Bowling Setup").gameObject.transform;
        Vector3 forward = -Vector3.Normalize(t.forward);

        Quaternion rotation = Quaternion.AngleAxis(angle, t.up);

        rb.constraints = RigidbodyConstraints.None;
        rb.velocity = rotation * (speed * forward);
    }
}
