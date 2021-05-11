using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ScieScript : MonoBehaviour
{
    [SerializeField] public float range;
    [SerializeField] public float speed;
    [SerializeField] private float distance;
    [SerializeField] public float angle;
    private int _position;
    private Transform _transformScie;
    private bool _direction = false; 
    private int _switch = 1;
    // direction == true -> ca monte
    // direction == false -> ca descend
    void Start()
    {
        _transformScie = GetComponent<Transform>();
        InvokeRepeating("MoveScie", 0.1f, speed);
    }
    

    void MoveScie()
    {
        if (_direction)
        {
            _transformScie.Translate(Vector3.up * range);
        }

        if (!_direction)
        {
            _transformScie.Translate(Vector3.down * range);
        }
        
        _position = _position + _switch;
        
        if (_position >= distance || _position  <= 0)
        {
            _direction = (1 * _switch == 1);
            _switch = _switch * -1;
        }
    } 
}
