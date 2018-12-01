using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantisMove : MonoBehaviour {

    public float scale;
    public float maxSpeed;

    Rigidbody2D rb;
    Vector3 initMouse;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        initMouse = Input.mousePosition;
    }

    void FixedUpdate() {
        float delta = (Input.mousePosition.x - initMouse.x) * scale;
        if (Mathf.Abs(delta) > maxSpeed) delta = Mathf.Abs(delta) / delta * maxSpeed;
        rb.AddForce(Vector3.right * delta);
    }
}
