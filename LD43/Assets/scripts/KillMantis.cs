using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillMantis : MonoBehaviour {

    public GameObject gameOverGO;

    void OnTriggerEnter2D(Collider2D col) {
        gameOverGO.SetActive(true);
    }
}
