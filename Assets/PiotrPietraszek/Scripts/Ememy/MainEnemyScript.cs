using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PiotrPietraszek
{
    //handle enemy behaviors, health and powerups influence
    public class MainEnemyScript : MonoBehaviour
    {
        [SerializeField] private GameObject _bullet;
        [SerializeField] private int _timeBetweenShoots = 1;
        [SerializeField] private int _healh = 2;
        private GameObject _player;
        private GameObject _bulletParent;
        private Vector3 _moveDirection;
        private float _timerEnds;
        private float _timerCount;
        private bool _noMove = false;

        private void Awake()
        {
            PowerUpsHandling.FreezingEvent += FreezEfect;
            PowerUpsHandling.UnfreezingEvent += Unfreez;
        }

        private void Start()
        {
            _player = GameObject.Find("Player");
            _bulletParent = GameObject.Find("Bullets");
        }
        private void FixedUpdate()
        {
            _timerCount += Time.deltaTime;
            HealthCheck();
            DirectionCalc();
            if (_noMove) return;
            EnemyMove.EnemyMoving(gameObject, _moveDirection);
            if (_timerCount > _timerEnds)
            {
                ShootingScript.ShootingBullet(_bullet, _bulletParent, transform.position, -_moveDirection);
                Timer(_timeBetweenShoots);
            }
        }

        private void Timer(int time)
        {
            _timerEnds = Time.time + time;
            _timerCount = Time.time;

        }


        private void HealthCheck()
        {
            if (_healh > 0) return;
            EnemySpawner.Instance.ObjectDestroyed();
            Destroy(gameObject);
        }

        private void DirectionCalc()
        {
            _moveDirection = -Helpers.DirectionCalculation(gameObject.transform.position, _player.transform.position);
        }

        private void FreezEfect()
        {
            _noMove = true;
        }

        private void Unfreez()
        {
            _noMove = false;
        }
        private void OnCollisionEnter(Collision collision)
        {

            string name = collision.gameObject.tag;
            if (name == "Player" || name == "Bullet")
            {
                if (PowerUpsHandling.Instance.IsShootingPowerup && name == "Bullet") _healh -= 2;
                else
                    _healh--;
            }
        }


    }
}
