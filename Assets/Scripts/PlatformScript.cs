using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlatformScript : MonoBehaviour
{
    [SerializeField] public float range;
    [SerializeField] public float speed;
    [SerializeField] private float distance;
    [SerializeField] public float angle;
    private int _position;
    private Transform _transformPlatform;
    private bool _direction = false; 
    private int _switch = 1;
    // direction == true -> ca droite
    // direction == false -> ca gauche
    void Start()
    {
        _transformPlatform = GetComponent<Transform>();
        InvokeRepeating("MovePlatform", 0.1f, speed);
    }
    void MovePlatform()
    {
        if (_direction)
        {
            _transformPlatform.Translate(Vector3.right * range);
        }

        if (!_direction)
        {
            _transformPlatform.Translate(Vector3.left * range);
        }
        
        _position = _position + _switch;
        
        if (_position >= distance || _position  <= 0)
        {
            _direction = (1 * _switch == 1);
            _switch = _switch * -1;
        }
    } 
}
