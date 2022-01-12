using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileExplosion : MonoBehaviour
{
    public readonly static float LIFE_TIME = 0.5f;

    void Start()
    {
        GameObject.Destroy(this.gameObject, LIFE_TIME);
    }

}
