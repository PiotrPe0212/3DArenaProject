using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PiotrPietraszek
{

    // powerup behavior after collision with player
    public class FrozenPowerup : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.transform.tag != "Player") return;
            PowerUpsHandling.Instance.FreezActivation();
            PowerUpSpawner.Instance.ObjectDestroyed();
            Destroy(gameObject);
        }
    }
}
