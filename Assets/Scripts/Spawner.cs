using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ball;
    public Transform spawnPos;

    public int maxBalls = 20;
    int counter;

    public float randomMin = 0;
    public float randomMax = 0.5f;

    private void FixedUpdate()
    {
        if(counter != maxBalls)
        {
          var newspawnPos =  new Vector2(spawnPos.position.x + Random.Range(randomMin, randomMax), spawnPos.position.y + Random.Range(randomMin, randomMax));

            Instantiate(ball, newspawnPos, Quaternion.identity);
            counter++;
        }
    }


}
