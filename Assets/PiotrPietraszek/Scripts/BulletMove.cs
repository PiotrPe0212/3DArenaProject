using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PiotrPietraszek
{
    //Bullets moving behaviors for player and enemy bullets
    public class BulletMove : MonoBehaviour
    {
        private Vector3 _drection;
        [SerializeField] private bool _playerBullet;
        private void Awake()
        {
            if (_playerBullet)
                _drection = -Helpers.DirectionCalculation(transform.position, Helpers.MouseWordPos(Input.mousePosition));
            else
            {
                GameObject _player = GameObject.Find("Player");
                _drection = -Helpers.DirectionCalculation(transform.position, _player.transform.position);
            }
            _drection.y = 0;
        }

        private void FixedUpdate()
        {
            transform.position += _drection * 0.4f;
        }
    }
}