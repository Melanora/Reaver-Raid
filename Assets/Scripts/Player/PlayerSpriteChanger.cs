using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerSpriteChanger : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;

    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        GetComponent<PlayerInput>().HorizontalInputChanged += OnHorizontalInputChanged;
    }

    private void OnHorizontalInputChanged(object sender, IntArgs a)
    {
        _spriteRenderer.sprite = _sprites[a.Value +1];
    }

}
