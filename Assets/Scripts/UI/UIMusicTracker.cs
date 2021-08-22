using UnityEngine;
using UnityEngine.UI;

public class UIMusicTracker : MonoBehaviour
{
    [SerializeField] private SimpleMusicChanger _musicPlayer;
    [SerializeField] private Sprite _spriteOn;
    [SerializeField] private Sprite _spriteOff;
    

    private Image _image;

    void Awake()
    {
        _image = GetComponent<Image>();
    }

    void UpdateSprite(bool b)
    {
        _image.sprite = (b)? _spriteOff : _spriteOn;
    }

    private void OnSceneChanging()
    {
        _musicPlayer.OnMuteChange -= UpdateSprite;
    }

    private void OnPlayerLoaded()
    {
        _musicPlayer.OnMuteChange += UpdateSprite;
    }

}
