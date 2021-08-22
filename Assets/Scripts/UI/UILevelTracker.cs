using System;
using UnityEngine;
using UnityEngine.UI;


public class UILevelTracker : MonoBehaviour
{
    private Text _text;

    void Start()
    {
        _text = GetComponent<Text>();
        GameManager.Instance.NewLevelReached += UpdateLevelText;
        UpdateLevelText(this, new IntArgs(GameManager.Instance.LevelReached));
    }

    void UpdateLevelText(object sender, IntArgs s)
    {
        if(_text != null)        
            _text.text = s.Value.ToString();
    }


}
