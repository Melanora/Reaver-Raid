using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SimpleSpriteAnimator : MonoBehaviour
{
    public enum Mode {Loop, Oneshot};
    [SerializeField] Mode _mode;
    [SerializeField] private float _spriteChangeDelay;
    [SerializeField] private Sprite[] _sprites;

    private SpriteRenderer _rend;
    private float _timer;
    private int _spriteLast;
    private int _spriteCurrent;

    void Awake()
    {
        _rend = GetComponent<SpriteRenderer>();
        _timer = _spriteChangeDelay;
        _spriteCurrent = 0;
        _spriteLast = _sprites.Length-1;
    }


    void Update()
    {
        if(_spriteLast > 0)
            SpriteChangeTimer();
    }

    void SpriteChangeTimer()
    {
        _timer -= Time.deltaTime;
        if(_timer <= 0)
        {
            _timer = _spriteChangeDelay;
            ChangeSprite();
        }

    }

    void ChangeSprite()
    {
        if(++_spriteCurrent > _spriteLast) 
        {
            _spriteCurrent = 0;
            if(_mode == Mode.Oneshot)
                    gameObject.SetActive(false);
        }
        _rend.sprite = _sprites[_spriteCurrent];
    }

    void OnEnable()
    {
        _spriteCurrent = 0;
        _timer = _spriteChangeDelay;
    }

}
