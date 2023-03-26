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
          var newspawnPos =  new Vector2(spawnPos.position.x + Random.Range(0, 0.5f), spawnPos.position.y + Random.Range(0, 0.5f));

            Instantiate(ball, newspawnPos, Quaternion.identity);
            counter++;
        }
    }


}
