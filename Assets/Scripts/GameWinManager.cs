using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinManager : MonoBehaviour
{

    public GameObject winScreen;
    public GameObject loseScreen;
    public bool gameHasEnded = false;

    public int letterGroupWonCounter = 0;

    public List<WordManager> wordManagers;

    private void Start()
    {
        winScreen.gameObject.SetActive(false);
        loseScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if (letterGroupWonCounter == 4 && !gameHasEnded)
        {
            foreach (var wordManager in wordManagers)
            {
                if (wordManager.hasWon == false)
                {
                    CheckLoseState();
                    Debug.Log("perdeu");
                    gameHasEnded = true;
                    return;
                }
            }
            CheckWinState();
            Debug.Log("ganhou");
            gameHasEnded = true;
        }
    }

    private void CheckLoseState()
    {
        StartCoroutine(LoseScreenTimeDelay());
    }

    private void OnEnable()
    {
        foreach (var wordManager in wordManagers)
        {
            wordManager.groupWonEvent.AddListener(OnGroupWonOrLost);
            wordManager.groupLostEvent.AddListener(OnGroupWonOrLost);
        }
    }

    private void OnGroupWonOrLost()
    {
        letterGroupWonCounter++;
    }
    public void CheckWinState()
    {
        var winScreenTimeDelay = WinScreenTimeDelay();
        StartCoroutine(winScreenTimeDelay);

    }

    IEnumerator WinScreenTimeDelay()
    {
        yield return new WaitForSeconds(1.3f);

        winScreen.gameObject.SetActive(true);
        FindObjectOfType<AudioManager>().Play("WonGame");
    }
    IEnumerator LoseScreenTimeDelay()
    {
        yield return new WaitForSeconds(1.3f);

        loseScreen.gameObject.SetActive(true);
        FindObjectOfType<AudioManager>().Play("LostGame");

    }
    public void ReloadGame()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        SceneManager.LoadScene("Game");
    }
    public void QuitGame()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        Application.Quit();
    }
}