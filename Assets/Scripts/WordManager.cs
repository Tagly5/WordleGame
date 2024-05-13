using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using DG.Tweening;
using UnityEngine.SceneManagement;


[System.Serializable]
public class Word
{
    public Text[] letters;
    public Image[] letterBackGround;

}


public class WordManager : MonoBehaviour
{

    public UnityEvent groupWonEvent;

    int _rightLetterGuesses = 0;
    public bool hasWon;
    public string[] wordList;
    public string chosenWord;
    public Word[] words;

    public Color wrongLetterColor, rightPlaceColor, wrongPlaceColor;
    public KeyButton[] keyButtons;

    private int _letterIndex;
    private int _wordIndex;
    
    private readonly int _letterQty = 5;
    public GameObject NotAWord;


    void Start()
    {
        chosenWord = wordList[Random.Range(0, wordList.Length)].ToUpper();
        keyButtons = FindObjectsOfType<KeyButton>();
        
        
        NotAWord.gameObject.SetActive(false);
        
    }


    public void SetLetter(string letter)
    {
        if (hasWon || _letterIndex == _letterQty)
        {
            return;
        }
        NotAWord.gameObject.SetActive(false);
        words[_wordIndex].letters[_letterIndex].text = letter;
        _letterIndex++;
    }
    

    public void BackSpace()
    {
        if (hasWon)
        {
            return;
        }
        if (_letterIndex != 0)
        {
            _letterIndex--;
            words[_wordIndex].letters[_letterIndex].text = " ";
        }
    }

    public void CheckWord()
    {
        if (hasWon)
        {
            return;
        }
        if (_letterIndex == _letterQty)
        {
            StartCoroutine(CheckWordStatus());
        }
        else
        {   NotAWord.gameObject.SetActive(false);
            NotAWord.gameObject.SetActive(true);
        }
        
    }
    
    private IEnumerator CheckWordStatus()
    {
        for (int i = 0; i < words[_wordIndex].letters.Length; i++)
        {
            
            List<string> leftLetters = chosenWord.Select(x => x.ToString()).ToList();
            chosenWord = chosenWord.ToUpper();
            
            

            if (words[_wordIndex].letters[i].text.Contains(chosenWord[i]))
            {
                words[_wordIndex].letterBackGround[i].color = rightPlaceColor;
                leftLetters[i] = string.Empty;
                SetKeyColor(words[_wordIndex].letters[i].text, rightPlaceColor, true);
                
                _rightLetterGuesses++;
                if(_rightLetterGuesses == _letterQty)
                {
                    hasWon = true;
                    groupWonEvent.Invoke();
                    
                }
            }
            else if (chosenWord.Contains(words[_wordIndex].letters[i].text) && leftLetters.Contains(words[_wordIndex].letters[i].text))
            {
                words[_wordIndex].letterBackGround[i].color = wrongPlaceColor;
                leftLetters[i] = string.Empty;
                SetKeyColor(words[_wordIndex].letters[i].text, wrongPlaceColor);
            }
            else
            {
                words[_wordIndex].letterBackGround[i].color = wrongLetterColor;
                SetKeyColor(words[_wordIndex].letters[i].text, wrongLetterColor);
                
            }
            
            yield return new WaitForSeconds(0.4f);
            
        }
        
        if (_wordIndex == 9 && hasWon == false)
        {
            SceneManager.LoadScene("Game");
        }
        _wordIndex++;
        _letterIndex = 0;
        
    }


    private void SetKeyColor(string letter, Color color, bool correct = false)
    {
        foreach (KeyButton t in keyButtons)
        {
            if (t.key == letter)
            {
                t.SetColor(color, correct);
                break;
            }
        }
    }
}
