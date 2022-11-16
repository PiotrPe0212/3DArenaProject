using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PiotrPietraszek
{
    // halnde powerups effects and dezactivate them
    public class PowerUpsHandling : MonoBehaviour
    {
        [SerializeField] private int _freezingTime = 10;
        [SerializeField] private int _shootPUTime = 10;
        public static event System.Action FreezingEvent;
        public static event System.Action UnfreezingEvent;
        public static PowerUpsHandling Instance;
        public bool IsShootingPowerup = false;
        private void Awake()
        {
            Instance = this;
        }

        public void FreezActivation()
        {
            FreezingEvent?.Invoke();
            StartCoroutine(FrozenTime());
        }

        public void ShootPUActivated()
        {
            IsShootingPowerup = true;
            StartCoroutine(ShootPUTime());
        }

        private IEnumerator FrozenTime()
        {
            yield return new WaitForSeconds(_freezingTime);
            UnfreezingEvent?.Invoke();
        }

        public IEnumerator ShootPUTime()
        {
            yield return new WaitForSeconds(_shootPUTime);
            IsShootingPowerup = false;
        }

    }
}
