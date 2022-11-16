using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PiotrPietraszek
{
    //player behaviors and health handling
    public class MainPlayerScript : MonoBehaviour
    {
        [SerializeField] private GameObject bullet;
        [SerializeField] private GameObject _bulletParent;
        [SerializeField] private int _moveSpeed = 10;
        [SerializeField] private float _timeBetweenShoots = 0.5f;
        [SerializeField] private int _initialHealth = 3;
        [SerializeField] private int _arenaDim = 23;
        private Vector3 _directionVector;
        private Vector3 _initialPosition;
        private int _health;
        private float _timerEnds;
        private float _timerCount;

        private void Awake()
        {
            GameManager.ResetGame += ResetParams;
        }
        private void OnDestroy()
        {
            GameManager.ResetGame -= ResetParams;
        }

        private void Start()
        {
            _health = _initialHealth;
            _initialPosition = transform.position;
        }
        void Update()
        {
            if (GameManager.Instance.State != GameManager.GameState.PlayGame) return;
            PlayerMovement.PlayerMove(gameObject, _moveSpeed, _arenaDim);
            _timerCount += Time.deltaTime;
            if (_timerCount > _timerEnds)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ShootingScript.ShootingBullet(bullet, _bulletParent, transform.position, _directionVector);
                    Timer(_timeBetweenShoots);
                }

            }
        }
        private void FixedUpdate()
        {
            if (GameManager.Instance.State != GameManager.GameState.PlayGame) return;
            DirectionCalc();
            Helpers.ObjectRotation(gameObject, DegCalculation());
        }

        private void Timer(float time)
        {
            _timerEnds = Time.time + time;
            _timerCount = Time.time;

        }
        private void DirectionCalc()
        {
            _directionVector = Helpers.DirectionCalculation(transform.position, Helpers.MouseWordPos(Input.mousePosition));
        }
        private Vector3 DegCalculation()
        {

            float x = _directionVector.x;
            float z = _directionVector.z;
            Vector3 result = new Vector3(0, Mathf.Atan2(x, z) * Mathf.Rad2Deg, 0);
            if (result.y < 0) result.y = 360 + result.y % 360;
            Vector3 mpos = Helpers.MouseWordPos(Input.mousePosition);
            return result;
        }

        private void ResetParams()
        {
            _health = _initialHealth;
            transform.position = _initialPosition;
            Helpers.DestroingAllChildren(_bulletParent);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "PowerUp") return;
            _health--;
            if (_health <= 0) GameManager.Instance.GameStateUpdate(GameManager.GameState.Lose);
        }
    }


}
