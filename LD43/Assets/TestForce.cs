using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestForce : MonoBehaviour {

    public Rigidbody rb;

    public float initForce;

	// Use this for initialization
	void Start () {
        this.rb = this.GetComponent<Rigidbody>();
        this.rb.AddForce(this.transform.forward * initForce, ForceMode.Force);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
