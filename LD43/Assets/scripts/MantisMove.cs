using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MantisMove : MonoBehaviour {

    public float scale;
    public float maxSpeed;

    public float energyLoss;
    public float lowEnergyPulseFreq;

    public float mantisEnergy;
    public float maxMantisEnergy;

    public Slider mantisEnergySlider;

    Rigidbody2D rb;
    Vector3 initMouse;
    SpriteOutline outline;
    new SpriteRenderer renderer;
    float tick;
    bool playedLowEnergySFX;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        outline = GetComponent<SpriteOutline>();
        renderer = GetComponent<SpriteRenderer>();
        initMouse = Input.mousePosition;
        mantisEnergySlider.maxValue = maxMantisEnergy;
    }

    void Update() {
        mantisEnergy -= energyLoss * Time.deltaTime;

        mantisEnergySlider.value = mantisEnergy;

        if (mantisEnergy < maxMantisEnergy * 0.25) {
            outline.color = (int)tick % 2 == 0 ? Color.black : Color.red;

            if (!playedLowEnergySFX) {
                playedLowEnergySFX = true;
                GetComponent<SFXPlayer>().PlayLowEnergySFX();
            }
        } else {
            outline.color = Color.black;
            playedLowEnergySFX = false;
        }

        if (mantisEnergy <= 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void FixedUpdate() {
        float delta = (Input.mousePosition.x - Camera.main.WorldToScreenPoint(transform.position).x) * scale;
        if (Mathf.Abs(delta) > maxSpeed) delta = Mathf.Abs(delta) / delta * maxSpeed;
        tick += Time.fixedDeltaTime * lowEnergyPulseFreq;
        rb.AddForce(Vector3.right * delta);

        //renderer.flipY = rb.velocity.y < 0;
        renderer.flipX = delta > 0;
    }

    public void GainEnergy(float gain) {        
        mantisEnergy += gain;
        mantisEnergy = Mathf.Min(maxMantisEnergy, mantisEnergy);
    }
}
