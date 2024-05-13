using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinManager : MonoBehaviour
{

    public bool winState;
    public GameObject winScreen;
    
    
    
    public List<WordManager> wordManagers;

    private void Start()
    {
        winScreen.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        foreach (var wordManager in wordManagers)
        {
            wordManager.groupWonEvent.AddListener(OnGroupWon);
        }
    }
    

    private void OnGroupWon()
    {
        foreach (var wordManager in wordManagers)
        {
            if (wordManager.hasWon == false)
            {
                return;
            }
        }

        CheckWinState();
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
    }
}