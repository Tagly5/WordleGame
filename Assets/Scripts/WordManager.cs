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
using System.IO;
using UnityEngine.Rendering;


[System.Serializable]
public class Word
{
    public Text[] letters;
    public Image[] letterBackGround;

}


public class WordManager : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent groupWonEvent;
    [HideInInspector]
    public UnityEvent groupLostEvent;

    int _rightLetterGuesses = 0;
    public bool hasWon = false;
    public List<string> possibleWordsList = new List<string>();
    public string chosenWord;
    public Word[] words;

    public Color wrongLetterColor, rightPlaceColor, wrongPlaceColor;
    public KeyButton[] keyButtons;

    private int _letterIndex;
    private int _wordIndex;

    private readonly int _wordMaxLenght = 5;
    public GameObject NotAWord;

    [SerializeField] TextAsset file;


    void Start()
    {
        //Word database setup
        String fileText = file.text;
        String[] wordsSplitted = fileText.Split(",");
        for (int i = 0; i < wordsSplitted.Length; i++)
        {
            possibleWordsList.Add(wordsSplitted[i]);
        }

        // Selecting the winning word
        chosenWord = possibleWordsList[Random.Range(0, possibleWordsList.Count)].ToUpper();

        keyButtons = FindObjectsOfType<KeyButton>();

        NotAWord.gameObject.SetActive(false);

    }


    public void SetLetter(string letter)
    {
        if (hasWon || _letterIndex == _wordMaxLenght)
        {
            // Se esse bloco de palavras ja tiver ganhado ou perdido, nao atualiza o input
            return;
        }

        // NotAWord.gameObject.SetActive(false);
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
            words[_wordIndex].letters[_letterIndex].text = "";
        }

    }

    public void CheckWord()
    {
        NotAWord.gameObject.SetActive(false);
        
        List<string> leftLetters = chosenWord.Select(x => x.ToString()).ToList();
        string wordWritten = "";

        for (int i = 0; i < words[_wordIndex].letters.Length; i++)
        {
            wordWritten += words[_wordIndex].letters[i].text;
        }

        if (hasWon)
        {
            return;
        }
        else if (_letterIndex == _wordMaxLenght && possibleWordsList.Contains(wordWritten, StringComparer.OrdinalIgnoreCase))
        {
            StartCoroutine(CheckWordStatus());
        }
        else
        {
            NotAWord.gameObject.SetActive(true);
        }

    }

    private IEnumerator CheckWordStatus()
    {
        List<string> leftLetters = chosenWord.Select(x => x.ToString()).ToList();
        for (int i = 0; i < words[_wordIndex].letters.Length; i++)
        {
            chosenWord = chosenWord.ToUpper();



            if (words[_wordIndex].letters[i].text.Contains(chosenWord[i])) // Achou a letra certa na posicao certa!
            {
                words[_wordIndex].letterBackGround[i].color = rightPlaceColor;
                leftLetters[i] = string.Empty;
                SetKeyColor(words[_wordIndex].letters[i].text, rightPlaceColor, true);

                _rightLetterGuesses++;

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
        _wordIndex++;
        _letterIndex = 0;

        if (_rightLetterGuesses == _wordMaxLenght)
        {
            hasWon = true;
            groupWonEvent.Invoke();

        }
        else
        {
            _rightLetterGuesses = 0;
        }

        if (_wordIndex == 8 && hasWon == false)
        {
            groupLostEvent.Invoke();
        }

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
