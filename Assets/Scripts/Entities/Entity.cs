using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class of entity. Every entity should inherit this class
/// </summary>
abstract public class Entity : MonoBehaviour
{
    abstract public void Effect();
}
