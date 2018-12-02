using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour {

    public AudioClip[] collisionSFX;
    public AudioClip[] lowEnergySFX;
    public AudioClip[] eatSFX;

    public float collisionThreshold;
    public float collisionVolumeScale;

    new AudioSource audio;

	void Start () {
        audio = GetComponent<AudioSource>();
	}
	
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Mantis")) {
            audio.clip = eatSFX[Random.Range(0, eatSFX.Length)];
            audio.volume = 1.0f;
            audio.Play();
        } else if (col.relativeVelocity.magnitude > collisionThreshold && !audio.isPlaying) {
            audio.clip = collisionSFX[Random.Range(0, collisionSFX.Length)];
            audio.volume = Mathf.Min(1.0f, col.relativeVelocity.magnitude / collisionVolumeScale);
            audio.Play();
        }
    }

    public void PlayLowEnergySFX() {
        audio.clip = lowEnergySFX[Random.Range(0, lowEnergySFX.Length)];
        audio.volume = 1.0f;
        audio.Play();
    }
}