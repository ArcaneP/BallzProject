using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ball;
    public Transform spawnPos;

    public int maxBalls = 20;
    int counter;

    private void FixedUpdate()
    {
        if(counter != maxBalls)
        {
            Instantiate(ball, spawnPos.position, Quaternion.identity);
            counter++;
        }
    }


}
