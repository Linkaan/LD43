using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour {

    void OnTriggerEnter(Collider col) {

        if (col.CompareTag("Player")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } else {
            HealthObject health = col.GetComponent<HealthObject>();
            if (health != null) health.LoseHealth(health.maxHealth);
        }
    }
}
