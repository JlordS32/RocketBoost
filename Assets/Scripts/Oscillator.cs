using Unity.Mathematics;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _movementVector;

    // Variables
    private Vector3 _endPosition;
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
        _endPosition = _startPosition + _movementVector;
    }

    private void Update()
    {
        float movementFactor = Mathf.PingPong(Time.time * _speed, 1f);
        transform.position = Vector3.Lerp(_startPosition, _endPosition, movementFactor);
    }
}
