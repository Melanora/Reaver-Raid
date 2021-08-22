using System;
using UnityEngine;
using UnityEngine.UI;


public class UIScoreTracker : MonoBehaviour
{
    private Text _text;

    void Start()
    {
        _text = GetComponent<Text>();
        GameManager.Instance.ScoreChanged += OnScoreChanged;
        OnScoreChanged(this, new IntArgs(GameManager.Instance.Score));
    }

    void OnScoreChanged(object sender, IntArgs s)
    {
        if(_text != null)
            _text.text = s.Value.ToString();
    }


}
