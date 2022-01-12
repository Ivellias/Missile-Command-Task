using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEnemy : FallingEntity
{
    public int scoreValue;

    public override void Effect()
    {
        GameController.AddScore(scoreValue);
    }

    public void SetValues(Vector3 toTarget, float speed)
    {
        _toTarget = toTarget;
        _speed = speed;
    }

    void Update()
    {
        transform.position = transform.position + Time.deltaTime * _speed * _toTarget;
    }
}
