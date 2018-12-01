using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    public Transform target;
    public Vector3 offset;

    void Start() {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }

    void Update() {
        transform.position = target.position + offset;
    }
}
