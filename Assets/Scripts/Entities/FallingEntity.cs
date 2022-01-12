using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class for falling objects
/// </summary>
public abstract class FallingEntity : Entity
{
    /// <summary>
    /// Vector directed to target
    /// </summary>
    private protected Vector3 _toTarget;

    /// <summary>
    /// Speed of falling entity
    /// </summary>
    private protected float _speed;
}
