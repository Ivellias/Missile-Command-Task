using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    /// <summary>
    /// Sprites for building, in bigger projects use animator instead
    /// </summary>
    public Sprite[] sprites;

    public SpriteRenderer leftPart;
    public SpriteRenderer rightPart;

    private bool _alive;

    void Start()
    {
        _alive = true;
    }

    public bool IsAlive()
    {
        return _alive;
    }

    public void Regen()
    {
        leftPart.sprite = sprites[0];
        rightPart.sprite = sprites[1];
        _alive = true;
    }

    /// <summary>
    /// Method called upon enemy hit
    /// </summary>
    public void Hitted()
    {
        leftPart.sprite = sprites[2];
        rightPart.sprite = sprites[3];
        _alive = false;
        GameController.BuildingHitted();
    }

    /// <summary>
    /// Method called upon getting power up package
    /// </summary>
    public void PowerUpGained(float duration, uint id)
    {
        GameController.ReceivePowerUp(duration, id);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (_alive)
        {
            if (collision.gameObject.GetComponent<FallingPowerUp>() != null)
            {
                FallingPowerUp powerUp = collision.gameObject.GetComponent<FallingPowerUp>();
                PowerUpGained(powerUp.GetPowerUpDuration(), powerUp.GetPowerUpId());
                GameController.AddScore(-powerUp.scoreValue);
            }
            else if (collision.gameObject.GetComponent<Entity>() != null)
            {
                Hitted();
                GameObject.Destroy(collision.gameObject);
            }
        }
        else
        {
            GameObject.Destroy(collision.gameObject);
        }

    }



}
