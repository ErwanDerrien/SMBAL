using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScieScript : MonoBehaviour
{
    [SerializeField] public float _range;
    [SerializeField] public float _speed;
    [SerializeField] private float _distance;
    [SerializeField] public float _angle;
    private int _position;
    private Transform _transformScie;
    private bool _direction = false; 
    private int _switch = 1;
    // direction == true -> ca monte
    // direction == false -> ca descend
    void Start()
    {
        _transformScie = GetComponent<Transform>();
        InvokeRepeating("MoveScie", 0.1f, _speed);
    }

 
    void Update()
    {
        
    }

    void MoveScie()
    {
        if (_direction)
        {
            _transformScie.Translate(Vector3.up * _range);
        }

        if (!_direction)
        {
            _transformScie.Translate(Vector3.down * _range);
        }
        
        _position = _position + _switch;
        
        if (_position >= _distance || _position  <= 0)
        {
            _direction = (1 * _switch == 1);
            _switch = _switch * -1;
        }
    } 
}
