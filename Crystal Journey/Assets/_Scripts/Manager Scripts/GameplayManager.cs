﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour {

    public static GameplayManager instance;

    [SerializeField]
    private Text countdownText;

    public int countdownTimer = 90;

    [SerializeField]
    private Text scoreText;

    private int scoreCount;

    [SerializeField]
    private Image scoreFillUI;

    void Awake() {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start() {

        DisplayScore(0);

        countdownText.text = countdownTimer.ToString();

        StartCoroutine("Countdown");

    }

    IEnumerator Countdown() {
        yield return new WaitForSeconds(1f);

        countdownTimer -= 1;

        countdownText.text = countdownTimer.ToString();

        if(countdownTimer <= 10) {
            SoundManager.instance.TimeRunningOut(true);
        }

        StartCoroutine("Countdown");

        if(countdownTimer <= 0) {
            StopCoroutine("Countdown");

            SoundManager.instance.GameEnd();
            SoundManager.instance.TimeRunningOut(false);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            StartCoroutine(RestartGame());
        }

    } // countdown

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            DisplayScore(0);
        }
    }

    public void DisplayScore(int scoreValue)
     {
        if (scoreText == null)
            return;

        scoreCount += scoreValue;
        scoreText.text = "$ " + scoreCount;

        scoreFillUI.fillAmount = (float)scoreCount / 100f;

        if(scoreCount >= 100) {
            StopCoroutine("Countdown");
            SoundManager.instance.GameEnd();
            StartCoroutine(RestartGame());
        }
    }

    IEnumerator RestartGame() {
        yield return new WaitForSeconds(4f);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }

} // class































