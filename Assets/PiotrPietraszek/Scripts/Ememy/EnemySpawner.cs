using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PiotrPietraszek
{
    //spawning enemies in random place of arena
    public class EnemySpawner : MonoBehaviour, SpawnInterface
    {
        public static EnemySpawner Instance;

        [SerializeField] private GameObject _spawnHome;
        [SerializeField] private GameObject _enemyObject;
        [SerializeField] private int _arenaDim = 23;
        [SerializeField] private int _enemyNumber = 3;
        [SerializeField] private int _timetoWait = 4;
        private int _currentEnemyNumber;

        private void Awake()
        {
            Instance = this;
            GameManager.ResetGame += ResetParams;
        }

        private void OnDestroy()
        {
            GameManager.ResetGame -= ResetParams;
        }
        void Start()
        {
            InitialSpawn();
        }

        private void FixedUpdate()
        {
            ObjectsNumberCheck();
        }

        public void SpawnObject()
        {
            GameObject enemyObj;
            enemyObj = Instantiate(_enemyObject, Helpers.PositionGenerating(_arenaDim), Quaternion.identity);
            enemyObj.transform.parent = _spawnHome.transform;

        }

        public void InitialSpawn()
        {
            for (int i = 0; i < _enemyNumber; i++)
            {
                SpawnObject();
                _currentEnemyNumber++;
            }
        }

        public void ObjectsNumberCheck()
        {
            if (_currentEnemyNumber >= _enemyNumber) return;
            StartCoroutine(WaittoSpawn());
        }
       

        public void ObjectDestroyed()
        {
            _currentEnemyNumber--;
        }

        private void ResetParams()
        {
            Helpers.DestroingAllChildren(_spawnHome);
            _currentEnemyNumber = 0;
        }
       public IEnumerator WaittoSpawn()
        {
            _currentEnemyNumber++;
            yield return new WaitForSeconds(_timetoWait);
            SpawnObject();
        }


    }
}