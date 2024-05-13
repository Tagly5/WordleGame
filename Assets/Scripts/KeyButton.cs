using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyButton : MonoBehaviour
{
    public string key;
    public bool isCorrect;
    Image _backGroundImage;
    // Start is called before the first frame update
    void Start()
    {
        _backGroundImage = GetComponent<Image>();
    }


    public void SetColor(Color color, bool correct = false)
    {
        if(isCorrect)
            return;

            isCorrect = correct;
            _backGroundImage.color = color;
    }
}
