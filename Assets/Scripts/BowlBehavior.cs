using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowlBehavior : MonoBehaviour
{
    public void OnBowl() {
        float angle = GameObject.Find("Station/X").gameObject.GetComponent<Slider>().value;
        float speed = GameObject.Find("Station/Speed").gameObject.GetComponent<Slider>().value;
        GameObject ball = GameObject.Find("BowlingBall").gameObject;
        Rigidbody rb = ball.GetComponent<Rigidbody>();

        Vector3 force = new Vector3(speed * rb.mass * Mathf.Sin(angle), 0.0f, speed * rb.mass * Mathf.Cos(angle));
        rb.AddForce(force, ForceMode.Impulse);

        // rb.velocity = new Vector3(speed * Mathf.Sin(angle), 0.0f, speed * Mathf.Cos(angle));
    }
}
