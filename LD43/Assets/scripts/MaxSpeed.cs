using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxSpeed : MonoBehaviour {

    public float maxSpeed;

    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = Vector2.Min(rb.velocity, new Vector2(0, maxSpeed));
	}
}
