using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PiotrPietraszek
{
    // interface for enemy and powerups spawner
    public interface SpawnInterface
    {
        void SpawnObject();
        void InitialSpawn();
        void ObjectsNumberCheck();
        void ObjectDestroyed();
        IEnumerator WaittoSpawn();
    }
}