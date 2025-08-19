using UnityEngine;

public class MovmentPlatform : MonoBehaviour 
{
    [SerializeField] private float _duration;
    [SerializeField] private Vector3[] _pathPoins;
    [SerializeField] private bool _Reversed = false;

    private float _speed;
    private bool _isReversed = false;
    private int _currentIndex = 0;

    private void Awake()
    {
        if (_pathPoins.Length < 2)
            Debug.LogError("Количество точек должно быть > 2");
        float distance = 0f;
        for (int i = 1; i < _pathPoins.Length; i++)
            distance += (_pathPoins[i] - _pathPoins[i - 1]).magnitude;
        _speed = distance / _duration;
        if (_Reversed) 
        {
            _isReversed = true;
            _currentIndex = _pathPoins.Length - 1;
        }
        transform.position = _pathPoins[_currentIndex];
    }

    private void Update()
    {
        int encreseIndex;
        if (_isReversed)
        {

            encreseIndex = _currentIndex - 1;
            transform.position = Vector3.MoveTowards(transform.position, _pathPoins[encreseIndex], _speed * Time.deltaTime);
            _currentIndex = transform.position == _pathPoins[encreseIndex] ? encreseIndex : _currentIndex;
            _isReversed = _currentIndex != 0;
        }
        else 
        {
            encreseIndex = _currentIndex + 1;
            transform.position = Vector3.MoveTowards(transform.position, _pathPoins[encreseIndex], _speed * Time.deltaTime);
            _currentIndex = transform.position == _pathPoins[encreseIndex] ? encreseIndex : _currentIndex;
            _isReversed = _currentIndex == _pathPoins.Length - 1;
        }
    }
}
