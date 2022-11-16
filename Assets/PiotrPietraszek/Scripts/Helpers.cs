using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PiotrPietraszek
{
    //class with methods used in diferent places
    public class Helpers : MonoBehaviour
    {
        public static Vector3 DirectionCalculation(Vector3 initial, Vector3 target)
        {
            Vector3 direction;
            direction = (initial - target);
            direction.y = 1; ;
            return direction.normalized;
        }

        public static Vector3 MouseWordPos(Vector3 mousePos)
        {
            mousePos.z = 19;
            return Camera.main.ScreenToWorldPoint(mousePos);
        }

        public static void ObjectRotation(GameObject target, Vector3 amount)
        {
            //target.transform.Rotate(amount * Time.deltaTime) ;
            target.transform.eulerAngles = Vector3.Lerp(target.transform.rotation.eulerAngles, amount, Time.deltaTime * 10);
        }

        public static Vector3 PositionGenerating(int area)
        {
            Vector3 _positionofSpawn;
            _positionofSpawn = new Vector3(Random.Range(-area, area), 1, Random.Range(-area, area));
            return _positionofSpawn;
        }


        public static void DestroingAllChildren(GameObject parent)
        {
            for (var i = parent.transform.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(parent.transform.GetChild(i).gameObject);
            }
        }
    }

}

