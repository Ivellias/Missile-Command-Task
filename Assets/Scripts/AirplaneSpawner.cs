using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneSpawner : MonoBehaviour
{
    /// <summary>
    /// Orientation of spawner
    /// </summary>
    public int orientation;

    /// <summary>
    /// Instantiates plane 
    /// </summary>
    /// <param name="airplaneObject">Prefab of airplane</param>
    /// <param name="height">Height difference between normal</param>
    public void SpawnPlane(GameObject airplaneObject, float height, int ammo, GameObject cargo, float speed)
    {
        GameObject airplane = Instantiate(airplaneObject, this.transform.position, new Quaternion());
        airplane.transform.position = airplane.transform.position + new Vector3(0.0f, height, 0.0f);
        airplane.GetComponent<AirplaneController>().SetValues(ammo, orientation, cargo, speed);
    }

}
