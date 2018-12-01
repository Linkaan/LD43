using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveController : MonoBehaviour {

    public Text waveText;
    public Transform[] spawnSpots;
    public int wave;

    public GameObject enemyPrefab;
    public Transform player;
    public Transform canvas;

    public GameObject hud;

    public float speedMultiplier;
    public float strengthMultipler;
    public float defenseMultipler;

    public float speedGain;
    public float strengthGain;
    public float defenseGain;

    List<GameObject> enemiesToKill;

	void Start () {
        enemiesToKill = new List<GameObject>();
        NextWave();
	}
	
	void Update () {        
        if (enemiesToKill.Count == 0) {
            ShowHUD();
        }
	}

    void FixedUpdate() {
        for (int i = enemiesToKill.Count - 1; i >= 0; i--) {
            GameObject enemy = enemiesToKill[i];
            if (enemy == null) enemiesToKill.Remove(enemy);
        }
    }

    void ShowHUD() {
        hud.SetActive(true);
    }

    public void GainEnemyStrength() {
        strengthMultipler += strengthGain;
    }

    public void GainEnemySpeed()
    {
        speedMultiplier += speedGain;
    }

    public void GainEnemyDefense()
    {
        defenseMultipler += defenseGain;
    }

    public void NextWave() {
        hud.SetActive(false);

        wave += 1;

        waveText.text = "Wave " + wave;

        Transform randomSpot = spawnSpots[Random.Range(0, spawnSpots.Length)];
        EnemyController enemy = Instantiate(enemyPrefab, randomSpot.position, Quaternion.identity).GetComponent<EnemyController>();
        enemy.speedMultiplier = speedMultiplier;
        enemy.strengthMultipler = strengthMultipler;
        enemy.defenseMultipler = defenseMultipler;
        enemy.playerTarget = player;
        enemy.canvasParent = canvas;
        enemiesToKill.Add(enemy.gameObject);
    }
}
