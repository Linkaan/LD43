using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveEnergy : MonoBehaviour {

    public float energyGain;
    public MantisMove player;

    public float explosionForce;
    public GameObject mantisHeadPrefab;
    public GameObject bloodParticles;

    void Start() {
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.transform.CompareTag("Player")) {
            player.GainEnergy(energyGain);

            GameObject particles = Instantiate(bloodParticles, transform.position, Quaternion.identity);
            Rigidbody2D rb = Instantiate(mantisHeadPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            rb.AddForce(explosionForce * Random.insideUnitCircle);
            rb.AddForce(Vector2.up * explosionForce * 0.25f);
            rb.AddTorque(explosionForce * Random.value);
            Destroy(gameObject);
            Destroy(rb.gameObject, 5f);
            Destroy(particles, 1f);
        }
    }
}
