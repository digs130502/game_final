using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] bool isPlayerInArea = false;
    [SerializeField] Rigidbody2D player;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInArea)
        {
            player.gravityScale = 0;

            if (Input.GetKey(KeyCode.W))
            {
                ClimbLadder(1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                ClimbLadder(-1);
            }
            else
            {
                player.velocity = new Vector2(player.velocity.x, 0);
            }
        }
    }

    void ClimbLadder(int direction)
    {
        player.velocity = new Vector2(player.velocity.x, 5f * direction);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInArea = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInArea = false;

            player.gravityScale = 5;
        }
    }

}
