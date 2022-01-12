using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPowerUp : FallingEntity, IPowerUp
{
    public int scoreValue;

    /// <summary>
    /// Duration of power up
    /// </summary>
    private float _duration = 8.0f;

    /// <summary>
    /// Id of given power up
    /// </summary>
    private uint _powerUpId = 0;

    public override void Effect()
    {
        GameController.AddScore(scoreValue);
    }

    public uint GetPowerUpId()
    {
        return _powerUpId;
    }

    public float GetPowerUpDuration()
    {
        return _duration;
    }

    public void SetValues(Vector3 toTarget, float speed/*, float duration, uint powerUpId*/)
    {
        _toTarget = toTarget;
        _speed = speed;
        /*_duration = duration;
        _powerUpId = powerUpId;*/
    }

    void Update()
    {
        transform.position = transform.position + Time.deltaTime * _speed * _toTarget;
    }
}
