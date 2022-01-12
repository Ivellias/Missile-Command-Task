using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneController : Entity
{
    static float MIN_DELAY = 0.2f;

    public int scoreValue;

    /// <summary>
    /// Amount of current avaiable ammunition
    /// </summary>
    private int _ammo;

    /// <summary>
    /// Tells if airplane moves from left to right (1) or vice versa (-1)
    /// </summary>
    private int _orientation;

    /// <summary>
    /// What type of ammunition is inside plane
    /// </summary>
    private GameObject _cargo;

    /// <summary>
    /// Speed of airplane
    /// </summary>
    private float _speed;

    public void SetValues(int ammo, int orientation, GameObject cargo, float speed)
    {
        _ammo = ammo;
        _orientation = orientation;
        _cargo = cargo;
        _speed = speed;
    }

    public override void Effect()
    {
        GameController.AddScore(scoreValue);
    }

    public void Start()
    {
        StartCoroutine(Shoot());
        if (_orientation < 0) gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }

    public void Update()
    {
        //this could be private variable vector initiated in constructor but speed in some point could be changed (maybe power up?)
        transform.position = transform.position + new Vector3(Time.deltaTime * _speed * _orientation, 0.0f, 0.0f);
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.75f/_speed);

        while (_ammo > 0)
        {
            yield return new WaitForSeconds(Random.Range(MIN_DELAY, GameController.PLATFORM_LENGTH / (_speed * _ammo)));

            Vector2 targetPosition = GameController.GetTarget();
            Vector3 position = this.transform.position + new Vector3(Random.Range(0.0f, GameController.PLATFORM_LENGTH) - (GameController.PLATFORM_LENGTH / 2.0f), 0.0f, 0.0f);
            Quaternion rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, Mathf.Atan2(position.y - targetPosition.y, position.x - targetPosition.x) * Mathf.Rad2Deg - 90.0f));

            GameObject cargo = Instantiate(_cargo, position, rotation);

            Vector2 targetVector = new Vector2(targetPosition.x - cargo.transform.position.x, targetPosition.y - cargo.transform.position.y);
            targetVector = targetVector.normalized;

            if(cargo.GetComponent<FallingEnemy>() != null) cargo.GetComponent<FallingEnemy>().SetValues(targetVector, _speed);
            else cargo.GetComponent<FallingPowerUp>().SetValues(targetVector, _speed);

        }
    }

}
