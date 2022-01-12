using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawner : MonoBehaviour
{

    public void SpawnRocket(GameObject rocketObject, float speed, Vector2 targetPosition)
    {
        Vector3 position = this.transform.position + new Vector3(Random.Range(0.0f, GameController.PLATFORM_LENGTH) - (GameController.PLATFORM_LENGTH / 2.0f), 0.0f, 0.0f);
        Quaternion rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, Mathf.Atan2(position.y - targetPosition.y, position.x - targetPosition.x) * Mathf.Rad2Deg - 90.0f));

        GameObject rocket = Instantiate(rocketObject, position, rotation);

        Vector2 targetVector = new Vector2(targetPosition.x - rocket.transform.position.x, targetPosition.y - rocket.transform.position.y);
        targetVector = targetVector.normalized;

        rocket.GetComponent<FallingEnemy>().SetValues(targetVector, speed);  
    }

}
