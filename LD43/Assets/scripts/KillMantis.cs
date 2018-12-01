using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillMantis : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
