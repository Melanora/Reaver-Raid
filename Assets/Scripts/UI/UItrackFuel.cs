using UnityEngine;

public class UItrackFuel : MonoBehaviour
{
    [SerializeField] private PlayerFuel _playerFuel;
    [SerializeField] private float _top;
    [SerializeField] private float _bottom;

    private RectTransform _rt;
    private Vector3 _pos;
    private float _height;

    void OnEnable()
    {
        _rt = GetComponent<RectTransform>();
        _pos = _rt.anchoredPosition;
        _height = _top - _bottom;
        PlayerFuel.FuelChanged += OnFuelChanged;
    }

    void OnDisable()
    {
        PlayerFuel.FuelChanged -= OnFuelChanged;
    }

    private void OnFuelChanged(float f)
    {
        UpdatePosition(f);
    }

    void UpdatePosition(float newFuel)
    {
        _pos.y = newFuel * _height + _bottom;
        _rt.anchoredPosition = _pos;
    }

}
