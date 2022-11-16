using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PiotrPietraszek
{
    //script for shooting for player and enemy
    public class ShootingScript : MonoBehaviour
    {
        public static void ShootingBullet(GameObject bullet, GameObject parent, Vector3 position, Vector3 direction)
        {
            position -= direction * 3;

            GameObject newBullet;
            newBullet = Instantiate(bullet, position, Quaternion.identity);
            newBullet.transform.parent = parent.transform;
        }
    }
}
