using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PiotrPietraszek
{
    //destroing each bullet handling
    public class BulletDestroy : MonoBehaviour
    {
        [SerializeField] private float TimetoDestroy = 2;
        private float _timerCount;
        void Start()
        {
            TimetoDestroy += Time.time;
            _timerCount = Time.time;
        }

        private void FixedUpdate()
        {
            _timerCount += Time.deltaTime;
            if (_timerCount > TimetoDestroy) DestroyBullet();
        }

        private void DestroyBullet()
        {
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            DestroyBullet();
        }
    }
}


