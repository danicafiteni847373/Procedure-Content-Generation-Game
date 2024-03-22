using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTheGame : MonoBehaviour
{
    public GameOverScript go;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("trigger"))
        {
            Debug.Log("Start");
        }

        if (other.CompareTag("gameover"))
        {
            Debug.Log("Game Over");
            FindObjectOfType<GameOverScript>().EndGame();
        }

    }
}
