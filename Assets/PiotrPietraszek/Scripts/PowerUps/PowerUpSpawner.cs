using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PiotrPietraszek
{

    //spawn powerup in random place on arena
    public class PowerUpSpawner : MonoBehaviour, SpawnInterface
    {
        [SerializeField] GameObject _spawnHome;
        [SerializeField] public GameObject[] _spawnObjects;
        [SerializeField] public int _arenaDim = 23;
        [SerializeField] public int _spawnedNumber = 1;
        [SerializeField] public int _timetoWait = 5;
        public int _currentSpawnedNumber;


        public static PowerUpSpawner Instance;

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
            if (GameManager.Instance.State != GameManager.GameState.PlayGame) return;
            ObjectsNumberCheck();
        }

        public void SpawnObject()
        {
            GameObject enemy;
            enemy = Instantiate(_spawnObjects[Random.Range(0, _spawnObjects.Length)], Helpers.PositionGenerating(_arenaDim), Quaternion.identity);
            enemy.transform.parent = _spawnHome.transform;

        }

        public void InitialSpawn()
        {
            for (int i = 0; i < _spawnedNumber; i++)
            {
                SpawnObject();
                _currentSpawnedNumber++;
            }
        }

        public void ObjectsNumberCheck()
        {
            if (_currentSpawnedNumber >= _spawnedNumber) return;
            StartCoroutine(WaittoSpawn());
        }

        private void ResetParams()
        {
            Helpers.DestroingAllChildren(_spawnHome);
            _currentSpawnedNumber = 0;
            StopCoroutine(WaittoSpawn());
        }
        public void ObjectDestroyed()
        {
            _currentSpawnedNumber--;
        }
        public IEnumerator WaittoSpawn()
        {
            _currentSpawnedNumber++;
            yield return new WaitForSeconds(_timetoWait);
            SpawnObject();
        }
    }
}