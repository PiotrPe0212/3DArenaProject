using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PiotrPietraszek
{
    //WASD player controlling
    public class PlayerMovement : MonoBehaviour
    {
        public static void PlayerMove(GameObject player, int speed, int arena)
        {
            Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (player.transform.position.x >= arena && Movement.x > 0) return;
            if (player.transform.position.x <= -arena && Movement.x < 0) return;
            if (player.transform.position.z >= arena && Movement.z > 0) return;
            if (player.transform.position.z <= -arena && Movement.z < 0) return;
            player.transform.position += Movement * speed * Time.deltaTime;
        }
    }

}

