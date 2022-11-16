using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PiotrPietraszek
{
    //moving enemy in player  direction
    public class EnemyMove : MonoBehaviour
    {

        public static void EnemyMoving(GameObject enemy, Vector3 direction)
        {
            Vector3 _moveDirection = direction;
            _moveDirection.y = 0;
            enemy.transform.position += _moveDirection * 0.05f;
        }
    }
}
