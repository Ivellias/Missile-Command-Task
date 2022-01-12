using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public readonly static float PLATFORM_LENGTH = 6.0f;
    public readonly static float DIFFICULTY_MULTIPLIER = 0.04f;
    public readonly static float STARTING_DIFFICULTY = 1.0f;
    public readonly static float MIN_INTERVAL = 0.1f;

    private static int _score = 0;
    private static int _highscore = 0;

    /// <summary>
    /// Reference to current game controller
    /// </summary>
    private static GameController _controller;

    public static void AddScore(int score)
    {
        _score += score;
        _controller.UpdateScore();
    }

    public static void ReceivePowerUp(float duration, uint id)
    {
        _controller.AddPowerUp(duration, id);
    }

    public static void BuildingHitted()
    {
        _controller.BuildingDestroyed();
    }

    public static Vector2 GetTarget()
    {
        return _controller.GetRandomAliveTarget();
    }

    public Text scoreText;
    public Text highscoreText;

    public Image powerUpImage;

    public GameObject rocketPrefab;
    public GameObject bombPrefab;
    public GameObject powerUpPrefab;
    public GameObject airplaneGreenPrefab;
    public GameObject airplaneRedPrefab;
    public GameObject missileExplosionPrefab;

    public AirplaneSpawner rightAirplaneSpawner;
    public AirplaneSpawner leftAirplaneSpawner;
    public RocketSpawner rocketSpawner;

    /// <summary>
    /// Launcher riffle
    /// </summary>
    public GameObject Launcher;

    /// <summary>
    /// References to buildings
    /// </summary>
    public BuildingController[] buildings;


    private float _difficulty;
    private float _powerUpTime;
    private bool _powerUpActive;

    private int _aliveBuildings;

    private void UpdateScore()
    {
        scoreText.text = "Score: " + _score;
        highscoreText.text = "Highscore: " + _highscore;
    }

    private void UpdatePowerUpImage()
    {
        if (_powerUpActive) powerUpImage.enabled = true;
        else powerUpImage.enabled = false;
    }

    private void AddPowerUp(float duration, uint id)
    {
        _powerUpTime += duration;
        if (!_powerUpActive)
        {
            _powerUpActive = true;
            UpdatePowerUpImage();
        }
    }

    public void GameOver()
    {
        RestartGame();
    }

    private void RestartGame()
    {
        //reset difficulty
        _difficulty = STARTING_DIFFICULTY;

        //check for highscore and reset score
        if (_highscore < _score) _highscore = _score;
        _score = 0;
        UpdateScore();

        //regen buildings
        _aliveBuildings = buildings.Length;
        foreach(BuildingController building in buildings)
        {
            building.Regen();
        }

        //reset power ups
        _powerUpTime = 0.0f;
        _powerUpActive = false;
        UpdatePowerUpImage();

        //destroy all entities
        foreach(GameObject entity in GameObject.FindGameObjectsWithTag("Entity"))
        {
            GameObject.Destroy(entity);
        }

        StopAllCoroutines();
        StartCoroutine(SpawnEnemies());
    }

    public void BuildingDestroyed()
    {
        _aliveBuildings--;
        if (_aliveBuildings == 0) GameOver();
    }

    /// <returns>Random target position</returns>
    public Vector2 GetRandomAliveTarget()
    {
        int randomTarget = Random.Range(1, _aliveBuildings + 1);
        for(int i = 0; i < buildings.Length; i++)
        {
            if (buildings[i].IsAlive())
            {
                randomTarget--;
                if (randomTarget == 0) return buildings[i].transform.position;
            }
        }
        return new Vector2();
    }

    void Start()
    {
        _controller = this;
        RestartGame();
    }

    void Update()
    {
        _difficulty += Time.deltaTime * DIFFICULTY_MULTIPLIER;

        if (_powerUpActive)
        {
            _powerUpTime -= Time.deltaTime;
            if (_powerUpTime < 0.0f)
            {
                _powerUpActive = false;
                UpdatePowerUpImage();
            }
        }

        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //riffle rotation
        Launcher.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, Mathf.Atan2(Launcher.transform.position.y - cursorPosition.y, Launcher.transform.position.x - cursorPosition.x) * Mathf.Rad2Deg + 90.0f));

        //Shooting missiles
        if (Input.GetMouseButtonDown(0))
        {
            GameObject missile = Instantiate(missileExplosionPrefab, cursorPosition, new Quaternion());
            if (_powerUpActive) missile.transform.localScale = new Vector3(1.5f, 1.5f, 1.0f);
        }

    }

    /// <summary>
    /// It's main loop of spawning process
    /// </summary>
    /// <returns></returns>
    public IEnumerator SpawnEnemies()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(MIN_INTERVAL + (1.0f / _difficulty));
            int randomSpawn = Random.Range(0, 100);
            if(randomSpawn < 60)
            {
                rocketSpawner.SpawnRocket(rocketPrefab, 1.0f, GetRandomAliveTarget());
            } else if(randomSpawn < 75)
            {
                leftAirplaneSpawner.SpawnPlane(airplaneRedPrefab, Random.Range(-3.0f * (1.0f - 1.0f / _difficulty), 3.0f), 3, bombPrefab, 1.2f * _difficulty);
            } else if(randomSpawn < 90)
            {
                rightAirplaneSpawner.SpawnPlane(airplaneRedPrefab, Random.Range(-3.0f * (1.0f - 1.0f / _difficulty), 3.0f), 3, bombPrefab, 1.2f * _difficulty);
            } else if(randomSpawn < 95)
            {
                leftAirplaneSpawner.SpawnPlane(airplaneGreenPrefab, Random.Range(-3.0f * (1.0f - 1.0f / _difficulty), 3.0f), 1, powerUpPrefab, 1.2f * _difficulty);
            }
            else
            {
                rightAirplaneSpawner.SpawnPlane(airplaneGreenPrefab, Random.Range(-3.0f * (1.0f - 1.0f / _difficulty), 3.0f), 1, powerUpPrefab, 1.2f * _difficulty);
            }

        }

    }

}
