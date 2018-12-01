using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthObject : MonoBehaviour {

    public float maxHealth;
    public float health;
    public float speedMultiplier;
    public float speed;
    public float defenseMultiplier;
    public float defense;
    public float strengthMultiplier;
    public float strength;

    public List<GameObject> toDestroy;

    public Slider healthSlider;
    public Text speedText;
    public Text defenseText;
    public Text strengthText;

    void Awake() {
        toDestroy = new List<GameObject>();
    }

    void Update() {
        if (!healthSlider) return;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;

        speedText.text = "Speed: x" + speedMultiplier;
        strengthText.text = "Strength: x" + strengthMultiplier;
        defenseText.text = "Defense: x" + defenseMultiplier;
    }

    public void LoseHealth(float healthDelta) {
        health -= Mathf.Max(healthDelta / 10, healthDelta - defense);
        if (health <= 0) Die();
    }

    void Die() {

        if (gameObject.CompareTag("Player")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        foreach (GameObject go in toDestroy) {
            Destroy(go);
        }
        Destroy(gameObject);
    }
}
