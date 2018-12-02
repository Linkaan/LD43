using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public MantisMove selector;

    public Text gameOverText;

    public void Start() {
        float timeAlive = selector.timeAlive;
        gameOverText.text = timeAlive + " seconds alive\n" + selector.mantisDestroyedCount + " eaten mantis heads\n";
        FindObjectOfType<ScoreManager>().LoadLeaderboard();
        selector.gameObject.SetActive(false);
    }

    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit() {
        Application.Quit();
    }

}
