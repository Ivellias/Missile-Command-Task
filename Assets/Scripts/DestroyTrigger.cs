using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component which destroys entities on trigger enter with with or without calling their effects
/// </summary>
public class DestroyTrigger : MonoBehaviour
{
    public bool callEffect;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Entity>() != null)
        {
            if (callEffect) collision.gameObject.GetComponent<Entity>().Effect();
            GameObject.Destroy(collision.gameObject);
        }
    }

}
